using AutoMapper;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;

namespace Strongbox.Application.Services
{
    public class DocumentService(IDocumentRepository docRepo, IAccessRequestRepository reqRepo, IUserRepository userRepo, IMapper mapper) : IDocumentService
    {
        public async Task<ICollection<DocumentAttributesResultDto>> GetDocumentsAttributesAsync(Guid userId)
        {
            var user = await userRepo.GetUserAsync(userId)
                       ?? throw new UnauthorizedAccessException();

            var documentsAttributesResult = mapper.Map<List<DocumentAttributesResultDto>>(await docRepo.GetDocumentsAsync());

            if (user.Role == PersonRole.Approver || user.Role == PersonRole.Admin)
            {
                documentsAttributesResult.ForEach(d => d.Access = AccessType.Edit);

                return documentsAttributesResult;
            }

            var approved = (await reqRepo.GetAccessRequestsByUserAsync(userId))
                .Where(r => r.Status == RequestStatus.Approved)
                .ToDictionary(r => r.DocumentId, r => r.Type);

            documentsAttributesResult.ForEach(d =>
                d.Access = approved.TryGetValue(d.DocumentId, out var t)
                    ? t
                    : AccessType.Blocked
            );

            return documentsAttributesResult;
        }

        public async Task<DocumentResultDto?> GetDocumentAsync(Guid documentId, Guid userId)
        {
            var document = await docRepo.GetDocumentAsync(documentId);
            if (document == null) return null;

            var user = await userRepo.GetUserAsync(userId)
                       ?? throw new UnauthorizedAccessException();

            AccessType access;
            if (user.Role == PersonRole.Approver || user.Role == PersonRole.Admin)
            {
                access = AccessType.Edit;
            }
            else
            {
                var req = (await reqRepo.GetAccessRequestsByUserAsync(userId))
                          .FirstOrDefault(r => r.DocumentId == documentId && r.Status == RequestStatus.Approved)
                       ?? throw new UnauthorizedAccessException();

                access = req.Type;
            }

            var documentResult = mapper.Map<DocumentResultDto>(document);
            documentResult.Access = access;

            return documentResult;
        }


        public async Task<ICollection<DocumentResultDto>> GetApprovedDocumentsAsync(Guid userId)
        {
            var user = await userRepo.GetUserAsync(userId)
                       ?? throw new UnauthorizedAccessException();

            var documents = await docRepo.GetDocumentsAsync();
            var documentsResult = mapper.Map<List<DocumentResultDto>>(documents);

            if (user.Role == PersonRole.Approver || user.Role == PersonRole.Admin)
            {
                documentsResult.ForEach(d => d.Access = AccessType.Edit);

                return documentsResult;
            }

            var approvedMap = (await reqRepo.GetAccessRequestsByUserAsync(userId))
                .Where(r => r.Status == RequestStatus.Approved)
                .ToDictionary(r => r.DocumentId, r => r.Type);

            return documentsResult
                .Where(d => approvedMap.ContainsKey(d.DocumentId))
                .Select(d =>
                {
                    d.Access = approvedMap[d.DocumentId];

                    return d;
                })
                .ToList();
        }
    }
}