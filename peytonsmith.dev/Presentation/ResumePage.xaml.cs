using Microsoft.Web.WebView2.Core;

namespace peytonsmith.dev.Presentation;

public sealed partial class ResumePage : Page
{
    public ResumePage()
    {
        this.InitializeComponent();

        // LoadResumeWebView();
    }

    // public async void LoadResumeWebView()
    // {
    //     await ResumeWebView.EnsureCoreWebView2Async();
    //     ResumeWebView.CoreWebView2.
    //         SetVirtualHostNameToFolderMapping(
    //         "localhost:5000",
    //         "WebContent",
    //         CoreWebView2HostResourceAccessKind.Allow);
        
    //     ResumeWebView.CoreWebView2.Navigate("http://localhost:5000/resume.pdf");
    // }
}

