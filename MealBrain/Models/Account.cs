using MealBrain.Models.DTO;
using MealBrain.Utilities.Services;

namespace MealBrain.Models
{
    public class Account
    {
        // service dependencies
        private IAccountService _accountService;

        // variables for all of the information for the view
        public Guid? AccountId { get; set; } // this variable is intended for when an account is selected and that unique user's info needs to be retrieved or the user needs to be updated; don't set this to try and make a new account
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string AccountType { get; set; } = "Personal";

        // variable for holding the existing accounts;
        // should be updated with the latest whenever the list of accounts is needed
        // such as for displaying them on a user selection screen
        public List<AccountDTO> SelectableAccounts { get; set; } = new List<AccountDTO>();

        // variable to handle notification
        public bool WasSuccess { get; set; }
        public string NotificationMessage { get; set; } = string.Empty;

        public Account(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// This method calls to the account service to create a new account
        /// and then sets the returned CrudResult to properties so that the
        /// view model can bind them to the view for notification purposes.
        /// </summary>
        public async Task CreateNewAccount()
        {
            // create an account dto with the model's information
            var accountDto = MapModelToAccountDto();

            var result = await _accountService.CreateAccount(accountDto);
            NotificationMessage = result.ResultMessage;
            WasSuccess = result.IsSuccess;
            // if the result has data map it to the information for this account
            if (result.Data is not null)
            {
                MapAccountDtoToModel(result.Data);
            }
        }

        /// <summary>
        /// This method calls to the account service to ask for a specific
        /// account by the Guid (AccountId) and then sets the other properties
        /// </summary>
        public async Task GetAccountByUniqueId()
        {
            var result = await _accountService.GetAccountById(AccountId);
            NotificationMessage = result.ResultMessage;
            WasSuccess = result.IsSuccess;
            if (result.Data is not null)
            {
                MapAccountDtoToModel(result.Data);
            }
        }

        public async Task GetAvailableAccounts()
        {
            var result = await _accountService.GetAllLocalAccounts();
            NotificationMessage = result.ResultMessage;
            //WasSuccess = result.IsSuccess; // if we figure out a good way to determine "success" when it always returns a list, even if empty, we should then enable this line
            if (result.Data is not null) // this should always be true because this method should always return a list, even if empty
                SelectableAccounts = result.Data;
        }

        private AccountDTO MapModelToAccountDto()
        {
            var dto = new AccountDTO
            {
                Guid = this.AccountId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                DisplayName = this.DisplayName,
                Email = this.Email,
                ImagePath = this.ImagePath,
                AccountType = this.AccountType,
            };

            return dto;
        }

        private void MapAccountDtoToModel(AccountDTO dto)
        {
            this.AccountId = dto.Guid;
            this.FirstName = dto.FirstName;
            this.LastName = dto.LastName;
            this.DisplayName = dto.DisplayName;
            this.Email = dto.Email;
            this.ImagePath = dto.ImagePath;
            this.AccountType = dto.AccountType;
        }
    }
}
