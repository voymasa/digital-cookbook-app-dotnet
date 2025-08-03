using MealBrain.Data.Repositories;
using MealBrain.Utilities.Services;
using SQLite;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using MealBrain.Models.DTO;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;

namespace MealBrain.Models
{
    // model to get and filter list of recipe (NOT INDIVDUAL RECIPES)
    public class RecipeListModel
    {
        public string SearchText { get; set; }
        private IRecipeService _recipeService;
        private ISessionContext _sessionContext;

        private List<RecipeDTO> AllRecipes { get; set; } = new();
        public ObservableCollection<RecipeDTO> DisplayedRecipes { get; set; } = new();

        public string NotificationMessage { get; set; } = string.Empty;

        public RecipeListModel(IRecipeService recipeService, ISessionContext sessionContext)
        {
            _recipeService = recipeService;
            _sessionContext = sessionContext;
        }

        // Load all the recipes for a give account
        public async Task LoadRecipesAsync()
        {

            if (_sessionContext.CurrentAccount != null && _sessionContext.CurrentAccount.AccountId != null)
            {
               
                var result = await _recipeService.GetAllRecipesForAcct(_sessionContext.CurrentAccount.AccountId);
                var dtos = result.Data ?? [];
                AllRecipes = dtos; // if GetAllRecipes is null then we make a new list to assign it to
                DisplayedRecipes = AllRecipes.ToObservableCollection<RecipeDTO>();

            } else
            {
                NotificationMessage = "No account is set in the session context, cannot load recipes.";
           
            }
        }

        public async Task DeleteRecipeAsync(Guid recipeGuid)
        {
            await _recipeService.DeleteRecipeAsync(recipeGuid); 
            AllRecipes = AllRecipes.Where(r => r.Guid != recipeGuid).ToList();
            DisplayedRecipes = AllRecipes.ToObservableCollection();
        }

        public async Task FilterRecipes()
        {
            DisplayedRecipes = AllRecipes.Where(r =>
            (!string.IsNullOrWhiteSpace(r.RecipeName) &&
            r.RecipeName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) ||
            (!string.IsNullOrWhiteSpace(r.Description) &&
            r.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
            ).ToObservableCollection<RecipeDTO>();
        }
    }
}