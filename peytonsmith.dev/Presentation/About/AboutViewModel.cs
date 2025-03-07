namespace peytonsmith.dev.Presentation;

public partial class AboutViewModel : ObservableObject
{
    private INavigator _navigator;
    private IStringLocalizer _localizer;
    private IOptions<AppConfig> _appInfo;

    [ObservableProperty]
    private string _title;

        [ObservableProperty]
    private string _aboutText;

    public AboutViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator)
    {
        _navigator = navigator;
        _localizer = localizer;
        _appInfo = appInfo;

        Title = "About Me";
        AboutText = $"{localizer["AboutBody"]}";
    }
}
