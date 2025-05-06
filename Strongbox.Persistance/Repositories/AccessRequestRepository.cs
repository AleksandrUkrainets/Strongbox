using Microsoft.EntityFrameworkCore;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;

namespace Strongbox.Persistance.Repositories
{
    public class AccessRequestRepository(AppDbContext dbContext) : IAccessRequestRepository
    {
        public async Task<Guid> CreateAccessRequestAsync(AccessRequest accessRequest)
        {
            await dbContext.AccessRequests.AddAsync(accessRequest);
            await dbContext.SaveChangesAsync();
            return accessRequest.Id;
        }

        public async Task<AccessRequest?> GetAccessRequestAsync(Guid accessRequestId)
            => await dbContext.AccessRequests
                .Include(ar => ar.User)
                .Include(ar => ar.Document)
                .Include(ar => ar.Decision)
                .FirstOrDefaultAsync(ar => ar.Id == accessRequestId);

        public async Task<bool> UpdateAccessRequestAsync(AccessRequest accessRequest)
        {
            dbContext.AccessRequests.Update(accessRequest);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAccessRequestAsync(Guid accessRequestId)
        {
            var entity = await dbContext.AccessRequests.FindAsync(accessRequestId);
            if (entity == null) return false;
            dbContext.AccessRequests.Remove(entity);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<AccessRequest>> GetAccessRequestsAsync()
            => await dbContext.AccessRequests
                .Include(ar => ar.User)
                .Include(ar => ar.Document)
                .Include(ar => ar.Decision)
                .ToListAsync();

        public async Task<ICollection<AccessRequest>> GetAccessRequestsByUserAsync(Guid userId)
            => await dbContext.AccessRequests
                .Include(ar => ar.Document)
                .Include(ar => ar.Decision)
                .Where(ar => ar.UserId == userId)
                .ToListAsync();

        public async Task<ICollection<AccessRequest>> GetPendingAccessRequestsAsync()
            => await dbContext.AccessRequests
                .Include(ar => ar.User)
                .Include(ar => ar.Document)
                .Where(ar => ar.Status == RequestStatus.Pending)
                .ToListAsync();
    }
}
