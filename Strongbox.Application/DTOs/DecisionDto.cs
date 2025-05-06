using Strongbox.Domain.Entities;

namespace Strongbox.Application.DTOs
{
    public class DecisionDto
    {
        public Guid ApproverId { get; set; }
        public Guid AccessRequestId { get; set; }
        public RequestStatus Status { get; set; }
        public string Comment { get; set; } = default!;

    }
}