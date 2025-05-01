using Microsoft.EntityFrameworkCore;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;

namespace Strongbox.Persistance.Repositories
{
    public class DocumentRepository(AppDbContext appDbContext) : IDocumentRepository
    {
        public async Task<Document?> GetDocumentAsync(Guid documentId)
        {

            return await appDbContext.Documents
                .Include(d => d.AccessRequests)
                .FirstOrDefaultAsync(d => d.Id == documentId);
        }

        public async Task<ICollection<Document>> GetDocumentsAsync()
        {

            return await appDbContext.Documents
                .Include(d => d.AccessRequests)
                .ToListAsync();
        }

        public async Task<bool> UpdateDocumentAsync(Document document)
        {
            appDbContext.Documents
                .Update(document);

            return await appDbContext.SaveChangesAsync() > 0;
        }

        public Task<Guid?> CreateDocumentAsync(Document document)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDocumentAsync(Guid documentId)
        {
            throw new NotImplementedException();
        }

    }
}
