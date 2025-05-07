using AutoMapper;
using Microsoft.AspNetCore.Http;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;
using System.Security.Claims;

namespace Strongbox.Application.Services
{
    public class AccessRequestService(IAccessRequestRepository reqRepo,
        IUserRepository userRepo,
        IDocumentRepository docRepo,
        IMapper mapper,
        IHttpContextAccessor httpCtx) : IAccessRequestService
    {
        private Guid CurrentUserId()
        {
            var sub = httpCtx.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                      ?? throw new UnauthorizedAccessException();
            return Guid.Parse(sub);
        }

        public async Task<Guid?> CreateAccessRequestAsync(AccessRequestDto dto)
        {
            var userId = CurrentUserId();
            var user = await userRepo.GetUserAsync(userId);
            if (user == null) return null;

            var doc = await docRepo.GetDocumentAsync(dto.DocumentId);
            if (doc == null) return null;

            var req = mapper.Map<AccessRequest>(dto);
            req.Id = Guid.NewGuid();
            req.Status = RequestStatus.Pending;
            req.CreatedAt = DateTime.UtcNow;
            req.UserId = userId;

            await reqRepo.CreateAccessRequestAsync(req);

            return req.Id;
        }

        public async Task<ICollection<AccessRequestResultDto>> GetMyRequestsAsync()
        {
            var userId = CurrentUserId();
            var list = await reqRepo.GetAccessRequestsByUserAsync(userId);

            return mapper.Map<List<AccessRequestResultDto>>(list);
        }

        public async Task<ICollection<AccessRequestResultDto>> GetPendingRequestsAsync()
        {
            var userId = CurrentUserId();
            var approver = await userRepo.GetUserAsync(userId);
            if (approver?.Role != PersonRole.Approver)
                throw new UnauthorizedAccessException();

            var list = await reqRepo.GetPendingAccessRequestsAsync();

            return mapper.Map<List<AccessRequestResultDto>>(list);
        }
    }
}
