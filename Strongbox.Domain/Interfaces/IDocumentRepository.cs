using Strongbox.Domain.Entities;

namespace Strongbox.Domain.Interfaces
{
    public interface IDocumentRepository
    {
        public Task<Guid?> CreateDocumentAsync(Document document);
        public Task<Document?> GetDocumentAsync(Guid documentId);
        public Task<bool> UpdateDocumentAsync(Document document);
        public Task<bool> DeleteDocumentAsync(Guid documentId);
        public Task<ICollection<Document>> GetDocumentsAsync();
    }
}
