using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using minutes90v8.Dto;
using minutes90v8.Interfaces;

namespace minutes90v8.Controllers
{
    public class AccountController(
    IAccountServices accountService,
    ILogger<AccountController> logger) : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (result, userDto) = await accountService.LoginAsync(loginDto);
            if (!result.Succeeded)
            {
                logger.LogWarning("Login failed for user {UsernameOrEmail}: {Error}", 
                    loginDto.UsernameOrEmail, result.ToString());
                return Unauthorized(result.IsLockedOut ? "Account is locked." : "Invalid credentials.");
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
            };
            if (loginDto.RememberMe)
            {
                cookieOptions.Expires = DateTime.UtcNow.AddDays(30);
            }
            if (userDto == null) 
            {
                logger.LogWarning("Login failed for user {UsernameOrEmail}: UserDto is null", loginDto.UsernameOrEmail);
                return Unauthorized("Login failed. Please check your credentials."); 
            }
            if (userDto.Token != null) Response.Cookies.Append("access_token", userDto.Token, cookieOptions);
            userDto.Token = null;
            return Ok(userDto);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await accountService.RegisterAsync(registerDto);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok("Registration successful.");
        }
    }
}
