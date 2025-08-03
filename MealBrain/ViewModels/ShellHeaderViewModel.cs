using MealBrain.Models;
using MealBrain.Utilities.Services;
using MealBrain.ViewModels;

public class ShellHeaderViewModel : BaseViewModel
{
    private readonly ISessionContext _sessionContext;

    public string DisplayName => string.IsNullOrWhiteSpace(_sessionContext.CurrentAccount?.DisplayName)
        ? "Guest"
        : _sessionContext.CurrentAccount.DisplayName;

    public string UserIcon => string.IsNullOrWhiteSpace(_sessionContext.CurrentAccount?.ImagePath)
        ? "placeholderprofileicon.jpg"
        : _sessionContext.CurrentAccount.ImagePath;

    public ShellHeaderViewModel(ISessionContext sessionContext)
    {
        _sessionContext = sessionContext;
    }

    public void Refresh()
    {
        RaisePropertyChanged(nameof(DisplayName));
        RaisePropertyChanged(nameof(UserIcon));
    }
}