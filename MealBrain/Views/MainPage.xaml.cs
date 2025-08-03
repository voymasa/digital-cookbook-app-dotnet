using MealBrain.ViewModels;

namespace MealBrain.Views;

public partial class MainPage : ContentPage
{
    private MainPageViewModel _viewModel;
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;

        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAccountsAsync();
        
        //Disables shell menu navigation for the start screen only
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
        Shell.SetTitleView(this, null);
    }
}