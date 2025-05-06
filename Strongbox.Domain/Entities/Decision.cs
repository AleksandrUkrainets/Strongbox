namespace Strongbox.Domain.Entities
{
    public class Decision
    {
        public Guid Id { get; set; }

        public Guid ApproverId { get; set; }
        public User Approver { get; set; } = null!;

        public Guid AccessRequestId { get; set; }
        public AccessRequest AccessRequest { get; set; } = null!;

        public RequestStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Comment { get; set; } = default!;
    }
}