namespace peytonsmith.dev.Presentation;

public partial class ShellViewModel : ObservableObject
{
	private readonly INavigator _navigator;

	public ShellViewModel(
		INavigator navigator)
	{
		_navigator = navigator;
		_ = Start();
	}

	public async Task Start()
	{
		// await _navigator.NavigateViewModelAsync<MainViewModel>(this);
		// await _navigator.NavigateRouteAsync(this, "/Main");

		// await _navigator.NavigateRouteAsync(this, "/TabBarWithData");

        // await _navigator.NavigateViewModelAsync<RootViewModel>(this);
		// await _navigator.NavigateRouteAsync(this, "/Root");

		// await _navigator.NavigateViewModelAsync<HomeViewModel>(this);
		await _navigator.NavigateRouteAsync(this, "/Home");
	}

	[RelayCommand]
    public async Task GoToHome()
    {
        await _navigator.NavigateRouteAsync(this, "/Home");
    }

	[RelayCommand]
    public async Task GoToMain()
    {
        await _navigator.NavigateRouteAsync(this, "/Main");
    }

	[RelayCommand]
    public async Task GoToContact()
    {
        await _navigator.NavigateRouteAsync(this, "/Contact");
    }

	[RelayCommand]
    public async Task GoToAbout()
    {
        await _navigator.NavigateRouteAsync(this, "/About");
    }

	[RelayCommand]
    public async Task GoBack()
    {
        await _navigator.NavigateBackAsync(this);
    }
}
