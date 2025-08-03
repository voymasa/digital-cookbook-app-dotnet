using MealBrain.Models;
using MealBrain.Utilities.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MealBrain.ViewModels
{
    internal class UserAccountViewModel : INotifyPropertyChanged
    {

        private Account _account;
        private string _selectedImagePath;
        private string _selectedAccountType = "Personal";

        public List<string> AvailableAccountTypes => new List<string>
        {
            "Personal",
            // "Business",
            // "Family",
            // "Professional"
        };


        public List<string> PreloadedIcons => new List<string>
        {
            "avatar1.jpg",
            "avatar2.jpg",
            "dotnet_bot.png",  // Can use this as an option too
            "placeholderprofileicon.jpg"
        };

        public UserAccountViewModel(IAccountService accountService)
        {
            // Ensure default image is set
            _selectedImagePath = "placeholderprofileicon.jpg";

            _account = new Account(accountService);

            SaveCommand = new Command(async () => await SaveAccountAsync());
            CancelCommand = new Command(async () => await CancelAsync());
            SelectImageCommand = new Command(async () => await SelectImageAsync());
            ClearImageCommand = new Command(() => SelectedImagePath = "placeholderprofileicon.jpg");
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SelectImageCommand { get; }
        public ICommand ClearImageCommand { get; }

        public string FirstName
        {
            get => _account.FirstName ?? string.Empty;
            set
            {
                _account.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _account.LastName ?? string.Empty;
            set
            {
                _account.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string DisplayName
        {
            get => _account.DisplayName;
            set
            {
                _account.DisplayName = value;
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public string Email
        {
            get => _account.Email ?? string.Empty;
            set
            {
                _account.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string SelectedImagePath
        {
            get => _selectedImagePath;
            set
            {
                _selectedImagePath = value;
                _account.ImagePath = value;
                OnPropertyChanged(nameof(SelectedImagePath));
            }
        }

        public string SelectedAccountType
        {
            get => _selectedAccountType;
            set
            {
                _selectedAccountType = value;
                OnPropertyChanged();
            }
        }

        private async Task SaveAccountAsync()
        {

            //TODO: XAML Logic to validate the form inputs before proceeding
            //TODO: confirm if they want a personal account specifically 
            bool confirm = await Application.Current.MainPage.DisplayAlert("Confirm", "Are you sure you want to save the account?", "Yes", "No");


            if (!confirm)
            {
                return; // User cancelled the save operation
            }

            _account.AccountType = SelectedAccountType;
            _account.ImagePath = SelectedImagePath;

            System.Diagnostics.Debug.WriteLine($"Creating account with: Name={_account.FirstName} {_account.LastName}, Type={_account.AccountType}");

            await _account.CreateNewAccount();
            OnPropertyChanged(nameof(_account.NotificationMessage));
            OnPropertyChanged(nameof(_account.WasSuccess));
            // Handle navigation or further actions based on success or failure

            // Check success AFTER creation
            if (!_account.WasSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", _account.NotificationMessage, "OK");
                return;
            }

            await Application.Current.MainPage.DisplayAlert("Success", "Account created successfully!", "OK");
            await Shell.Current.GoToAsync("..");
        }

        private async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async Task SelectImageAsync()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select Account Icon",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                SelectedImagePath = result.FullPath;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

}
