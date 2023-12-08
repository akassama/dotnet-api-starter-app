namespace NetApiStarterApp.Models.Account
{
    public class UpdateAccountDetailsDto
    {
        public Guid AccountId { get; set; }

        public DateTime DateOfBirth { get; set; }

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
    }

}
