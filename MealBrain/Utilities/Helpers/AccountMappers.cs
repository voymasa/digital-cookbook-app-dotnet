using MealBrain.Models;
using MealBrain.Models.DTO;
using EntityAccount = MealBrain.Data.Entities.Account;
using ModelAccount = MealBrain.Models.Account;
using MealBrain.Utilities.Services;

namespace MealBrain.Utilities.Helpers
{
    public static class AccountMappers
    {
        public static void MapAccountDtoToModel(AccountDTO from, Account to)
        {
            to.AccountId = from.Guid;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.DisplayName = from.DisplayName;
            to.Email = from.Email;
            to.ImagePath = from.ImagePath;
            to.AccountType = from.AccountType;
        }

		public static void MapAccountModelToDto(Account from, AccountDTO to)
		{
			to.Guid = from.AccountId;
			to.FirstName = from.FirstName;
			to.LastName = from.LastName;
			to.DisplayName = from.DisplayName;
			to.Email = from.Email;
			to.ImagePath = from.ImagePath;
			to.AccountType = from.AccountType;
		}

        public static ModelAccount ToModel(this EntityAccount entity, IAccountService accountService)
        {
            return new ModelAccount(accountService)
            {
                AccountId = entity.Guid,
                DisplayName = entity.DisplayName,
                Email = entity.Email,
                ImagePath = entity.ImagePath,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
            };

        }
    }
}
