using Microsoft.EntityFrameworkCore;
using NetApiStarterApp.Data;
using NetApiStarterApp.Models.Account;
using System.Globalization;
using System.Security.Cryptography;

namespace NetApiStarterApp.Repository.Account
{
    public class AccountService : IAccountService
    {
        private readonly DataConnection _dbContext;

        public AccountService(DataConnection context)
        {
            _dbContext = context;
        }

        public async Task<List<AccountModel>> GetAccountListAsync()
        {
            return await _dbContext.Accounts.ToListAsync();
        }

        public async Task<List<AccountViewModel>> GetAccountDetailsListAsync()
        {
            var accountsWithDetails = await _dbContext.Accounts
                .Join(
                    _dbContext.AccountDetails,
                    account => account.AccountId,
                    details => details.AccountId,
                    (account, details) => new { Account = account, Details = details }
                )
                .ToListAsync();

            var accountViewModels = accountsWithDetails.Select(result =>
                new AccountViewModel
                {
                    AccountId = result.Account.AccountId,
                    Username = result.Account.Username,
                    Email = result.Account.Email,
                    PhoneNumber = result.Account.PhoneNumber,
                    IsVerified = result.Account.IsVerified,
                    IsEmailVerified = result.Account.IsEmailVerified,
                    IsPhoneVerified = result.Account.IsPhoneVerified,
                    IsLocked = result.Account.IsLocked,
                    LockedUntil = result.Account.LockedUntil,
                    LastLogin = result.Account.LastLogin,
                    CreatedAt = result.Account.CreatedAt,
                    AccountDetailsId = result.Details.AccountDetailsId,
                    DateOfBirth = result.Details.DateOfBirth,
                    Age = CalculateAge(result.Details.DateOfBirth), // Calculate age using a separate method
                    Gender = result.Details.Gender,
                    Address = result.Details.Address,
                    City = result.Details.City,
                    Country = result.Details.Country,
                    ProfilePicture = result.Details.ProfilePicture,
                    EmergencyContactName = result.Details.EmergencyContactName,
                    EmergencyContactPhoneNumber = result.Details.EmergencyContactPhoneNumber,
                    Occupation = result.Details.Occupation,
                    Company = result.Details.Company,
                    SecurityQuestionOne = result.Details.SecurityQuestionOne,
                    SecurityAnswerOne = result.Details.SecurityAnswerOne,
                    SecurityQuestionTwo = result.Details.SecurityQuestionTwo,
                    SecurityAnswerTwo = result.Details.SecurityAnswerTwo,
                    AboutInfo = result.Details.AboutIfo,
                    AdditionalNotes = result.Details.AdditionalNotes,
                    UpdatedAt = result.Details.UpdatedAt
                })
                .ToList();

            return accountViewModels;
        }

        // Helper method to calculate age
        private int CalculateAge(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }


        public async Task<AccountModel> GetAccountByIdAsync(Guid accountId)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.AccountId == accountId);

            if (account == null)
            {
                // Handle the case where the account is not found
                throw new InvalidOperationException("Account not found");
            }

