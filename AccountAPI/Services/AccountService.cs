using AccountAPI.DTOs;
using AccountAPI.Repositories.Interfaces;
using AccountAPI.Services.Interfaces;

namespace AccountAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountDTO> GetAccountByAccountId(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}
