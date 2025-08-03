using System.Windows.Input;

namespace MealBrain.ViewModels.Components;

public class FlyoutMenuViewModel : BaseViewModel
{
    public ICommand NavigateCommand { get; }
    private const string DashboardRoute = "AccountDashboard";
    private const string ConverterRoute = "MeasurementConverterPage";
    private const string SettingsRoute = "UserSettingsPage";
    private const string MainPageRoute = "///MainPage";

    public FlyoutMenuViewModel()
    {
        NavigateCommand = new Command<string>(async (target) => await NavigateToPage(target));
    }

    private async Task NavigateToPage(string target)
    {
        if (Shell.Current == null) return;

        switch (target)
        {
            case "Dashboard":
                await Shell.Current.GoToAsync(DashboardRoute); 
                break;
            case "Converter":
                await Shell.Current.GoToAsync(ConverterRoute);
                break;
            case "Settings":
                await Shell.Current.GoToAsync(SettingsRoute);
                break;
            case "SwitchUser":
                await Shell.Current.GoToAsync(MainPageRoute);
                break;
            case "Exit":
                bool confirm = await Shell.Current.DisplayAlert("Confirm Exit", "Are you sure you want to exit?", "Yes", "No");
                if (confirm)
                {
#if ANDROID
                    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#elif WINDOWS
                System.Environment.Exit(0);
#endif
                }
                break;
        }
    }
}