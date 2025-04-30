using Strongbox.Application.DTOs;

namespace Strongbox.Application.Interfaces
{
    public interface IAccessRequestService
    {
        public Task<Guid?> CreateAccessRequestAsync(AccessRequestDto accessRequest);
        public Task<AccessRequestResultDto?> GetAccessRequestAsync(Guid accessRequestId, Guid userId);
        public Task<ICollection<AccessRequestResultDto>> GetUserAccessRequestsAsync(Guid userId);
        public Task<ICollection<AccessRequestResultDto>> GetPendingAccessRequestsAsync(Guid approverId);
    }
}
