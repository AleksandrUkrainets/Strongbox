namespace Strongbox.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Content { get; set; } = default!;
        public ICollection<AccessRequest> AccessRequests { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}