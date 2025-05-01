using Strongbox.Domain.Entities;

namespace Strongbox.Application.DTOs
{
    public class DocumentAttributesResultDto
    {
        public Guid DocumentId { get; set; }
        public required string DocumentName { get; set; }
        public AccessType Access { get; set; }
    }

    public class DocumentResultDto : DocumentAttributesResultDto
    {
        public string? DocumentContent { get; set; }
    }
}
