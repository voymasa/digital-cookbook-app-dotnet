using MealBrain.Data.Entities;

namespace MealBrain.Data.Repositories
{
    public interface IAccountRepository
    {
        string GetStatusMessage();
        //Create
        Task<int> InsertAccount(Account newAccount);

        //Read
        Task<Account?> GetAccountByGuid(Guid? guid);
        Task<Account?> GetAccountByFullnameAndEmail(string firstName, string lastName, string email);
        Task<List<Account>> GetAllAccounts();

        //Update
        Task<Account?> UpdateAccount(Account accountToUpdate);

        //Delete
        Task<bool> DeleteAccountByGuid(Guid guid);

        // For Account Type table calls
        Task<List<AccountType>> GetAllAccountTypes();
        Task<AccountType?> GetAccountTypeByName(string name);
    }
}
