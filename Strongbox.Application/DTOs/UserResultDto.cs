using Strongbox.Domain.Entities;

namespace Strongbox.Application.DTOs
{
    public class UserResultDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public PersonRole Role { get; set; }
    }
}
