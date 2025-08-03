using MealBrain.Data.Repositories;
using MealBrain.Models.DTO;
using MealBrain.Utilities.ServiceResults;

namespace MealBrain.Utilities.Services
{
    /// <summary>
    /// The Account Service handles logic related to User Account information, whether from the database
    /// or in the business logic. Its primary focus at its inception is brokering between the Account-related MVVM (specifically the model),
    /// and the AccountRepository which deals with the Account Entity in the database.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

		/// <summary>
		/// Method to handle interacting with the repository and database to create a new user account
		/// This should return whether the result was a success or failure as well as the status message
		/// of the transaction, to the model.
		/// </summary>
		/// <param name="account"></param>
		/// <returns>An account result object with the success or failure and status message.</returns>
		public async Task<CrudResult<AccountDTO>> CreateAccount(AccountDTO account)
		{
			// Regardless of return path we are returning a CrudResult
			// so we are going to generate at least one CrudResult in the scope
			// of this method. Since we are, create it at the top and just
			// fill in the information at time of use.
			// This should aid in readability
			var queryResult = new CrudResult<AccountDTO>();
			// convert from the account model to an account entity with a newly generated guid
			var newAccountEntity = MapDtoToEntity(account);
			
			// convert the account type from the model to the correct lookup value from accounttype
			var accountType = await _accountRepository.GetAccountTypeByName(account.AccountType);
			if (accountType is null)
			{
				// hmmmmm if we fail to get the account type we should fail account creation because it won't have a
				// proper GUID for the account type "lookup"
				queryResult.IsSuccess = false;
				queryResult.ResultMessage = _accountRepository.GetStatusMessage();
				return queryResult;
			}
			newAccountEntity.AccountTypeGuid = accountType.Guid;
			newAccountEntity.Created = DateTime.UtcNow; // timestampz is set here so that the value is returned on success

            var result = await _accountRepository.InsertAccount(newAccountEntity);

			if (result != 1) // both 0 and > 1 will be true here, but have different status messages, so we capture both failures once
			{
				// if we fail to insert the account we want to notify the user
				queryResult.IsSuccess = false;
				queryResult.ResultMessage = _accountRepository.GetStatusMessage();
				return queryResult;
			}

			// when the account is successfully created notify the user and provide
			// them with the inserted account information via a dto
			queryResult.IsSuccess = true;
			queryResult.ResultMessage = _accountRepository.GetStatusMessage();
			queryResult.Data = MapEntityToDto(newAccountEntity); // this is the entity rather than the original DTO because it now contains the guid and timestamps

            return queryResult;
        }

        /// <summary>
        /// This method handles requesting the account information from the database
        /// based upon the supplied guid.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CrudResult<AccountDTO>> GetAccountById(Guid? id)
        {
            var queryResult = new CrudResult<AccountDTO>();

			if (id is null)
			{
				queryResult.IsSuccess = false;
				queryResult.ResultMessage = "Missing id";
				return queryResult;
			}

            // get the account with the given id
            var result = await _accountRepository.GetAccountByGuid(id);

			if (result is null)
			{
				queryResult.IsSuccess = false;
				queryResult.ResultMessage = _accountRepository.GetStatusMessage();
				return queryResult;
			}

			queryResult.IsSuccess = true;
			queryResult.ResultMessage = _accountRepository.GetStatusMessage();
			queryResult.Data = MapEntityToDto(result);

            return queryResult;
        }

        /// <summary>
        /// This method handles the reuquest to get all of the local accounts from the
        /// database and return dtos to the caller.
        /// </summary>
        /// <returns></returns>
        public async Task<CrudResult<List<AccountDTO>>> GetAllLocalAccounts()
        {
            var queryResult = new CrudResult<List<AccountDTO>>();

            var result = await _accountRepository.GetAllAccounts();

			queryResult.IsSuccess = false;
			queryResult.ResultMessage = _accountRepository.GetStatusMessage();
			queryResult.Data = MapEntitiesToDtos(result);

            return queryResult;
        }

        /// <summary>
        /// This method converts the data in the entity object to an account dto
        /// that can be passed back to the model
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private AccountDTO MapEntityToDto(Data.Entities.Account entity)
        {
            var dto = new AccountDTO
            {
                Guid = entity.Guid,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DisplayName = entity.DisplayName,
                Email = entity.Email,
                ImagePath = entity.ImagePath,
                Created = entity.Created,
                Modified = entity.Modified,
            };

            return dto;
        }

        /// <summary>
        /// This method is to convert from the data account data transfer object to
        /// an entity object to use with the database.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private Data.Entities.Account MapDtoToEntity(AccountDTO dto)
        {
            var entity = new Data.Entities.Account
            {
                Guid = dto.Guid ?? Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                ImagePath = dto.ImagePath,
                Created = dto.Created,
                Modified = dto.Modified,
            };

            return entity;
        }

        /// <summary>
        /// This method converts a list of account entities into account dtos
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        private List<AccountDTO> MapEntitiesToDtos(List<Data.Entities.Account> entities)
        {
            List<AccountDTO> dtos = new List<AccountDTO>();
            foreach (var entity in entities)
            {
                dtos.Add(MapEntityToDto(entity));
            }

            return dtos;
        }
    }
}
