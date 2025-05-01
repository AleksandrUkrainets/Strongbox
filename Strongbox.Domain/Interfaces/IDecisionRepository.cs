using Strongbox.Domain.Entities;

namespace Strongbox.Domain.Interfaces
{
    public interface IDecisionRepository
    {
        public Task<Guid?> CreateDecisionAsync(Decision decision);
        public Task<Decision?> GetDecisionAsync(Guid decisionId);
        public Task<bool> UpdateDecisionAsync(Decision decision);
        public Task<bool> DeleteDecisionAsync(Guid decisionId);
        public Task<ICollection<Decision>> GetDecisionsAsync();
    }
}