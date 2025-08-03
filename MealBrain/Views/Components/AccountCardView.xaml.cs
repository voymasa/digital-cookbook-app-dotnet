using MealBrain.Models;
using MealBrain.Data.Entities;
using MealBrain.Utilities.Services;
using MealBrain.ViewModels.Components;
using MealBrain.Utilities.Helpers;

namespace MealBrain.Views.Components;

public partial class AccountCardView : ContentView
{
    private readonly ISessionContext _sessionService;
    private readonly IAccountService _accountService;
    public AccountCardView()
	{
		InitializeComponent();

        // Listen to when the BindingContext is set
        BindingContextChanged += OnBindingContextChanged;

        _sessionService = MauiProgram.Services.GetService<ISessionContext>();
        _accountService = MauiProgram.Services.GetService<IAccountService>();
    }

    private void OnBindingContextChanged(object sender, EventArgs e)
    {
        if (BindingContext is MealBrain.Data.Entities.Account entity)
        {
            // Convert entity to model
            var model = entity.ToModel(_accountService);

            BindingContext = new AccountCardViewModel(model, _sessionService);
        }
    }
}