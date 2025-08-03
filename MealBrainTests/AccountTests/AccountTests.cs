using MealBrain.Data.Entities;
using MealBrain.Data.Repositories;
using MealBrain.Models.DTO;
using MealBrain.Utilities.Helpers;
using MealBrain.Utilities.ServiceResults;
using MealBrain.Utilities.Services;
using Moq;

namespace MealBrainTests.AccountTests
{
    /// <summary>
    /// Tests to cover behaviors related to User Accounts.
    /// For the purposes of the architecture, this will include the AccountViewModel,
    /// Account model, AccountService, AccountRepository, and Account entity, ultimately.
    /// </summary>
    public class AccountTests
    {
        // concrete test variables
        Account? _accountUnit; // this should be initialized within each test depending on whether you want to use the service mock or real account service
        AccountDTO? _accountDtoUnit;
        MealBrain.Data.Entities.AccountType? _accountTypeUnit;
        IAccountService? _serviceUnit; // this should be initialized within each it is needed, whether you want to use the repo mock or the real account repo
        IAccountRepository? _repoUnit;
        MealBrain.Data.Entities.Account? _entityUnit;

        // mock test variables
        Mock<AccountDTO>? _dtoMock;
        Mock<IAccountService>? _serviceMock;
        Mock<IAccountRepository>? _repoMock;
        Mock<MealBrain.Data.Entities.Account>? _entityMock;

        [SetUp]
        public void Setup()
        {
            // initialize test variables
            // mocks
            _dtoMock = new Mock<AccountDTO>();
            _entityMock = new Mock<MealBrain.Data.Entities.Account>();
            _repoMock = new Mock<IAccountRepository>();
            _serviceMock = new Mock<IAccountService>();

            // units
            _accountDtoUnit = new AccountDTO();
            _entityUnit = new MealBrain.Data.Entities.Account();
            _accountTypeUnit = new MealBrain.Data.Entities.AccountType();
        }

        [TearDown]
        public void TearDown()
        {
            // set test variables to null for garbage collection
            _accountUnit = null;
            _accountDtoUnit = null;
            _accountTypeUnit = null;
            _entityUnit = null;
            _repoUnit = null;
            _serviceUnit = null;

            _serviceMock = null;
            _repoMock = null;
            _entityMock = null;
            _dtoMock = null;
        }

        //[Test]
        //public void CanCreateAccountWithoutFirstName()
        //{
        //    _accountServiceMock.Setup(service => service.CreateAccount(_unitUnderTest)).Returns(_crudResult);

        //    Assert.That(_unitUnderTest.FirstName == null);

        //    _unitUnderTest.CreateNewAccount();

        //    Assert.That(_unitUnderTest.WasSuccess);
        //    Assert.Equals(_unitUnderTest.NotificationMessage, "");
        //}

        // Account Model Tests
        // When acount creation requested, model obtains success status and message
        [TestCase(false, "Unable to create account.")]
        [TestCase(true, "Account Createed")]
        public async Task ModelGetsStatusAndMessageOnAccountCreation(bool success, string message)
        {
            // Assign/Setups
            if (_serviceMock == null) _serviceMock = new Mock<IAccountService>(); // these conditional with assignment are because the lint says they could be null right here, but the setup method initiailzes the mock, so this line shouldn't be true
            _accountUnit = new Account(_serviceMock.Object);

            var result = new CrudResult<AccountDTO>();
            result.IsSuccess = success;
            result.ResultMessage = message;

            _serviceMock?.Setup(service => service.CreateAccount(It.IsAny<AccountDTO>())).ReturnsAsync(result);

            //Act
            _accountUnit.CreateNewAccount();

            //Assert
            Assert.That(_accountUnit.WasSuccess, Is.EqualTo(success));
            Assert.That(_accountUnit.NotificationMessage, Is.EqualTo(message));
        }

        // Account Service Tests
        // When account creation requested from the service, it returns a result with whether the request succeeded or failed.
        [TestCase("SuccessAccount", "Personal", true, "Account Created", 1)]
        [TestCase("FailAccount", "Business", false, "Unable to create account", 0)]
        [TestCase("FailAccountDupes", "Personal", false, "Dupe insertions", 2)]
        public async Task AccountCreationReturnsResultWithStatus(string name, string acctType, bool success, string message, int numInserted)
        {
            // Assign
            var result = new CrudResult<AccountDTO>();
            result.IsSuccess = success;
            result.ResultMessage = message;

            _serviceUnit = new AccountService(_repoMock.Object); // this shouldn't be null bc the mocks are initialized in the Setup method

            _repoMock?.Setup(repo => repo.InsertAccount(It.IsAny<MealBrain.Data.Entities.Account>())).ReturnsAsync(numInserted);
            _repoMock?.Setup(repo => repo.GetAccountTypeByName(It.IsAny<string>())).ReturnsAsync(_accountTypeUnit);
            _repoMock?.Setup(repo => repo.GetStatusMessage()).Returns(message);

            _accountUnit = new Account(_serviceUnit);
            _accountUnit.DisplayName = name;
            _accountUnit.AccountType = acctType;

            AccountMappers.MapAccountModelToDto(_accountUnit, _accountDtoUnit);
            // Act
            result = await _serviceUnit.CreateAccount(_accountDtoUnit);

            // Assert
            Assert.That(result.IsSuccess, Is.EqualTo(success));
            Assert.That(result.ResultMessage, Is.EqualTo(message));
        }

        // Account Repository Tests

        // Model - Repository, end to end
    }
}