using AccountAPI.DTOs;
using AccountAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<AccountDTO>> GetAccountByAccountId([FromRoute] int accountId)
        {
            var account = await _accountService.GetAccountByAccountId(accountId);

            if (account is null)
                return NotFound(accountId);

            return Ok(account);
        }
    }
}
