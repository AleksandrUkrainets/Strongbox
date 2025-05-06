using Microsoft.EntityFrameworkCore;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;

namespace Strongbox.Persistance.Repositories
{
    public class DecisionRepository(AppDbContext dbContext) : IDecisionRepository
    {
        public async Task<Guid> CreateDecisionAsync(Decision decision)
        {
            await dbContext.Decisions.AddAsync(decision);
            await dbContext.SaveChangesAsync();
            return decision.Id;
        }

        public async Task<Decision?> GetDecisionAsync(Guid decisionId)
            => await dbContext.Decisions
                .Include(d => d.AccessRequest)
                    .ThenInclude(ar => ar.User)
                .FirstOrDefaultAsync(d => d.Id == decisionId);

        public async Task<ICollection<Decision>> GetAllDecisionsAsync()
            => await dbContext.Decisions
                .Include(d => d.AccessRequest)
                    .ThenInclude(ar => ar.User)
                .ToListAsync();

        public async Task<bool> UpdateDecisionAsync(Decision decision)
        {
            dbContext.Decisions.Update(decision);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
