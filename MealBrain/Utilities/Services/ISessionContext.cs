using MealBrain.Models;

namespace MealBrain.Utilities.Services
{
    public interface ISessionContext
    {
        Account CurrentAccount { get; }
        void SetAccount(Account account);
        void ClearAccount();
        bool IsLoggedIn { get; }
    }
}