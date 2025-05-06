using Strongbox.Application.DTOs;

namespace Strongbox.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthResponseDto?> RegisterAsync(RegisterDto dto);
        public Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    }
}
