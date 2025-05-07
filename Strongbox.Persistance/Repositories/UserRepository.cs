using Microsoft.EntityFrameworkCore;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;
using System.Security.Cryptography;

namespace Strongbox.Persistance.Repositories
{
    public class UserRepository(AppDbContext dbContext) : IUserRepository
    {
        public async Task<User?> GetByUsernameAsync(string username) =>
            await dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);

        public async Task<User?> GetUserAsync(Guid userId) =>
            await dbContext.Users.FindAsync(userId);

        public async Task<bool> CreateUserAsync(User user, string plainPassword)
        {
            using var hmac = new HMACSHA512();
            user.PasswordSalt = hmac.Key;
            user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(plainPassword));

            await dbContext.Users.AddAsync(user);

            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<User>> GetUsersAsync() =>
            await dbContext.Users.ToListAsync();

        public async Task<bool> UpdateUserAsync(User user)
        {
            dbContext.Users.Update(user);

            return await dbContext.SaveChangesAsync() > 0;
        }

        Task<bool> IUserRepository.DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }

}
