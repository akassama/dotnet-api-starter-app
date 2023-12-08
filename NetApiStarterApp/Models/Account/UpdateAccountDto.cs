namespace NetApiStarterApp.Models.Account
{
    public class UpdateAccountDto
    {
        public Guid AccountId { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsVerified { get; set; }

        public bool IsEmailVerified { get; set; }

        public bool IsPhoneVerified { get; set; }
    }
}
