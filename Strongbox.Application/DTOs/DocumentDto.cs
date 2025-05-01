namespace Strongbox.Application.DTOs
{
    public class DocumentDto
    {
        public Guid DocumentId { get; set; }
        public Guid UserId { get; set; }
        public required string DocumentContent { get; set; }
    }
}
