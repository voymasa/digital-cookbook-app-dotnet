using CommunityToolkit.Mvvm.Input;
using MealBrain.Data.Entities;
using MealBrain.Data.Repositories;
using MealBrain.Utilities.Services;
using MealBrain.ViewModels.Components;
using System.Collections.ObjectModel;
using MealBrain.Utilities.Helpers;

namespace MealBrain.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISessionContext _sessionService;
        private readonly IAccountService _accountService;

        public ObservableCollection<AccountCardViewModel> Accounts { get; } = new();

        public IAsyncRelayCommand SignupCommand { get; }
        public IAsyncRelayCommand ExitCommand { get; }

        public MainPageViewModel(IAccountRepository accountRepository, ISessionContext sessionService, IAccountService accountService)
        {
            _accountRepository = accountRepository;

            _sessionService = sessionService;
            _accountService = accountService;


            SignupCommand = new AsyncRelayCommand(OnSignupAsync);
            ExitCommand = new AsyncRelayCommand(OnExit);
        }

        private async Task OnSignupAsync()
        {
            // Navigate to the account creation page
            await Shell.Current.GoToAsync(nameof(MealBrain.Views.UserAccountPage));
        }

        private async Task OnExit()
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirm Exit", "Are you sure you want to exit?", "Yes", "No");
            if (confirm)
            {


        #if ANDROID
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        #elif WINDOWS
                System.Environment.Exit(0);
        #endif
            }
        }

        public async Task LoadAccountsAsync()
        {
            var accountList = await _accountRepository.GetAllAccounts();
            Accounts.Clear();
            System.Diagnostics.Debug.WriteLine($"Loaded {accountList.Count} accounts.");
            foreach (var account in accountList)
            {
                System.Diagnostics.Debug.WriteLine($"Account: {account.DisplayName}, Guid: {account.Guid}");
                var model = account.ToModel(_accountService);
                Accounts.Add(new AccountCardViewModel(model, _sessionService));
            }
            RaisePropertyChanged(nameof(Accounts));
        }
    }
}
