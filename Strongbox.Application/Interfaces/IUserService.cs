using Strongbox.Application.DTOs;

namespace Strongbox.Application.Interfaces
{
    public interface IUserService
    {
        public Task<ICollection<UserResultDto>> GetAllUsersAsync();
        public Task<bool> ChangeUserRoleAsync(ChangeUserRoleDto dto);
        public Task<bool> ChangePasswordAsync(ChangePasswordDto dto);
    }
}
