namespace MealBrain.ViewModels.Components;
using CommunityToolkit.Mvvm.Input;
using MealBrain.Models;
using MealBrain.Data.Repositories;
using MealBrain.Utilities.Services;
using MealBrain.Views;
using System.Windows.Input;

public partial class RecipeCardViewModel : BaseViewModel
{

	private RecipeCardModel _recipeCardModel;
	public Guid Guid { get; }

	public string DisplayName
	{
		get
		{
			return _recipeCardModel.RecipeName;
		}
	}
	
	public string ImagePath
	{
		get
		{
			return _recipeCardModel.ImagePath;
		}
	}


	public string NumServings
	{
		get
		{
			return _recipeCardModel.Servings;
		}
	}

	public string PrepTime
	{
		get
		{
			return _recipeCardModel.PrepTime;
		}
	}

	public string CookTime
	{
		get
		{
			return _recipeCardModel.CookTime;
		}
	}

	public ICommand CardTappedCommand { get; }

	public RecipeCardViewModel(RecipeCardModel recipeCardModel)
	{
		_recipeCardModel = recipeCardModel;

		CardTappedCommand = new AsyncRelayCommand(OnCardTapped);
	}
	 
	public async Task OnCardTapped()
	{
        //TODO: Implement navigation to recipe detail page with the selected recipe
        //await Shell.Current.GoToAsync($"{nameof(RecipeDetailPage)}?recipeGuid={_recipeCardModel.Guid}");
    }
}
