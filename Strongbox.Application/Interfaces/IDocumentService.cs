using Strongbox.Application.DTOs;
using Strongbox.Domain.Entities;

namespace Strongbox.Application.Interfaces
{
    public interface IDocumentService
    {
        public Task<DocumentResultDto?> GetDocumentAsync(Guid userId, PersonRole role, Guid documentId);
        public Task<ICollection<DocumentResultDto>> GetApprovedDocumentsAsync(Guid userId, PersonRole role);
        public Task<ICollection<DocumentAttributesResultDto>> GetDocumentsAttributesAsync(Guid userId, PersonRole role);
    }
}