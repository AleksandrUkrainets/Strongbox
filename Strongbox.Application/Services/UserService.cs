using AutoMapper;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;
using Strongbox.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Strongbox.Application.Services
{
    public class UserService(IUserRepository userRepo, IMapper mapper) : IUserService
    {
        public async Task<ICollection<UserResultDto>> GetAllUsersAsync()
        {
            var users = await userRepo.GetUsersAsync();

            return mapper.Map<List<UserResultDto>>(users);
        }

        public async Task<bool> ChangeUserRoleAsync(ChangeUserRoleDto dto)
        {
            var user = await userRepo.GetUserAsync(dto.UserId);
            if (user == null) return false;

            user.Role = dto.Role;

            return await userRepo.UpdateUserAsync(user);
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var user = await userRepo.GetByUsernameAsync(dto.Username);
            if (user == null) return false;

            using var hmacVerify = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmacVerify.ComputeHash(Encoding.UTF8.GetBytes(dto.OldPassword));
            if (!computedHash.SequenceEqual(user.PasswordHash)) return false;

            using var hmacNew = new HMACSHA512();
            user.PasswordSalt = hmacNew.Key;
            user.PasswordHash = hmacNew.ComputeHash(Encoding.UTF8.GetBytes(dto.NewPassword));

            return await userRepo.UpdateUserAsync(user);
        }
    }
}
