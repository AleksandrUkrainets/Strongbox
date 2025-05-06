using AutoMapper;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;

namespace Strongbox.Application.Services
{
    public class AccessRequestService(IAccessRequestRepository reqRepo, IUserRepository userRepo, IDocumentRepository docRepo, IMapper mapper) : IAccessRequestService
    {
        public async Task<Guid?> CreateAccessRequestAsync(AccessRequestDto dto)
        {
            var user = await userRepo.GetUserAsync(dto.UserId);
            if (user == null) return null;

            var doc = await docRepo.GetDocumentAsync(dto.DocumentId);
            if (doc == null) return null;

            var req = mapper.Map<AccessRequest>(dto);
            req.Id = Guid.NewGuid();
            req.Status = RequestStatus.Pending;
            req.CreatedAt = DateTime.UtcNow;

            await reqRepo.CreateAccessRequestAsync(req);

            return req.Id;
        }

        public async Task<ICollection<AccessRequestResultDto>> GetMyRequestsAsync(Guid userId)
        {
            var list = await reqRepo.GetAccessRequestsByUserAsync(userId);

            return mapper.Map<List<AccessRequestResultDto>>(list);
        }

        public async Task<ICollection<AccessRequestResultDto>> GetPendingRequestsAsync(Guid approverId)
        {
            var approver = await userRepo.GetUserAsync(approverId);
            if (approver == null || approver.Role != PersonRole.Approver)
                throw new UnauthorizedAccessException();

            var list = await reqRepo.GetPendingAccessRequestsAsync();

            return mapper.Map<List<AccessRequestResultDto>>(list);
        }
    }
}
