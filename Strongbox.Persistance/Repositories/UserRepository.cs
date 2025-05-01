using Microsoft.EntityFrameworkCore;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;

namespace Strongbox.Persistance.Repositories
{
    public class UserRepository(AppDbContext dbContext) : IUserRepository
    {
        public async Task<User?> GetUserAsync(Guid userId)
        {
            return await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        Task<Guid?> IUserRepository.CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        Task<bool> IUserRepository.DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        Task<ICollection<User>> IUserRepository.GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        Task<bool> IUserRepository.UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }

}
