using NetApiStarterApp.Models.Account;

namespace NetApiStarterApp.Repository.Account
{
    public interface IAccountService
    {
        public Task<List<AccountModel>> GetAccountListAsync(); //get list of all account 
        public Task<AccountModel> GetAccountByIdAsync(Guid accountId); //gets account by vehicle id
        public Task<AccountDetailsModel> GetAccountDetailsByIdAsync(Guid accountId); //gets account by vehicle id
        public Task<AccountModel> AddAccountAsync(AddAccountDto addAccountDto); //adds new account data
        public Task<bool> InitiateAccountDetails(Guid accountId); //adds a new account details record using accountId for relation
        public Task<AccountModel> UpdateAccountAsync(UpdateAccountDto ubpdateAccountDto); // update account data
        public Task<AccountDetailsModel> UpdateAccountDetailsAsync(UpdateAccountDetailsDto ubpdateAccountDetailsDto); // update account details data 
        public Task<bool> DeleteAccountAsync(Guid accountId);  //removes vehicle by vehicleId
        public Task<string?> GetAccountData(Guid accountId, string returnColumn); //takes Account id and a returnColumn as parameters and returns the data.
    }
}
