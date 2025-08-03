using System;
using MealBrain.Utilities.Services;
using MealBrain.ViewModels.Components;
using Microsoft.Maui.Controls;

namespace MealBrain.Views.Components;

public partial class FlyoutMenuViewWithUser : ContentView
{
    private readonly ISessionContext _sessionService;
    public FlyoutMenuViewWithUser()
    {
        InitializeComponent();
        _sessionService = MauiProgram.Services.GetService<ISessionContext>();
        BindingContext = new FlyoutMenuViewWithUserModel(_sessionService);
    }

    public void Refresh()
    {
        if (BindingContext is FlyoutMenuViewWithUserModel vm)
        {
            vm.Refresh();
        }
    }

    private void OnMenuToggleClicked(object sender, EventArgs e)
    {
        FlyoutPanel.IsVisible = !FlyoutPanel.IsVisible;
    }
}