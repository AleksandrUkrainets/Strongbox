using Strongbox.Domain.Entities;

namespace Strongbox.Application.DTOs
{
    public class ChangeUserRoleDto
    {
        public Guid UserId { get; set; }
        public PersonRole Role { get; set; }
    }
}
