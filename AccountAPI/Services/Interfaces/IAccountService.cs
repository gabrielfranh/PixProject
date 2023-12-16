using AccountAPI.DTOs;

namespace AccountAPI.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDTO> GetAccountByAccountId(int accountId);
    }
}
