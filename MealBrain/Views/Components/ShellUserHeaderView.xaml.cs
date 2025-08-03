

using MealBrain.Utilities.Services;

namespace MealBrain.Views.Components;

public partial class ShellUserHeaderView : ContentView
{
    private readonly ShellHeaderViewModel _viewModel;
    public ShellUserHeaderView(ShellHeaderViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        _viewModel?.Refresh();
    }
}