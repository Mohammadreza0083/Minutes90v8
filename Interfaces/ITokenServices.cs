using minutes90v8.Entities;

namespace minutes90v8.Interfaces
{
    public interface ITokenServices
    {
        Task<string?> CreateToken(AppUsers user);
    }
}
