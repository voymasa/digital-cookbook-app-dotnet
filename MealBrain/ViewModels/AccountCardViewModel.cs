namespace MealBrain.ViewModels.Components;
using CommunityToolkit.Mvvm.Input;
using MealBrain.Models;
using MealBrain.Data.Repositories;
using MealBrain.Utilities.Services;
using MealBrain.Views;
using System.Windows.Input;

public partial class AccountCardViewModel : BaseViewModel
{
    private readonly ISessionContext _sessionService;

    private readonly Account _account;
    public Guid Guid { get; }
    public string DisplayName { get; }

    public string ImagePath { get; }

    public ICommand CardTappedCommand { get; }

    public AccountCardViewModel(Account account, ISessionContext sessionService)
    {
        _sessionService = sessionService;
        _account = account;

        //Guid = account.Guid;
        DisplayName = account.DisplayName ?? "No Name";
        ImagePath = string.IsNullOrEmpty(account.ImagePath) ? "placeholderprofileicon.jpg" : account.ImagePath;
        RaisePropertyChanged(nameof(ImagePath));

        CardTappedCommand = new AsyncRelayCommand(OnCardTapped);
    }

    public async Task OnCardTapped()
    {
        _sessionService.SetAccount(_account);
        await Shell.Current.GoToAsync($"{nameof(AccountDashboard)}");
        Shell.Current.BindingContext = new ShellHeaderViewModel(MauiProgram.Services.GetService<ISessionContext>());
    }
}