using Strongbox.Domain.Entities;

namespace Strongbox.Application.DTOs
{
    public class AccessRequestResultDto
    {
        public Guid AccessRequestId { get; set; }
        public Guid UserId { get; set; }
        public required string UserName { get; set; }
        public Guid DocumentId { get; set; }
        public required string DocumentName { get; set; }
        public required string Reason { get; set; }
        public required AccessType Type { get; set; }
        public RequestStatus Status { get; set; }
    }
}