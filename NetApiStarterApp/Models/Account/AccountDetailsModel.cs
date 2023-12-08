using System.ComponentModel.DataAnnotations;

namespace NetApiStarterApp.Models.Account
{
    public class AccountDetailsModel
    {
        [Key]
        public Guid AccountDetailsId { get; set; }

        public Guid AccountId { get; set; }

        public DateTime DateOfBirth { get; set; }

        // Computed Age (using a read-only property)
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

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

        public string? AboutIfo { get; set; }

        public string? AdditionalNotes { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum GenderEnum
    {
        Male,
        Female,
        Other
    }

}
