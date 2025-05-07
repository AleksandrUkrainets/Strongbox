using Strongbox.Application.DTOs;

namespace Strongbox.Application.Interfaces
{
    public interface IAccessRequestService
    {
        public Task<Guid?> CreateAccessRequestAsync(AccessRequestDto accessRequestDto);
        public Task<ICollection<AccessRequestResultDto>> GetMyRequestsAsync();
        public Task<ICollection<AccessRequestResultDto>> GetPendingRequestsAsync();
    }

}
