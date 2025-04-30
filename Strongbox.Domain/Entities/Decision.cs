namespace Strongbox.Domain.Entities
{
    public class Decision
    {
        public Guid Id { get; set; }

        public Guid ApproverId { get; set; }
        public User? Approver { get; set; }

        public Guid AccessRequestId { get; set; }
        public AccessRequest? AccessRequest { get; set; }

        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Comment { get; set; } = default!;
    }
}