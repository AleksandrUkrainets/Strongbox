using AutoMapper;
using Microsoft.AspNetCore.Http;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;
using System.Security.Claims;

namespace Strongbox.Application.Services
{
    public class DocumentService(IDocumentRepository docRepo, IAccessRequestRepository reqRepo, IMapper mapper, IHttpContextAccessor httpCtx) : IDocumentService
    {
        private Guid GetCurrentUserId()
        {
            var sub = httpCtx.HttpContext?.User
                      .FindFirst(ClaimTypes.NameIdentifier)?.Value
                  ?? throw new UnauthorizedAccessException();
            return Guid.Parse(sub);
        }
        private PersonRole GetCurrentUserRole()
        {
            var role = httpCtx.HttpContext?.User
                       .FindFirst(ClaimTypes.Role)?.Value
                   ?? throw new UnauthorizedAccessException();
            return Enum.Parse<PersonRole>(role);
        }

        public async Task<ICollection<DocumentAttributesResultDto>> GetDocumentsAttributesAsync()
        {
            var userId = GetCurrentUserId();
            var role = GetCurrentUserRole();

            var documentsAttributesResult = mapper.Map<List<DocumentAttributesResultDto>>(await docRepo.GetDocumentsAsync());

            if (role == PersonRole.Approver || role == PersonRole.Admin)
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

        public async Task<DocumentResultDto?> GetDocumentAsync(Guid documentId)
        {
            var userId = GetCurrentUserId();
            var role = GetCurrentUserRole();
            var document = await docRepo.GetDocumentAsync(documentId);
            if (document == null) return null;


            AccessType access;
            if (role == PersonRole.Approver || role == PersonRole.Admin)
            {
                access = AccessType.Edit;
            }
            else
            {
                var request = (await reqRepo.GetAccessRequestsByUserAsync(userId))
                          .FirstOrDefault(r => r.DocumentId == documentId && r.Status == RequestStatus.Approved)
                       ?? throw new UnauthorizedAccessException();

                access = request.Type;
            }

            var documentResult = mapper.Map<DocumentResultDto>(document);
            documentResult.Access = access;

            return documentResult;
        }

        public async Task<ICollection<DocumentResultDto>> GetApprovedDocumentsAsync()
        {
            var userId = GetCurrentUserId();
            var role = GetCurrentUserRole();

            var documents = await docRepo.GetDocumentsAsync();
            var documentsResult = mapper.Map<List<DocumentResultDto>>(documents);

            if (role == PersonRole.Approver || role == PersonRole.Admin)
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