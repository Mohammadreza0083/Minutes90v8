using System.ComponentModel.DataAnnotations;

namespace minutes90.Dto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        [StringLength(20, MinimumLength = 8)]
        [RegularExpression(
            "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,15}$",
            ErrorMessage = "Password must have 1 uppercase, 1 lowercase, 1 number, " +
                           "and at least 8 characters"
        )]
        public required string Password { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Username must be at least 4 characters long.")]
        public required string Username { get; set; }

        [Required]
        public required string DisplayName { get; set; }
    }
}
