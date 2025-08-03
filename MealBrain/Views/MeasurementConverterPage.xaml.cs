using MealBrain.ViewModels;

using MealBrain.Views.Components;

namespace MealBrain.Views;

public partial class MeasurementConverterPage : ContentPage
{
    private MeasurementConverterViewModel _viewModel;
    public MeasurementConverterPage(MeasurementConverterViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // load measurments
        _viewModel.LoadMeasurements();

        //Set the Title of the shell to display the current user info
        var shellUserHeader = MauiProgram.Services.GetRequiredService<ShellUserHeaderView>();
        Shell.SetTitleView(this, shellUserHeader);
    }

	private void MeasurementFromPicker_SelectedIndexChanged(object sender, EventArgs e)
	{
        // when the item changes update the picker display text
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            picker.Title = picker.Items[selectedIndex];
        }
	}

	private void MeasurementToPicker_SelectedIndexChanged(object sender, EventArgs e)
	{
		// when the item changes update the picker display text
		var picker = (Picker)sender;
		int selectedIndex = picker.SelectedIndex;

		if (selectedIndex != -1)
		{
			picker.Title = picker.Items[selectedIndex];
		}
	}
}