using Strongbox.Application.DTOs;

namespace Strongbox.Application.Interfaces
{
    public interface IAccessRequestService
    {
        public Task<Guid?> CreateAccessRequestAsync(AccessRequestDto accessRequest);
        public Task<AccessRequestResultDto?> GetAccessRequestAsync(Guid accessRequestId, Guid approverId);
        public Task<ICollection<AccessRequestResultDto>> GetAccessRequestsAsync(Guid approverId);
    }

}
