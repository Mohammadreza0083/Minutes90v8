using minutes90.Entities;

namespace minutes90.Interfaces
{
    public interface ITokenServices
    {
        Task<string?> CreateToken(AppUsers user);
    }
}
