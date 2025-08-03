using MealBrain.Data.Repositories;
namespace MealBrain
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

        }
    }
}
