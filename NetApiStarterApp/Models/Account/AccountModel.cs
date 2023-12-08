using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetApiStarterApp.Models.Account
{
    [Table("Accounts")]
    public class AccountModel
    {
        [Key]
        public Guid AccountId { get; set; }

        public string? Username { get; set; }
        public string? NormalizedUsername { get; set; }

        public string? Email { get; set; }

        public string? NormalizedEmail { get; set; }

        public string? Password { get; set; }

        public string? PasswordSalt { get; set; }

        public string? SecurityStamp { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsVerified { get; set; }

        public bool IsEmailVerified { get; set; }

        public bool IsPhoneVerified { get; set; }

        public bool IsLocked { get; set; }

        public DateTime? LockedUntil { get; set; }

        public DateTime? LastLogin { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
