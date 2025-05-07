using Microsoft.EntityFrameworkCore;
using Strongbox.Domain.Entities;

namespace Strongbox.Persistance
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AccessRequest> AccessRequests { get; set; }
        public DbSet<Decision> Decisions { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AccessRequest>()
                .HasOne(ar => ar.Decision)
                .WithOne(d => d.AccessRequest)
                .HasForeignKey<Decision>(d => d.AccessRequestId);

            modelBuilder.Entity<Decision>()
                .HasOne(d => d.Approver)
                .WithMany(u => u.Decisions)
                .HasForeignKey(d => d.ApproverId);



            // Seed 

            var doc1Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var doc2Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var doc3Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

            var createdAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc);


            modelBuilder.Entity<Document>().HasData(
                new Document { Id = doc1Id, Name = "Document 1", Content = "Content of Document 1", CreatedAt = createdAt },
                new Document { Id = doc2Id, Name = "Document 2", Content = "Content of Document 2", CreatedAt = createdAt },
                new Document { Id = doc3Id, Name = "Document 3", Content = "Content of Document 3", CreatedAt = createdAt }
            );
        }
    }
}
