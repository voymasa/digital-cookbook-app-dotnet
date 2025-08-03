using MealBrain.Views.Components;

namespace MealBrain.Views;

public partial class UserSettingsPage : ContentPage
{
    public UserSettingsPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        //Set the Title of the shell to display the current user info
        var shellUserHeader = MauiProgram.Services.GetRequiredService<ShellUserHeaderView>();
        Shell.SetTitleView(this, shellUserHeader);
    }
}