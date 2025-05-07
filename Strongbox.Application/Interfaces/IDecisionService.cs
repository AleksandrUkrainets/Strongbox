using Strongbox.Application.DTOs;

namespace Strongbox.Application.Interfaces
{
    public interface IDecisionService
    {
        public Task<Guid?> CreateDecisionAsync(Guid approverId, DecisionDto decisionDto);
        public Task<DecisionResultDto?> GetDecisionAsync(Guid decisionId);
        public Task<bool> UpdateDecisionAsync(Guid decisionId, DecisionDto decisionDto);
        public Task<ICollection<DecisionResultDto>> GetDecisionsAsync();
    }
}
