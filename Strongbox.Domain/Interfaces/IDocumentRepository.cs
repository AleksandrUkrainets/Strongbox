using Strongbox.Domain.Entities;

namespace Strongbox.Domain.Interfaces
{
    public interface IDocumentRepository
    {
        public Task<Document?> GetDocumentAsync(Guid documentId);
        public Task<ICollection<Document>> GetDocumentsAsync();
    }
}
