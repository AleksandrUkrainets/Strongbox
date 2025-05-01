using AutoMapper;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;

namespace Strongbox.Application.Services
{
    public class DecisionService(IDecisionRepository decisionRepository, IAccessRequestRepository accessRequestRepository, IUserRepository userRepository, IMapper mapper) : IDecisionService
    {
        public async Task<Guid?> CreateDecisionAsync(DecisionDto decisionDto)
        {
            var approver = await userRepository.GetUserAsync(decisionDto.ApproverId);
            if (approver == null || approver.Role == PersonRole.User) return null;

            var accessRequest = await accessRequestRepository.GetAccessRequestAsync(decisionDto.AccessRequestId);
            if (accessRequest == null || accessRequest.Status != RequestStatus.Pending) return null;

            var decision = mapper.Map<Decision>(decisionDto);
            decision.Id = Guid.NewGuid();
            decision.CreatedAt = DateTime.UtcNow;

            accessRequest.Status = decision.IsApproved ? RequestStatus.Approved : RequestStatus.Rejected;

            return await decisionRepository.CreateDecisionAsync(decision);
        }

        public async Task<DecisionResultDto?> GetDecisionAsync(Guid decisionId, Guid approverId)
        {
            var approver = await userRepository.GetUserAsync(approverId);
            if (approver == null || approver.Role == PersonRole.User) return null;

            var decision = await decisionRepository.GetDecisionAsync(decisionId);
            if (decision == null || decision.ApproverId != approverId) return null;

            return mapper.Map<DecisionResultDto>(decision);
        }

        public async Task<ICollection<DecisionResultDto>> GetDecisionsAsync(Guid approverId)
        {
            var approver = await userRepository.GetUserAsync(approverId);
            if (approver == null || approver.Role == PersonRole.User) return [];

            var decisions = await decisionRepository.GetDecisionsAsync();

            return mapper.Map<ICollection<DecisionResultDto>>(decisions);
        }

        public async Task<bool> UpdateDecisionAsync(Guid decisionId, DecisionDto decisionDto)
        {
            var approver = await userRepository.GetUserAsync(decisionDto.ApproverId);
            if (approver == null || approver.Role == PersonRole.User) return false;

            var existing = await decisionRepository.GetDecisionAsync(decisionId);
            if (existing == null || existing.ApproverId != decisionDto.ApproverId) return false;

            existing.Comment = decisionDto.Comment;
            existing.IsApproved = decisionDto.IsApproved;
            existing.CreatedAt = DateTime.UtcNow;

            return await decisionRepository.UpdateDecisionAsync(existing);
        }
    }

}
