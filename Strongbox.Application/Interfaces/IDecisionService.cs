using Strongbox.Application.DTOs;

namespace Strongbox.Application.Interfaces
{
    public interface IDecisionService
    {
        public Task<Guid?> CreateDecisionAsync(DecisionDto decisionDto);
        public Task<DecisionResultDto?> GetDecisionAsync(Guid decisionId, Guid approverId);
        public Task<bool> UpdateDecisionAsync(Guid decisionId, DecisionDto decisionDto);
        public Task<ICollection<DecisionResultDto>> GetDecisionsAsync(Guid approverId);
    }
}
