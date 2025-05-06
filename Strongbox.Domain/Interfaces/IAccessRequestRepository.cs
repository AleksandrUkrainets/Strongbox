using Strongbox.Domain.Entities;

namespace Strongbox.Domain.Interfaces
{
    public interface IAccessRequestRepository
    {
        public Task<Guid> CreateAccessRequestAsync(AccessRequest accessRequest);
        public Task<AccessRequest?> GetAccessRequestAsync(Guid accessRequestId);
        public Task<bool> UpdateAccessRequestAsync(AccessRequest accessRequest);
        public Task<bool> DeleteAccessRequestAsync(Guid accessRequestId);
        public Task<ICollection<AccessRequest>> GetAccessRequestsAsync();
        public Task<ICollection<AccessRequest>> GetAccessRequestsByUserAsync(Guid userId);
        public Task<ICollection<AccessRequest>> GetPendingAccessRequestsAsync();
    }
}
