using Microsoft.EntityFrameworkCore;
using NetApiStarterApp.Data;
using NetApiStarterApp.Models.Account;
using System.Globalization;

namespace NetApiStarterApp.Repository.Account
{
    public class AccountService : IAccountService
    {
        private readonly DataConnection _context;

        public AccountService(DataConnection context)
        {
            _context = context;
        }

        public async Task<List<AccountModel>> GetAccountListAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<AccountModel> GetAccountByIdAsync(Guid accountId)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountId == accountId);

            if (account == null)
            {
                // Handle the case where the account is not found
                throw new InvalidOperationException("Account not found");
            }

            return account;
        }

        public async Task<AccountDetailsModel> GetAccountDetailsByIdAsync(Guid accountId)
        {
            var account = await _context.AccountDetails.FirstOrDefaultAsync(x => x.AccountId == accountId);

            if (account == null)
            {
                // Handle the case where the account detail is not found
                throw new InvalidOperationException("Account details not found");
            }

            return account;
        }

        public async Task<AccountModel> AddAccountAsync(AddAccountDto addAccountDto)
        {
            var accountModel = new AccountModel
            {
                AccountId = Guid.NewGuid(),
                Email = addAccountDto.Email,
                NormalizedEmail = addAccountDto.Email.ToUpper(),
                Username = addAccountDto.Username,
                NormalizedUsername = addAccountDto.Username.ToUpper(),
                Password = BCrypt.Net.BCrypt.HashPassword(addAccountDto.Password),
                CreatedAt = DateTime.UtcNow,
                // Set other properties from addAccountDto
            };

            _context.Accounts.Add(accountModel);
            await _context.SaveChangesAsync();

            //create account details
            InitiateAccountDetails(accountModel.AccountId);

            return accountModel;
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

            _context.AccountDetails.Add(accountDetails);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<AccountModel> UpdateAccountAsync(UpdateAccountDto updateAccountDto)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x=> x.AccountId == updateAccountDto.AccountId);
            //TODO mapper

            if (account != null)
            {
                // Update properties from updateAccountDto
                account.PhoneNumber = updateAccountDto.PhoneNumber;
                account.IsVerified = updateAccountDto.IsVerified;
                account.IsEmailVerified = updateAccountDto.IsEmailVerified;
                account.IsPhoneVerified = updateAccountDto.IsPhoneVerified;
                account.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            else
            {
                account = new AccountModel();
            }

            return account;
        }

        public async Task<AccountDetailsModel> UpdateAccountDetailsAsync(UpdateAccountDetailsDto updateAccountDetailsDto)
        {
            var accountDetails = await _context.AccountDetails.FirstOrDefaultAsync(x=> x.AccountId == updateAccountDetailsDto.AccountId);
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
                await _context.SaveChangesAsync();
            }
            else
            {
                accountDetails = new AccountDetailsModel();
            }

            return accountDetails;
        }

        public async Task<bool> DeleteAccountAsync(Guid accountId)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountId == accountId);
            var accountDetails = await _context.AccountDetails.FirstOrDefaultAsync(x => x.AccountId == accountId);

            if (account != null && accountDetails != null)
            {
                //remove account
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();

                //remove account details
                _context.AccountDetails.Remove(accountDetails);
                await _context.SaveChangesAsync();

                return true;
            }


            return false;
        }

        public async Task<string?> GetAccountData(Guid accountId, string returnColumn)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountId == accountId);

            if (account != null)
            {
                // Use reflection to get the value of the specified property
                var property = typeof(AccountModel).GetProperty(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(returnColumn));
                return property?.GetValue(account)?.ToString();
            }

            return null;
        }

    }
}
