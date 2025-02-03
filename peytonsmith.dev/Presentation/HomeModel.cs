using Windows.System;

namespace peytonsmith.dev.Presentation;

public partial record HomeModel
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
    }

    public string? Title { get; }

    public IState<string> Name => State<string>.Value(this, () => string.Empty);

    public async Task GoToSecond()
    {
        var name = await Name;
        await _navigator.NavigateViewModelAsync<SecondModel>(this, data: new Entity(name!));
    }

    public async Task GoToMain()
    {
        await _navigator.NavigateViewModelAsync<MainModel>(this);
    }

    public async Task OpenGitHub()
    {
        // await _navigator.NavigateViewModelAsync<MainModel>(this);
        await Launcher.LaunchUriAsync(new Uri("https://github.com/peytonbrsmith"));
    }

    public async Task OpenLinkedIn()
    {
        // await _navigator.NavigateViewModelAsync<MainModel>(this);
        await Launcher.LaunchUriAsync(new Uri("https://www.linkedin.com/in/peytonbrsmith/"));
    }

    public async Task OpenResume()
    {
        // await _navigator.NavigateViewModelAsync<MainModel>(this);
        await Launcher.LaunchUriAsync(new Uri("https://www.icloud.com/iclouddrive/01aMbjvnP3VrWlmft97aq2QcA#Peyton_Smith_-_Resume"));
    }

}
