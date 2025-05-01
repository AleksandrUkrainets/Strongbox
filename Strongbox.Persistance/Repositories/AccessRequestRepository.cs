using Microsoft.EntityFrameworkCore;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strongbox.Persistance.Repositories
{
    public class AccessRequestRepository(AppDbContext dbContext) : IAccessRequestRepository
    {
        public async Task<Guid> CreateAccessRequestAsync(AccessRequest request)
        {
            await dbContext.AccessRequests.AddAsync(request);
            await dbContext.SaveChangesAsync();

            return request.Id;
        }

        public async Task<AccessRequest?> GetAccessRequestAsync(Guid accessRequestId)
        {
            return await dbContext.AccessRequests
                .Include(a => a.User)
                .Include(a => a.Document)
                .Include(a => a.Decision)
                .FirstOrDefaultAsync(a => a.Id == accessRequestId);
        }

        public async Task<ICollection<AccessRequest>> GetAccessRequestsAsync()
        {
            return await dbContext.AccessRequests
                .Include(a => a.User)
                .Include(a => a.Document)
                .Include(a => a.Decision)
                .ToListAsync();
        }


        public Task<bool> DeleteAccessRequestAsync(Guid accessRequestId)
        {
            throw new NotImplementedException();
        }


        public Task<bool> UpdateAccessRequestAsync(AccessRequest accessRequest)
        {
            throw new NotImplementedException();
        }
    }

}
