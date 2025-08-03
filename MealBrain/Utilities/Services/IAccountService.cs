using MealBrain.Models.DTO;
using MealBrain.Utilities.ServiceResults;

namespace MealBrain.Utilities.Services
{
    public interface IAccountService
    {
        Task<CrudResult<AccountDTO>> GetAccountById(Guid? id);
        Task<CrudResult<List<AccountDTO>>> GetAllLocalAccounts();
        Task<CrudResult<AccountDTO>> CreateAccount(AccountDTO account);
    }
}
