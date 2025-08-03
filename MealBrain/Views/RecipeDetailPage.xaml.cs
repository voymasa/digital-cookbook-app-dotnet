using MealBrain.ViewModels;

namespace MealBrain.Views;

public partial class RecipeDetailPage : ContentPage
{
    private readonly RecipeDetailViewModel _viewModel;

    public RecipeDetailPage(RecipeDetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;


    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.LoadMeasurements();

        // Load recipe if needed
        if (_viewModel.Recipes.IsNew)
        {
            await _viewModel.Recipes.LoadAsync(new Guid()); // Load for a new recipe (optional depending on logic)
        }
        else
        {
            await _viewModel.Recipes.LoadAsync(_viewModel.Recipes.Guid); // Load an existing recipe
        }
    }
}