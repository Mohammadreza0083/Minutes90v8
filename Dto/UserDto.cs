namespace minutes90.Dto
{
    public class UserDto
    {
        public required string DisplayName { get; set; }
        public required string? UserName { get; set; }
        public required string? Email { get; set; }
        public required string? Token { get; set; }
    }
}
