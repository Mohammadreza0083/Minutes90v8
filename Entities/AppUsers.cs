using Microsoft.AspNetCore.Identity;
using minutes90v8.Entities.Roles;
using System.ComponentModel.DataAnnotations;

namespace minutes90v8.Entities
{
    public class AppUsers : IdentityUser<int>
    {
        [MaxLength(100)]
        public required string DisplayName { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
    }
}
