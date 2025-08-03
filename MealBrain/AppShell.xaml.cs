namespace MealBrain
{
    using MealBrain.Utilities.Services;
    using MealBrain.Views;
    public partial class AppShell : Shell
    {
        
        public AppShell()
        {
            InitializeComponent();

            //Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(AccountDashboard), typeof(AccountDashboard));
            Routing.RegisterRoute(nameof(UserAccountPage), typeof(UserAccountPage));
            Routing.RegisterRoute(nameof(MeasurementConverterPage), typeof(MeasurementConverterPage));
            Routing.RegisterRoute(nameof(UserSettingsPage), typeof(UserSettingsPage));

            
        }
        private async void OnSwitchUserClicked(object sender, EventArgs e)
        {
            // Placeholder: Navigate to account selection or login page
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async void OnExitAppClicked(object sender, EventArgs e)
        {
            // Placeholder: Exit application logic
            //System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
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
    }
}