using AutoMapper;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;

namespace Strongbox.Application.Services
{
    public class DecisionService(IDecisionRepository decisionRepo, IAccessRequestRepository reqRepo, IUserRepository userRepo, IMapper mapper) : IDecisionService
    {
        public async Task<Guid?> CreateDecisionAsync(DecisionDto dto)
        {
            var approver = await userRepo.GetUserAsync(dto.ApproverId);
            if (approver == null || approver.Role != PersonRole.Approver)
                throw new UnauthorizedAccessException();

            var request = await reqRepo.GetAccessRequestAsync(dto.AccessRequestId);
            if (request == null) return null;

            request.Status = dto.Status;
            await reqRepo.UpdateAccessRequestAsync(request);

            var decision = mapper.Map<Decision>(dto);
            decision.Id = Guid.NewGuid();

            return await decisionRepo.CreateDecisionAsync(decision);
        }

        public async Task<ICollection<DecisionResultDto>> GetDecisionsAsync(Guid approverId)
        {
            var approver = await userRepo.GetUserAsync(approverId);
            if (approver == null || approver.Role != PersonRole.Approver)
                throw new UnauthorizedAccessException();

            var decisions = await decisionRepo.GetAllDecisionsAsync();

            return mapper.Map<List<DecisionResultDto>>(decisions);
        }

        public async Task<DecisionResultDto?> GetDecisionAsync(Guid decisionId, Guid approverId)
        {
            var approver = await userRepo.GetUserAsync(approverId);
            if (approver == null || approver.Role != PersonRole.Approver)
                throw new UnauthorizedAccessException();

            var decision = await decisionRepo.GetDecisionAsync(decisionId);
            if (decision == null) return null;

            return mapper.Map<DecisionResultDto>(decision);
        }

        public async Task<bool> UpdateDecisionAsync(Guid decisionId, DecisionDto dto)
        {
            var approver = await userRepo.GetUserAsync(dto.ApproverId);
            if (approver == null || approver.Role != PersonRole.Approver)
                throw new UnauthorizedAccessException();

            var existing = await decisionRepo.GetDecisionAsync(decisionId);
            if (existing == null) return false;

            mapper.Map(dto, existing);
            var updated = await decisionRepo.UpdateDecisionAsync(existing);

            var request = await reqRepo.GetAccessRequestAsync(existing.AccessRequestId);
            if (request != null)
            {
                request.Status = existing.Status;
                await reqRepo.UpdateAccessRequestAsync(request);
            }

            return updated;
        }
    }
}
