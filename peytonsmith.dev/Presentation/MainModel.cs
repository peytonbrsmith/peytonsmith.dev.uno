namespace peytonsmith.dev.Presentation;

using Windows.System;

public partial record MainModel
{
    private INavigator _navigator;

    public MainModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator)
    {
        _navigator = navigator;
        
        Title = $"{localizer["ApplicationName"]}";
        Title += $" - {appInfo?.Value?.Environment}";

        int img = new Random().Next(1, 6);
        HomeImage = $"ms-appx:///Assets/Images/memoji-{img}.png";
    }

    public string? Title { get; }
    public string? HomeImage { get; }

    public IState<string> Name => State<string>.Value(this, () => string.Empty);

    public async Task GoToSecond()
    {
        var name = await Name;
        await _navigator.NavigateViewModelAsync<SecondModel>(this, data: new Entity(name!));
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
        // await _navigator.NavigateViewModelAsync<ResumeModel>(this);

// #if __WASM__
//         if (System.Uri.TryCreate(
//             Uno.Foundation.WebAssemblyRuntime.InvokeJS("document.location.search"),
//             UriKind.RelativeOrAbsolute,
//             out var browserUrl))
//         {
//             var appPath = browserUrl.GetComponents(System.UriComponents.Path, UriFormat.Unescaped);
//             // TODO: Process the path in your application
//             await Launcher.LaunchUriAsync(new Uri(appPath + "/resume.pdf"));

//             Console.WriteLine($"Browser URL: {appPath}");
//         }
// #endif

        await Launcher.LaunchUriAsync(new Uri("https://peytonsmith.dev/resume.pdf"));
    }

}
