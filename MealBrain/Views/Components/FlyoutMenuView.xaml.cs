using System;
using Microsoft.Maui.Controls;

namespace MealBrain.Views.Components;

public partial class FlyoutMenuView : ContentView
{
    public FlyoutMenuView()
    {
        InitializeComponent();
    }

    private void OnMenuToggleClicked(object sender, EventArgs e)
    {
        FlyoutPanel.IsVisible = !FlyoutPanel.IsVisible;
    }
}