namespace Strongbox.Domain.Entities
{
    public class AccessRequest
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid DocumentId { get; set; }
        public Document? Document { get; set; }


        public Decision? Decision { get; set; }

        public required string Reason { get; set; }
        public required AccessType Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public RequestStatus Status { get; set; } = RequestStatus.Pending;

    }
}