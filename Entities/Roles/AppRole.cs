using Microsoft.AspNetCore.Identity;

namespace minutes90.Entities.Roles
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
    }
}
