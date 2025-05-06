using Microsoft.EntityFrameworkCore;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;

namespace Strongbox.Persistance.Repositories
{
    public class DocumentRepository(AppDbContext dbContext) : IDocumentRepository
    {
        public async Task<Document?> GetDocumentAsync(Guid documentId)
            => await dbContext.Documents
                .Include(d => d.AccessRequests)
                    .ThenInclude(ar => ar.Decision)
                .FirstOrDefaultAsync(d => d.Id == documentId);

        public async Task<ICollection<Document>> GetDocumentsAsync()
            => await dbContext.Documents
                .Include(d => d.AccessRequests)
                    .ThenInclude(ar => ar.Decision)
                .ToListAsync();
    }
}
