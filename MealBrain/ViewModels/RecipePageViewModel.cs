using CommunityToolkit.Mvvm.Input;
using MealBrain.Data.Entities;
using MealBrain.Data.Repositories;
using MealBrain.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MealBrain.ViewModels
{
    public class RecipePageViewModel : BaseViewModel
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ISessionContext _session;

        public ObservableCollection<string> SearchResults { get; } = new();
        public IAsyncRelayCommand SearchCommand { get; }
        public IAsyncRelayCommand CreateCommand { get; }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged();
                    _ = PerformSearch();
                }
            }
        }

        public bool NoResultsFound => SearchResults.Count == 0 && !string.IsNullOrWhiteSpace(SearchQuery);

        public RecipePageViewModel(IRecipeRepository recipeRepository, ISessionContext session)
        {
            _recipeRepository = recipeRepository;
            SearchCommand = new AsyncRelayCommand(PerformSearch);
            CreateCommand = new AsyncRelayCommand(CreateNewRecipe);
            _session = session;
        }

        private async Task PerformSearch()
        {
            var allRecipes = await _recipeRepository.GetAllRecipesByAcct(_session.CurrentAccount.AccountId);
            var filtered = allRecipes.Where(r => !string.IsNullOrWhiteSpace(SearchQuery) &&
                                                r.RecipeName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();   
            
            SearchResults.Clear();
             foreach (var recipe in filtered)
             {
                 SearchResults.Add(recipe.RecipeName);
                
             }
             OnPropertyChanged(nameof(NoResultsFound));
        }

        private async Task CreateNewRecipe()
        {
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                var newRecipe = new Recipe { RecipeName = SearchQuery };
                await _recipeRepository.InsertRecipe(newRecipe);
                await PerformSearch();

            }
            
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
