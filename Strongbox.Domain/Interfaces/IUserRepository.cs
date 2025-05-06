using Strongbox.Domain.Entities;

namespace Strongbox.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> CreateUserAsync(User user, string plainPassword);
        public Task<User?> GetUserAsync(Guid userId);
        public Task<User?> GetByUsernameAsync(string username);
        public Task<bool> UpdateUserAsync(User user);
        public Task<bool> DeleteUserAsync(Guid userId);
        public Task<ICollection<User>> GetUsersAsync();
    }
}
