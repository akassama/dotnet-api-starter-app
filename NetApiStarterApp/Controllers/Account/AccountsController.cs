using Microsoft.AspNetCore.Mvc;
using NetApiStarterApp.Models.Account;
using NetApiStarterApp.Repository.Account;

namespace NetApiStarterApp.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AddAccountDto accountRequest)
        {
            var data = await _accountService.AddAccountAsync(accountRequest);

            return Ok(data);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AddAccountDto accountRequest)
        {
            var data = await _accountService.AddAccountAsync(accountRequest);

            return Ok(data);
        }

        [HttpGet("get-accounts")]
        public async Task<IActionResult> GetAccounts()
        {
            var data = await _accountService.GetAccountListAsync();

            return Ok(data);
        }

        [HttpGet("get-account")]
        public async Task<IActionResult> GetAccount(Guid accountId)
        {
            var data = await _accountService.GetAccountByIdAsync(accountId);

            return Ok(data);
        }

        [HttpGet("get-account-details")]
        public async Task<IActionResult> GetAccountDetails(Guid accountId)
        {
            var data = await _accountService.GetAccountDetailsByIdAsync(accountId);

            return Ok(data);
        }


        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string code, string email)
        {
            var data = true;

            return Ok(data);
        }

        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ConfirmationEmail()
        {
            var data = true;

            return Ok(data);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword()
        {
            var data = true;

            return Ok(data);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword()
        {
            var data = true;

            return Ok(data);
        }

        [HttpPost("update-account")]
        public async Task<IActionResult> UpdateAccount(UpdateAccountDto accountRequest)
        {
            var data = await _accountService.UpdateAccountAsync(accountRequest);

            return Ok(data);
        }

        [HttpPost("update-account-details")]
        public async Task<IActionResult> UpdateAccountDetails(UpdateAccountDetailsDto accountDetailsRequest)
        {
            var data = await _accountService.UpdateAccountDetailsAsync(accountDetailsRequest);

            return Ok(data);
        }

        [HttpPost("remove-account")]
        public async Task<IActionResult> RemoveAccount(Guid accountId)
        {
            var data = await _accountService.DeleteAccountAsync(accountId);

            return Ok(data);
        }

        [HttpPost("get-account-data")]
        public async Task<IActionResult> GetAccountData(Guid accountId, string returnColumn)
        {
            var data = await _accountService.GetAccountData(accountId, returnColumn);

            return Ok(data);
        }
    }
}
