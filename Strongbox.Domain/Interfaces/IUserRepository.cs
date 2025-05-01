using Strongbox.Domain.Entities;

namespace Strongbox.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<Guid?> CreateUserAsync(User user);
        public Task<User?> GetUserAsync(Guid userId);
        public Task<bool> UpdateUserAsync(User user);
        public Task<bool> DeleteUserAsync(Guid userId);
        public Task<ICollection<User>> GetUsersAsync();
    }
}
