using Microsoft.AspNetCore.Identity;
using minutes90.Dto;

namespace minutes90.Interfaces
{
    public interface IAccountServices
    {
        Task<(SignInResult, UserDto?)> LoginAsync(LoginDto loginDto);
        Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
    }
}
