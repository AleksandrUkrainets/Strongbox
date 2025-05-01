using Microsoft.EntityFrameworkCore;
using Strongbox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // Seed 
            var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var approverId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            var doc1Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var doc2Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var doc3Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

            var createdAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<User>().HasData(
                new User { Id = userId, Name = "Regular User", Role = PersonRole.User },
                new User { Id = approverId, Name = "Approver User", Role = PersonRole.Approver }
            );


            modelBuilder.Entity<Document>().HasData(
                new Document { Id = doc1Id, Name = "Document 1", Content = "Content of Document 1", CreatedAt = createdAt },
                new Document { Id = doc2Id, Name = "Document 2", Content = "Content of Document 2", CreatedAt = createdAt },
                new Document { Id = doc3Id, Name = "Document 3", Content = "Content of Document 3", CreatedAt = createdAt }
            );
        }
    }
}
