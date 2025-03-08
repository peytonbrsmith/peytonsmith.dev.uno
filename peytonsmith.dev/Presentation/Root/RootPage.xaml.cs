namespace peytonsmith.dev.Presentation;

public sealed partial class RootPage : Page
{
	public RootPage()
	{
		this.InitializeComponent();
	}

	private void NavViewToggleButton_Click(object sender, RoutedEventArgs e)
	{
		NavigationViewControl.IsPaneOpen = !NavigationViewControl.IsPaneOpen;
	}
}
