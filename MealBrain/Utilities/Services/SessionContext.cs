using MealBrain.Models;

namespace MealBrain.Utilities.Services
{
    /// <summary>
    /// This Service is to track the current user and store it for global access accross different views
    /// </summary>
    public class SessionContext : ISessionContext
    {
        public Account CurrentAccount { get; private set; }

        public void SetAccount(Account account)
        {
            CurrentAccount = account;
        }

        public void ClearAccount()
        {
            CurrentAccount = null;
        }

        public bool IsLoggedIn => CurrentAccount != null;
    }
}