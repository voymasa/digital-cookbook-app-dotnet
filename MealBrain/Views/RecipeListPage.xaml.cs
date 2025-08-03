using MealBrain.ViewModels;
using MealBrain.Data.Repositories;
using MealBrain.Utilities.Services;
using MealBrain.Models.DTO;
using Microsoft.Extensions.DependencyInjection;
using MealBrain.Views.Components;

namespace MealBrain.Views;

public partial class RecipeListPage : ContentPage
{
    private RecipeListViewModel _viewModel;

    public RecipeListPage()
    {
        InitializeComponent();

        _viewModel = MauiProgram.Services.GetService<RecipeListViewModel>(); ;
        BindingContext = _viewModel;
    }

    private async void OnAddRecipeClicked(object sender, EventArgs e)
    {
        try
        {
            // Get a new RecipeDetailViewModel from the service container
            var recipeDetailViewModel = MauiProgram.Services.GetRequiredService<RecipeDetailViewModel>();
            
            // Create a new RecipeDetailPage with the view model
            var recipeDetailPage = new RecipeDetailPage(recipeDetailViewModel);
            
            // Navigate to the recipe detail page for creating a new recipe
            await Navigation.PushAsync(recipeDetailPage);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to open recipe creation page: {ex.Message}", "OK");
        }
    }


    private async void OnDeleteRecipeClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is RecipeDTO recipe)
        {
            bool confirm = await DisplayAlert(
                "Confirm Delete", 
                $"Are you sure you want to delete '{recipe.RecipeName}'?\n\nNote: Any meal plans or grocery lists that contained this recipe will have a placeholder stating that the recipe was removed.", 
                "Delete", 
                "Cancel");

            if (confirm)
            {
                //await _viewModel.DeleteRecipeAsync(recipe);
            }
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shellUserHeader = MauiProgram.Services.GetRequiredService<ShellUserHeaderView>();
        Shell.SetTitleView(this, shellUserHeader);
        await _viewModel.LoadRecipesAsync();
    }
}