using MealBrain.Data.Entities;
using SQLite;

namespace MealBrain.Data.Repositories
{
    /// <summary>
    /// The reasoning behind combining the Account and AccountType tables into one Repository class is due to a combination of
    /// factors including standardized use of Guids for Primary Keys, avoiding the need to have one repository class reference another or its service,
    /// and because the only table that references the AccountType lookup table is the Account table, so this becomes cohesive on
    /// Account information.
    /// </summary>
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(string dbPath) : base(dbPath) { }

        public Task<bool> DeleteAccountByGuid(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task<Account?> GetAccountByFullnameAndEmail(string firstName, string lastName, string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Account?> GetAccountByGuid(Guid? guid)
        {
            Account account;
            try
            {
                await Init();

                // we use FirstAsync here to leverage the no elements exception
                account = await _connection.Table<Account>().Where(acct => acct.Guid == guid).FirstAsync();
                StatusMessage = string.Format("Retrieved Account: {0}", account.DisplayName);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Unable to find Account. {0}", ex.Message);
                return null;
            }

            return account;
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();
            try
            {
                await Init();

                accounts = await _connection.Table<Account>().ToListAsync();
                StatusMessage = string.Format("Retrieved {0} accounts", accounts.Count);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Unable to retrieve accounts. {0}", ex.Message);
                return new List<Account>();
            }

            return accounts;
        }

        /// <summary>
        /// Insert a new account record and return the number of records inserted
        /// 
        /// </summary>
        /// <param name="newAccount"></param>
        /// <returns></returns>
        public async Task<int> InsertAccount(Account newAccount)
        {
            int recordsAdded = 0;
            try
            {
                await Init();

                recordsAdded = await _connection.InsertAsync(newAccount);
                if (recordsAdded == 0)
                    StatusMessage = string.Format("Failed to insert account {0} into the database.", newAccount);
                else if (recordsAdded != 1)
                    StatusMessage = string.Format("There was an issue with the inserted account: {0}", newAccount);
                else
                    StatusMessage = string.Format("Account inserted. {0}", newAccount);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("There was an issue with inserting the account. {0}", ex.Message);
            }

            return recordsAdded;
        }

        public Task<Account?> UpdateAccount(Account accountToUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountType?> GetAccountTypeByName(string name)
        {
            try
            {
                await Init();

                var account = await _connection.Table<AccountType>().Where(a => a.Name == name).FirstAsync();
                StatusMessage = string.Format("Retrieved account type {0}", account.Name);
                return account;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve account type with name {0}. {1}", name, ex.Message);
            }

            return null;
        }

        public async Task<List<AccountType>> GetAllAccountTypes()
        {
            try
            {
                await Init();

                var types = await _connection.Table<AccountType>().ToListAsync();
                StatusMessage = string.Format("Retrieved account types.");
                return types;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve account types. {0}", ex.Message);
            }

            return new List<AccountType>();
        }

        protected override async Task Init()
        {
            if (_connection is not null)
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine($"DB Path: {_dbPath}");

            // if it hasn't connected yet, establish the connection.
            _connection = new SQLiteAsyncConnection(_dbPath);
            await _connection.CreateTablesAsync<Account, AccountType>();
            await InitializeTables();
        }

        /// <summary>
        /// Initialize the database with data if the table is empty.
        /// This is in case when the app starts up that the connection
        /// hasn't been established but the database table already has data
        /// so we want to avoid duplicate data.
        /// </summary>
        /// <returns></returns>
        protected override async Task InitializeTables()
        {
            // create the default account types if the table is empty
            var currAcctTypes = await _connection.Table<AccountType>().ToListAsync();
            if (currAcctTypes.Count == 0)
            {
                List<AccountType> defaultTypes = new List<AccountType>
                {
                    new AccountType{ Guid = Guid.NewGuid(), Name = "Personal" },
                    new AccountType{ Guid = Guid.NewGuid(), Name = "Business" },
                };
                await _connection.InsertAllAsync(defaultTypes);
            }

            // insert a default personal user account if the table is empty
            var currentRecords = await _connection.Table<Account>().ToListAsync();
            if (currentRecords.Count == 0)
            {
                // get the Personal account type
                var paxAcctType = await _connection.Table<AccountType>().Where(acct => acct.Name.Equals("Personal")).FirstOrDefaultAsync();
                var defaultUserAccount = new Account
                {
                    Guid = Guid.NewGuid(),
                    DisplayName = "Default User",
                    Created = DateTime.Now,
                    AccountTypeGuid = paxAcctType.Guid,
                };
                await _connection.InsertAsync(defaultUserAccount);
            }
        }

        protected override async Task MigrateTables()
        {
            throw new NotImplementedException();
        }
    }
}
