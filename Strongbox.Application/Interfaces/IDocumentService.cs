using Strongbox.Application.DTOs;

namespace Strongbox.Application.Interfaces
{
    public interface IDocumentService
    {
        public Task<DocumentResultDto?> GetDocumentAsync(Guid documentId, Guid userId);
        public Task<bool> UpdateDocumentAsync(DocumentDto document);
        public Task<ICollection<DocumentResultDto>> GetDocumentsAsync(Guid userId);
        public Task<ICollection<DocumentAttributesResultDto>> GetDocumentsAttributesAsync(Guid userId);
    }
}
