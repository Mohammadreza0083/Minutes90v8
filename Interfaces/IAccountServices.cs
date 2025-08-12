using Microsoft.AspNetCore.Identity;
using minutes90v8.Dto;

namespace minutes90v8.Interfaces
{
    public interface IAccountServices
    {
        Task<(SignInResult, UserDto?)> LoginAsync(LoginDto loginDto);
        Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
    }
}
