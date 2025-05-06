using Strongbox.Domain.Entities;

namespace Strongbox.Application.DTOs
{
    public class DecisionResultDto
    {
        public Guid DecisionId { get; set; }
        public Guid ApproverId { get; set; }
        public Guid AccessRequestId { get; set; }
        public string ApproverName { get; set; } = default!;
        public RequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Comment { get; set; } = default!;
    }
}