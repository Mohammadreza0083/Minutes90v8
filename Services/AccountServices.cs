using AutoMapper;
using Microsoft.AspNetCore.Identity;
using minutes90v8.Dto;
using minutes90v8.Entities;
using minutes90v8.Interfaces;

namespace minutes90v8.Services
{
    public class AccountServices: IAccountServices
    {
        private readonly UserManager<AppUsers> userManager;
        private readonly SignInManager<AppUsers> signInManager;
        private readonly ITokenServices tokenServices;
        private readonly IMapper mapper;
        private readonly IUnitOfWorkRepo repo;
        public AccountServices(
            UserManager<AppUsers> userManager,
            SignInManager<AppUsers> signInManager,
            ITokenServices tokenServices,
            IMapper mapper,
            IUnitOfWorkRepo repo)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenServices = tokenServices;
            this.mapper = mapper;
            this.repo = repo;
        }
        public async Task<(SignInResult, UserDto?)> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.UsernameOrEmail)
                       ?? await userManager.FindByEmailAsync(loginDto.UsernameOrEmail);

            if (user == null) return (SignInResult.Failed, null);

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                await repo.Complete();
                return (SignInResult.LockedOut, null);
            }

            if (!result.Succeeded)
            {
                await repo.Complete();
                return (result, null);
            }

            await userManager.ResetAccessFailedCountAsync(user);
            await repo.Complete();

            var userDto = mapper.Map<UserDto>(user);
            userDto.Token = await tokenServices.CreateToken(user);

            return (SignInResult.Success, userDto);
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
        {
            var user = mapper.Map<AppUsers>(registerDto);
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return result;
            await userManager.AddToRoleAsync(user, "User");
            return IdentityResult.Success;
        }
    }
}
