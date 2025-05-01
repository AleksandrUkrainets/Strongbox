using AutoMapper;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;

namespace Strongbox.Application.Services
{
    public class AccessRequestService(IAccessRequestRepository accessRequestRepository, IUserRepository userRepository, IMapper mapper, IDocumentRepository documentRepository) : IAccessRequestService
    {
        public async Task<Guid?> CreateAccessRequestAsync(AccessRequestDto accessRequestDto)
        {
            var user = await userRepository.GetUserAsync(accessRequestDto.UserId);
            if (user == null) return null;

            var document = await documentRepository.GetDocumentAsync(accessRequestDto.DocumentId);
            if (document == null) return null;

            var request = mapper.Map<AccessRequest>(accessRequestDto);
            request.Id = Guid.NewGuid();
            request.Status = RequestStatus.Pending;
            request.CreatedAt = DateTime.UtcNow;
            request.Document = document;

            return await accessRequestRepository.CreateAccessRequestAsync(request);
        }

        public async Task<AccessRequestResultDto?> GetAccessRequestAsync(Guid accessRequestId, Guid approverId)
        {
            var user = await userRepository.GetUserAsync(approverId);
            if (user == null || user.Role == PersonRole.User) return null;

            var request = await accessRequestRepository.GetAccessRequestAsync(accessRequestId);
            if (request == null) return null;

            return mapper.Map<AccessRequestResultDto>(request);
        }

        public async Task<ICollection<AccessRequestResultDto>> GetAccessRequestsAsync(Guid approverId)
        {
            var user = await userRepository.GetUserAsync(approverId);
            if (user == null || user.Role == PersonRole.User) return [];

            var requests = await accessRequestRepository.GetAccessRequestsAsync();
            return mapper.Map<ICollection<AccessRequestResultDto>>(requests);
        }
    }

}