            return account;
        }

        public async Task<AccountDetailsModel> GetAccountDetailsByIdAsync(Guid accountId)
        {
            var account = await _dbContext.AccountDetails.FirstOrDefaultAsync(x => x.AccountId == accountId);

            if (account == null)
            {
                // Handle the case where the account detail is not found
                throw new InvalidOperationException("Account details not found");
            }

            return account;
        }

        public async Task<AccountModel> AddAccountAsync(AddAccountDto addAccountDto)
        {
            string passwordSalt = GenerateSalt();
            var accountModel = new AccountModel
            {
                AccountId = Guid.NewGuid(),
                Email = addAccountDto.Email,
                NormalizedEmail = addAccountDto.Email.ToUpper(),
                Username = addAccountDto.Username,
                NormalizedUsername = addAccountDto.Username.ToUpper(),
                PasswordSalt = passwordSalt,
                Password = BCrypt.Net.BCrypt.HashPassword($"{passwordSalt}{addAccountDto.Password}"),
                CreatedAt = DateTime.UtcNow,
                // Set other properties from addAccountDto
            };

            _dbContext.Accounts.Add(accountModel);
            await _dbContext.SaveChangesAsync();

            //create account details
            InitiateAccountDetails(accountModel.AccountId);

            return accountModel;
        }

        private string GenerateSalt(int size = 32)
        {
            byte[] saltBytes = new byte[size];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }

            // Convert the byte array to a hex string
            string salt = BitConverter.ToString(saltBytes).Replace("-", "").ToLower();

            return salt;
        }

        public async Task<bool> InitiateAccountDetails(Guid accountId)
        {
            var accountDetails = new AccountDetailsModel
            {
                AccountDetailsId = Guid.NewGuid(),
                AccountId = accountId,
                CreatedAt = DateTime.UtcNow,
                // Set other properties as needed
            };

            _dbContext.AccountDetails.Add(accountDetails);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<AccountModel> UpdateAccountAsync(UpdateAccountDto updateAccountDto)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(x=> x.AccountId == updateAccountDto.AccountId);
            //TODO mapper

            if (account != null)
            {
                // Update properties from updateAccountDto
                account.PhoneNumber = updateAccountDto.PhoneNumber;
                account.IsVerified = updateAccountDto.IsVerified;
                account.IsEmailVerified = updateAccountDto.IsEmailVerified;
                account.IsPhoneVerified = updateAccountDto.IsPhoneVerified;
                account.UpdatedAt = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                account = new AccountModel();
            }

            return account;
        }

        public async Task<AccountDetailsModel> UpdateAccountDetailsAsync(UpdateAccountDetailsDto updateAccountDetailsDto)
        {
            var accountDetails = await _dbContext.AccountDetails.FirstOrDefaultAsync(x=> x.AccountId == updateAccountDetailsDto.AccountId);
            //TODO mapper

            if (accountDetails != null)
            {
                // Update properties from updateAccountDetailsDto
                accountDetails.DateOfBirth = updateAccountDetailsDto.DateOfBirth;
                accountDetails.Gender = updateAccountDetailsDto.Gender;
                accountDetails.Address = updateAccountDetailsDto.Address;
                accountDetails.City = updateAccountDetailsDto.City;
                accountDetails.Country = updateAccountDetailsDto.Country;
                accountDetails.ProfilePicture = updateAccountDetailsDto.ProfilePicture;
                accountDetails.EmergencyContactName = updateAccountDetailsDto.EmergencyContactName;
                accountDetails.EmergencyContactPhoneNumber = updateAccountDetailsDto.EmergencyContactPhoneNumber;
                accountDetails.Occupation = updateAccountDetailsDto.Occupation;
                accountDetails.Company = updateAccountDetailsDto.Company;
                accountDetails.SecurityQuestionOne = updateAccountDetailsDto.SecurityQuestionOne;
                accountDetails.SecurityAnswerOne = updateAccountDetailsDto.SecurityAnswerOne;
                accountDetails.SecurityQuestionTwo = updateAccountDetailsDto.SecurityQuestionTwo;
                accountDetails.SecurityAnswerTwo = updateAccountDetailsDto.SecurityAnswerTwo;
                accountDetails.AboutIfo = updateAccountDetailsDto.AboutIfo;
                accountDetails.AdditionalNotes = updateAccountDetailsDto.AdditionalNotes;
                accountDetails.UpdatedAt = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                accountDetails = new AccountDetailsModel();
            }

            return accountDetails;
        }

        public async Task<bool> DeleteAccountAsync(Guid accountId)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.AccountId == accountId);
            var accountDetails = await _dbContext.AccountDetails.FirstOrDefaultAsync(x => x.AccountId == accountId);

            if (account != null && accountDetails != null)
            {
                //remove account
                _dbContext.Accounts.Remove(account);
                await _dbContext.SaveChangesAsync();

                //remove account details
                _dbContext.AccountDetails.Remove(accountDetails);
                await _dbContext.SaveChangesAsync();

                return true;
            }


            return false;
        }

        public async Task<string?> GetAccountData(Guid accountId, string returnColumn)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.AccountId == accountId);

            if (account != null)
            {
                // Use reflection to get the value of the specified property
                var property = typeof(AccountModel).GetProperty(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(returnColumn));
                return property?.GetValue(account)?.ToString();
            }

            return null;
        }


        public async Task<bool> ValidateLogin(LoginModel loginModel)
        {
            // Find the user by email in the database
            var user = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Email == loginModel.Email);

            // If the user is not found, or the account is locked, return false
            if (user == null || user.IsLocked)
            {
                return false;
            }

            // Use BCrypt to verify the entered password against the hashed password stored in the database
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify($"{user.PasswordSalt}{loginModel.Password}", user.Password);

            if (isPasswordValid)
            {
                // Update last login timestamp or any other relevant logic
                user.LastLogin = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
            }

            return isPasswordValid;
        }

    }
}
