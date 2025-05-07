using AutoMapper;
using Microsoft.AspNetCore.Http;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;
using System.Security.Claims;

namespace Strongbox.Application.Services
{
    public class AccessRequestService(IAccessRequestRepository reqRepo, IDocumentRepository docRepo, IMapper mapper) : IAccessRequestService
    {
        public async Task<Guid?> CreateAccessRequestAsync(Guid userId, AccessRequestDto dto)
        {
            var doc = await docRepo.GetDocumentAsync(dto.DocumentId);
            if (doc == null) return null;

            var req = mapper.Map<AccessRequest>(dto);
            req.Id = Guid.NewGuid();
            req.UserId = userId;
            req.Status = RequestStatus.Pending;
            req.CreatedAt = DateTime.UtcNow;

            var newId = await reqRepo.CreateAccessRequestAsync(req);

            return newId;
        }

        public async Task<ICollection<AccessRequestResultDto>> GetMyRequestsAsync(Guid userId)
        {
            var list = await reqRepo.GetAccessRequestsByUserAsync(userId);

            return mapper.Map<List<AccessRequestResultDto>>(list);
        }

        public async Task<ICollection<AccessRequestResultDto>> GetPendingRequestsAsync()
        {
            var list = await reqRepo.GetPendingAccessRequestsAsync();

            return mapper.Map<List<AccessRequestResultDto>>(list);
        }
    }
}
