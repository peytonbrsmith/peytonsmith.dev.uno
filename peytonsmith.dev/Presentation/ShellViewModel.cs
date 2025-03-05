namespace peytonsmith.dev.Presentation;

public class ShellViewModel
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

		// await _navigator.NavigateViewModelAsync<HomeViewModel>(this);
		await _navigator.NavigateRouteAsync(this, "/Home");
	}
}
