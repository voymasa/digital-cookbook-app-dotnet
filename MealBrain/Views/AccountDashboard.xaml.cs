using MealBrain.ViewModels;
using MealBrain.Views.Components;

namespace MealBrain.Views
{
    [QueryProperty(nameof(_viewModel), "accountGuid")]
    public partial class AccountDashboard : ContentPage
    {
        private AccountDashboardViewModel _viewModel;
        public AccountDashboard(AccountDashboardViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        private async void OnRecipesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecipeListPage());
        }

        private async void OnMealPlansClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MealPlanPage());
        }

        private async void OnGroceryListClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GroceryListPage());
        }

        // This enables the shell menu navigation
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Enable Flyout Menu on Dashboard Page
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            //Set the Title of the shell to display the current user info
            var shellUserHeader = MauiProgram.Services.GetRequiredService<ShellUserHeaderView>();
            Shell.SetTitleView(this, shellUserHeader);
        }
    }
}
