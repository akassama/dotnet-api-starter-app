namespace NetApiStarterApp.Models.Account
{
    public class LoginModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
