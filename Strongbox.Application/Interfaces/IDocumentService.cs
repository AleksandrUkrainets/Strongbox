using Strongbox.Application.DTOs;

namespace Strongbox.Application.Interfaces
{
    public interface IDocumentService
    {
        public Task<DocumentResultDto?> GetDocumentAsync(Guid documentId, Guid userId);
        public Task<ICollection<DocumentResultDto>> GetApprovedDocumentsAsync(Guid userId);
        public Task<ICollection<DocumentAttributesResultDto>> GetDocumentsAttributesAsync(Guid userId);
    }
}
