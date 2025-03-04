namespace peytonsmith.dev.Presentation;

public partial class HomeViewModel : ObservableObject
{
	private INavigator _navigator;

	[ObservableProperty]
	private string title;
	[ObservableProperty]
    public string homeImage;

	private bool _initialized = false;

	public HomeViewModel(        
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

	[RelayCommand]
    public async Task OpenGitHub()
    {
        // await _navigator.NavigateViewModelAsync<MainModel>(this);
        await Windows.System.Launcher.LaunchUriAsync(new Uri("https://github.com/peytonbrsmith"));
    }

    [RelayCommand]
    public async Task OpenLinkedIn()
    {
        // await _navigator.NavigateViewModelAsync<MainModel>(this);
        await Windows.System.Launcher.LaunchUriAsync(new Uri("https://www.linkedin.com/in/peytonbrsmith/"));
    }

    [RelayCommand]
    public async Task OpenResume()
    {
        // await _navigator.NavigateViewModelAsync<MainModel>(this);
        await Windows.System.Launcher.LaunchUriAsync(new Uri("https://www.icloud.com/iclouddrive/02fa40QeJrBWj6EnEJx_c2ZtA#Peyton_Smith_-_Resume"));
    }
}
