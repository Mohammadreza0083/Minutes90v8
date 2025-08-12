using Microsoft.AspNetCore.Identity;
using minutes90.Entities.Roles;
using System.ComponentModel.DataAnnotations;

namespace minutes90.Entities
{
    public class AppUsers : IdentityUser<int>
    {
        [MaxLength(100)]
        public required string DisplayName { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
    }
}
