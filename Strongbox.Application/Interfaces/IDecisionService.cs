using Strongbox.Application.DTOs;

namespace Strongbox.Application.Interfaces
{
    public interface IDecisionService
    {
        public Task<Guid?> CreateDecisionAsync(DecisionDto decision);
        public Task<DecisionResultDto?> GetDecisionAsync(Guid decisionId, Guid approverId);
        public Task<bool> UpdateDecisionAsync(Guid decisionId, DecisionDto decision);
        public Task<ICollection<DecisionResultDto>> GetDecisionsAsync(Guid approverId);
    }
}
