namespace minutes90v8.Dto
{
    public class LoginDto
    {
        public required string UsernameOrEmail { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
