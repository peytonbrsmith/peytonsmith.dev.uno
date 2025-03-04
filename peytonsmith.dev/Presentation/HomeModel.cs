using Windows.System;

namespace peytonsmith.dev.Presentation;

public partial class HomeModel : ObservableObject
{
    private INavigator _navigator;

    public HomeModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator)
    {
        _navigator = navigator;
        Title = "Home";
        Title += $" - {localizer["ApplicationName"]}";
        Title += $" - {appInfo?.Value?.Environment}";

        int img = new Random().Next(1, 6);
        HomeImage = $"ms-appx:///Assets/Images/memoji-{img}.png";
    }
    public string? Title { get; }
    public string? HomeImage { get; }

    [RelayCommand]
    public async Task OpenGitHub()
    {
        // await _navigator.NavigateViewModelAsync<MainModel>(this);
        await Launcher.LaunchUriAsync(new Uri("https://github.com/peytonbrsmith"));
    }

    [RelayCommand]
    public async Task OpenLinkedIn()
    {
        // await _navigator.NavigateViewModelAsync<MainModel>(this);
        await Launcher.LaunchUriAsync(new Uri("https://www.linkedin.com/in/peytonbrsmith/"));
    }

    [RelayCommand]
    public async Task OpenResume()
    {
        // await _navigator.NavigateViewModelAsync<MainModel>(this);
        await Launcher.LaunchUriAsync(new Uri("https://www.icloud.com/iclouddrive/02fa40QeJrBWj6EnEJx_c2ZtA#Peyton_Smith_-_Resume"));
    }

    public async Task GoToHome()
    {
        _ = _navigator.ShowMessageDialogAsync(this, title: "This is Uno", content: "Hello Uno.Extensions!");
        await _navigator.NavigateViewModelAsync<HomeViewModel>(this);
    }

    public async Task GoToAbout()
    {
        _ = _navigator.ShowMessageDialogAsync(this, title: "This is Uno", content: "Hello Uno.Extensions!");
        await _navigator.NavigateViewModelAsync<AboutModel>(this);
    }

    public async Task GoToContact()
    {
        _ = _navigator.ShowMessageDialogAsync(this, title: "This is Uno", content: "Hello Uno.Extensions!");
        await _navigator.NavigateViewModelAsync<ContactModel>(this);
    }

}
