using CommunityToolkit.Maui;
using MealBrain.Data.Repositories;
using MealBrain.Models;
using MealBrain.Utilities.Helpers;
using MealBrain.Utilities.Services;
using MealBrain.ViewModels;
using MealBrain.ViewModels.Components;
using MealBrain.Views;
using MealBrain.Views.Components;
using Microsoft.Extensions.Logging;

namespace MealBrain
{
    public static class MauiProgram
    {
        public static IServiceProvider Services { get; private set; }
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Dependency Injection
            // Scopes: Singleton (One and only one instance), Scoped (the scope of where it is injected), Transient (no specific lifetime but usually follow that of the host)
            builder.RegisterServices()
                .RegisterModels()
                .RegisterViewModels()
                .RegisterViews()
                .RegisterRepositories();


            var app = builder.Build();

            Services = app.Services;

            return app;

        }

        /// <summary>
        /// Extension member for registering the view models in the DI container.
        /// This only needs the viewmodel class passed into the types.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            // Example: builder.Services.AddTransient<ViewModelClassName>(); 
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddTransient<AccountDashboardViewModel>();
            builder.Services.AddTransient<AccountCardViewModel>();
            // Add other ViewModels here
            builder.Services.AddTransient<UserAccountViewModel>();
            builder.Services.AddTransient<FlyoutMenuViewModel>();
            builder.Services.AddTransient<ShellHeaderViewModel>();
            builder.Services.AddTransient<MeasurementConverterViewModel>();
            builder.Services.AddTransient<RecipeDetailViewModel>();
            builder.Services.AddTransient<RecipeListViewModel>();
            return builder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            // Example: builder.Services.AddScoped<
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<AccountDashboard>();
            builder.Services.AddTransient<AccountCardView>();
            builder.Services.AddTransient<RecipeDetailPage>();
            builder.Services.AddTransient<IngredientEntry>();
            builder.Services.AddTransient<UserAccountPage>();
            builder.Services.AddTransient<FlyoutMenuView>();
            builder.Services.AddTransient<UserSettingsPage>();
            builder.Services.AddTransient<MeasurementConverterPage>();
            builder.Services.AddTransient<ShellUserHeaderView>();

            builder.Services.AddTransient<MeasurementConverterPage>();
            // Add other Views here
            return builder;
        }

        public static MauiAppBuilder RegisterModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<Account>();
            builder.Services.AddTransient<MeasurementModel>();
            builder.Services.AddTransient<RecipeDetailModel>();
            return builder;
        }

        /// <summary>
        /// Extension member for registering the services in the DI container.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            // Example: builder.Services.AddScoped<IServiceInterfaceName, ServiceClassToInject>();
            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddSingleton<ISessionContext, SessionContext>();
            builder.Services.AddTransient<IMeasurementService, MeasurementService>();
            builder.Services.AddTransient<IRecipeService, RecipeService>();
            return builder;
        }

        /// <summary>
        /// Extension member for registering the repository classes in the DI container.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static MauiAppBuilder RegisterRepositories(this MauiAppBuilder builder)
        {
			string dbPath = FileAccessHelper.GetLocalFilePath("mealbrain.db3");
            builder.Services.AddSingleton<IRecipeRepository, RecipeRepository>(
                s => ActivatorUtilities.CreateInstance<RecipeRepository>(s, dbPath));
            builder.Services.AddSingleton<IMeasurementRepository, MeasurementRepository>(
				s => ActivatorUtilities.CreateInstance<MeasurementRepository>(s, dbPath));
            builder.Services.AddSingleton<IAccountRepository, AccountRepository>(
                s => ActivatorUtilities.CreateInstance<AccountRepository>(s, dbPath));
            return builder;
        }
    }
}
