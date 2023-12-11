using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetApiStarterApp.Models.Account
{
    [NotMapped]
    public class AccountViewModel
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

        public DateTime CreatedAt { get; set; }

        // AccountDetailsModel properties
        public Guid AccountDetailsId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public GenderEnum Gender { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? ProfilePicture { get; set; }

        public string? EmergencyContactName { get; set; }

        public string? EmergencyContactPhoneNumber { get; set; }

        public string? Occupation { get; set; }

        public string? Company { get; set; }

        public string? SecurityQuestionOne { get; set; }

        public string? SecurityAnswerOne { get; set; }

        public string? SecurityQuestionTwo { get; set; }

        public string? SecurityAnswerTwo { get; set; }

        public string? AboutInfo { get; set; }

        public string? AdditionalNotes { get; set; }
    }

}
