using Strongbox.Domain.Entities;

namespace Strongbox.Domain.Interfaces
{
    public interface IAccessRequestRepository
    {
        public Task<Guid> CreateAccessRequest(AccessRequest accessRequest);
        public Task<AccessRequest> GetAccessRequest(Guid accessRequestId);
        public Task<AccessRequest> UpdateAccessRequest(AccessRequest accessRequest);
        public Task<bool> DeleteAccessRequest(Guid accessRequestId);
        public Task<ICollection<AccessRequest>> GetAccessRequests();
    }
}
