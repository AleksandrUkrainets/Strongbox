using AutoMapper;
using Microsoft.Extensions.Configuration;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Strongbox.Application.Services
{
    public class AuthService(IUserRepository userRepo, IMapper mapper, IConfiguration config) : IAuthService
    {
        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
        {
            if (await userRepo.GetByUsernameAsync(dto.Username) != null)
                return null;

            var user = mapper.Map<User>(dto);
            user.Id = Guid.NewGuid();

            var created = await userRepo.CreateUserAsync(user, dto.Password);
            if (!created) return null;

            return GenerateToken(user);
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await userRepo.GetByUsernameAsync(dto.Username);
            if (user == null) return null;

            using var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            for (int i = 0; i < hash.Length; i++)
                if (hash[i] != user.PasswordHash[i])
                    return null;

            return GenerateToken(user);
        }

        private AuthResponseDto GenerateToken(User user)
        {
            var jwtKey = config["Jwt:Key"]!;
            var issuer = config["Jwt:Issuer"]!;
            var audience = config["Jwt:Audience"]!;
            var expiresIn = int.Parse(config["Jwt:ExpiresInMinutes"]!);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(expiresIn),
                signingCredentials: creds);

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = token.ValidTo
            };
        }
    }
}
