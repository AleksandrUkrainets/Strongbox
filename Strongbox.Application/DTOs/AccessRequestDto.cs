using Strongbox.Domain.Entities;

namespace Strongbox.Application.DTOs
{
    public class AccessRequestDto
    {
        public Guid UserId { get; set; }
        public Guid DocumentId { get; set; }
        public required string Reason { get; set; }
        public required AccessType Type { get; set; }

    }
}