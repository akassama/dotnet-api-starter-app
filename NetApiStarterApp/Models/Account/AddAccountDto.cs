namespace NetApiStarterApp.Models.Account
{
    public class AddAccountDto
    {
        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}
