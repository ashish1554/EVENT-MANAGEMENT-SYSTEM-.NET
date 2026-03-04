using EVENT_MANAGEMENT_SYSTEM.DTOs;
using EVENT_MANAGEMENT_SYSTEM.Models;

namespace EVENT_MANAGEMENT_SYSTEM.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponseDto> Register(RegisterDto user);
        Task<string> Login(LoginDto user);
    }
}
