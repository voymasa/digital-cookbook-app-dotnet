using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MealBrain.Data.Repositories;
using MealBrain.Models;
using MealBrain.Utilities.Services;
using MealBrain.Models.DTO;
using Microsoft.Extensions.DependencyInjection;
using MealBrain.Views;

namespace MealBrain.ViewModels
{
    /// <summary>
    /// View model for managing recipe list and displaying it to the UI
    /// loads data from repository and converts the DTO's to view models
    /// Option to filter recipes by name, ingredients, or tags
    /// </summary>
    public class RecipeListViewModel : BaseViewModel
    {
        private readonly RecipeListModel _recipeListModel;

        public ObservableCollection<RecipeDTO> Recipes {   
            get
            {
                return _recipeListModel.DisplayedRecipes;
            }
        }

        public string SearchText
        {
            get => _recipeListModel.SearchText;
            set
            {
                if (_recipeListModel.SearchText == value) 
                    return;

                _recipeListModel.SearchText = value;
                RaisePropertyChanged(nameof(SearchText));
                FilterRecipes();
            }
        }

        /// <summary>
        /// command to call LoadRecipesAsync()
        /// made to bind to the UI for manual refreshing of the recipe list
        /// </summary>
        public ICommand RefreshCommand { get; }

        /// <summary>
        /// Command to navigate to recipe detail page when a card is tapped
        /// </summary>
        public ICommand CardTappedCommand { get; }

        public ICommand DeleteRecipeCommand { get; }

        public RecipeListViewModel(IRecipeService recipeService, ISessionContext sessionContext)
        {
            _recipeListModel = new RecipeListModel(recipeService, sessionContext);

            RefreshCommand = new Command(async () => await LoadRecipesAsync());
            CardTappedCommand = new Command<RecipeDTO>(async (recipe) => await NavigateToRecipeDetail(recipe));
            DeleteRecipeCommand = new Command<RecipeDTO>(async (recipe) => await DeleteRecipeAsync(recipe));
            //_ = LoadRecipesAsync(); // Start our first load
        }

        // converts all recipes into view models and then updates the collection of recipes
        public async Task LoadRecipesAsync()
        {
            await _recipeListModel.LoadRecipesAsync();
            RaisePropertyChanged(nameof(Recipes));
        }

        // filter recipes list by name, ingredients, or tags
        public async Task FilterRecipes()
        {
            await _recipeListModel.FilterRecipes();
            RaisePropertyChanged(nameof(Recipes));
        }

        private async Task DeleteRecipeAsync(RecipeDTO recipe)
        {
            if (recipe?.Guid == null)
                return;

            bool confirm = await Application.Current.MainPage.DisplayAlert("Confirm Delete", $"Delete '{recipe.RecipeName}'?", "Yes", "No");
            if (!confirm)
                return;

            await _recipeListModel.DeleteRecipeAsync(recipe.Guid.Value);
            await LoadRecipesAsync(); // Refresh the list
        }

        // Navigate to recipe detail page
        private async Task NavigateToRecipeDetail(RecipeDTO recipe)
        {
            try
            {
                // Get a new RecipeDetailViewModel from the service container
                var recipeDetailViewModel = MauiProgram.Services.GetRequiredService<RecipeDetailViewModel>();
                
                // Load the recipe data into the view model
                await recipeDetailViewModel.LoadAsync(recipe.Guid ?? Guid.Empty);
                
                // Create a new RecipeDetailPage with the view model
                var recipeDetailPage = new RecipeDetailPage(recipeDetailViewModel);
                
                // Navigate to the recipe detail page
                await Shell.Current.Navigation.PushAsync(recipeDetailPage);
            }
            catch (Exception ex)
            {
                // Handle navigation error
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to open recipe: {ex.Message}", "OK");
            }
        }
    }
}