namespace peytonsmith.dev.Presentation;

public partial class HomePage : Page
{
	public HomePage()
	{
		InitializeComponent();

		LoadResumeWebView();
	}

	public async void LoadResumeWebView()
	{
		// await ResumeWebView.EnsureCoreWebView2Async();
		// ResumeWebView.CoreWebView2.SetVirtualHostNameToFolderMapping(
		// 	"UnoNativeAssets",
		// 	"WebContent",
		// 	CoreWebView2HostResourceAccessKind.Allow);
		// ResumeWebView.CoreWebView2.Navigate("http://UnoNativeAssets/index.html");
	}
}