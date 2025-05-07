using AutoMapper;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;

namespace Strongbox.Application.Services
{
    public class DocumentService(IDocumentRepository docRepo, IAccessRequestRepository reqRepo, IMapper mapper) : IDocumentService
    {
        public async Task<ICollection<DocumentAttributesResultDto>> GetDocumentsAttributesAsync(Guid userId, PersonRole role)
        {
            var dtos = mapper.Map<List<DocumentAttributesResultDto>>(await docRepo.GetDocumentsAsync());

            if (role is PersonRole.Approver or PersonRole.Admin)
            {
                dtos.ForEach(d => d.Access = AccessType.Edit);

                return dtos;
            }

            var approved = (await reqRepo.GetAccessRequestsByUserAsync(userId))
                .Where(r => r.Status == RequestStatus.Approved)
                .ToDictionary(r => r.DocumentId, r => r.Type);

            dtos.ForEach(d =>
                d.Access = approved.TryGetValue(d.DocumentId, out var t)
                    ? t
                    : AccessType.Blocked
            );

            return dtos;
        }

        public async Task<DocumentResultDto?> GetDocumentAsync(Guid userId, PersonRole role, Guid documentId)
        {
            var document = await docRepo.GetDocumentAsync(documentId);
            if (document == null) return null;

            AccessType access;
            if (role is PersonRole.Approver or PersonRole.Admin)
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

            var result = mapper.Map<DocumentResultDto>(document);
            result.Access = access;

            return result;
        }

        public async Task<ICollection<DocumentResultDto>> GetApprovedDocumentsAsync(Guid userId, PersonRole role)
        {
            var dtos = mapper.Map<List<DocumentResultDto>>(await docRepo.GetDocumentsAsync());

            if (role is PersonRole.Approver or PersonRole.Admin)
            {
                dtos.ForEach(d => d.Access = AccessType.Edit);

                return dtos;
            }

            var approvedMap = (await reqRepo.GetAccessRequestsByUserAsync(userId))
                .Where(r => r.Status == RequestStatus.Approved)
                .ToDictionary(r => r.DocumentId, r => r.Type);

            return dtos
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