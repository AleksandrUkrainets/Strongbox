using Strongbox.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strongbox.Application.Interfaces
{
    public interface IDecisionService
    {
        public Task<Guid?> CreateDecisionAsync(DecisionDto decision);
        public Task<DecisionResultDto?> GetDecisionAsync(Guid decisionId, Guid approverId);
        public Task<DecisionResultDto?> UpdateDecisionAsync(DecisionDto decision);
        public Task<ICollection<DecisionResultDto>> GetApproverDecisionsAsync(Guid approverId);
    }
}
