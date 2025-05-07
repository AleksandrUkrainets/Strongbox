using Strongbox.Application.DTOs;

namespace Strongbox.Application.Interfaces
{
    public interface IDocumentService
    {
        public Task<DocumentResultDto?> GetDocumentAsync(Guid documentId);
        public Task<ICollection<DocumentResultDto>> GetApprovedDocumentsAsync();
        public Task<ICollection<DocumentAttributesResultDto>> GetDocumentsAttributesAsync();
    }
}
