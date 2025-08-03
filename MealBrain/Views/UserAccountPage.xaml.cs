using MealBrain.Utilities.Services;

using MealBrain.ViewModels;

namespace MealBrain.Views
{
    public partial class UserAccountPage : ContentPage
    {
        public UserAccountPage(IAccountService accountService)
        {
            InitializeComponent();
            BindingContext = new ViewModels.UserAccountViewModel(accountService);
        }

        private void OnPreloadedIconTapped(object sender, TappedEventArgs e)
        {
            // This method would handle the event when a preloaded icon is tapped.
            // It could be used to select an icon for the user account.
            // The actual implementation is not shown here as it depends on the specific requirements.

            if (e.Parameter is string iconPath && BindingContext is UserAccountViewModel viewModel)
            {
                viewModel.SelectedImagePath = iconPath;
            }
        }

        private void OnAccountTypeChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value && sender is RadioButton radioButton && BindingContext is UserAccountViewModel viewModel)
            {
                viewModel.SelectedAccountType = radioButton.Content?.ToString();
            }
        }
    }
}