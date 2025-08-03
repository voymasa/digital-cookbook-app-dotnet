using MealBrain.Models;
using MealBrain.Utilities.Services;
using System.Windows.Input;

namespace MealBrain.ViewModels
{
    public class AccountDashboardViewModel : BaseViewModel
    {
        private readonly ISessionContext _sessionService;
        public Account CurrentAccount { get; set; }
        public string DisplayName
        {
            get
            {
                return string.IsNullOrWhiteSpace(CurrentAccount.DisplayName) ? "John Doe" : CurrentAccount.DisplayName;
            }
        }
        public string UserIcon
        {
            get
            {
                return string.IsNullOrWhiteSpace(CurrentAccount.ImagePath) ? "placeholderprofileicon.jpg" : CurrentAccount.ImagePath;
            }
        }

        // notification properties
        public string Notification
        {
            get
            {
                return CurrentAccount.NotificationMessage;
            }
        }
        public bool Success
        {
            get
            {
                return CurrentAccount.WasSuccess;
            }
        }

        public AccountDashboardViewModel(ISessionContext sessionService)
        {
            _sessionService = sessionService;
            CurrentAccount = _sessionService.CurrentAccount;
            
            RaiseAllProperties();
        }

        private void RaiseAllProperties()
        {
            RaisePropertyChanged(nameof(DisplayName));
            RaisePropertyChanged(nameof(UserIcon));
            RaisePropertyChanged(nameof(Notification));
            RaisePropertyChanged(nameof(Success));
        }

        
	}
}
