using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Strongbox.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

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
            var approverId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var approverSalt = Convert.FromBase64String("w+p9YXePwGmaMDIdYWovvi8zMGuH8451L1bAyLudVMUMrmW2k/q0NQADvgnjP9Gxnaa2MkN4AdJB8zox5h3iRnCdv7o1MBtGNlYLq7AnzMw7Kzgr+NI+O4wCfG7uj3DIpBqQYpSYnq8uFLfie4ePQI0xrukWIURMIjf31cNC/Rc=");
            var approverHash = Convert.FromBase64String("3Gvdlr48le8sDEqVnQJlVh2vj6CR1ZMupBnYPMgcszv4OqpYTByhQIOFa59W1amSNdEEy4oBzHknyLqJ0aC4Ag==");

            var doc1Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var doc2Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var doc3Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

            var createdAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = approverId,
                    Name = "Approver User",
                    Role = PersonRole.Approver,
                    Username = "ApproverUser",
                    PasswordSalt = approverSalt,
                    PasswordHash = approverHash,
                }
            );


            modelBuilder.Entity<Document>().HasData(
                new Document { Id = doc1Id, Name = "Document 1", Content = "Content of Document 1", CreatedAt = createdAt },
                new Document { Id = doc2Id, Name = "Document 2", Content = "Content of Document 2", CreatedAt = createdAt },
                new Document { Id = doc3Id, Name = "Document 3", Content = "Content of Document 3", CreatedAt = createdAt }
            );
        }
    }
}
