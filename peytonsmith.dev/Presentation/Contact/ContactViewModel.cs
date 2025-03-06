using Windows.ApplicationModel.Email;

namespace peytonsmith.dev.Presentation;

public partial class ContactViewModel : ObservableObject
{
    private INavigator _navigator;
    private IStringLocalizer _localizer;
    private IOptions<AppConfig> _appInfo;


    [ObservableProperty]
    private string title;

    //inputs

    [ObservableProperty]
    public string _name = "";
    [ObservableProperty]
    public string _email = "";
    [ObservableProperty]
    public string _subject = "";
    [ObservableProperty]
    public string _message = "";

    //teaching tips (alerts)

    [ObservableProperty]
    public bool nameMissing;
    [ObservableProperty]
    public bool emailMissing;
    [ObservableProperty]
    public bool subjectMissing;
    [ObservableProperty]
    public bool messageMissing;

    private bool _initialized = false;

    public ContactViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator)
    {
        _navigator = navigator;
        _localizer = localizer;
        _appInfo = appInfo;

        Title = "Contact Me";
    }

    // [RelayCommand]
    // public async Task GoToHome()
    // {
    //     await _navigator.
    // }

    [RelayCommand]
    public async Task SendMessage()
    {
        NameMissing = string.IsNullOrWhiteSpace(Name);
        EmailMissing = string.IsNullOrWhiteSpace(Email);
        SubjectMissing = string.IsNullOrWhiteSpace(Subject);
        MessageMissing = string.IsNullOrWhiteSpace(Message);
        if (NameMissing || EmailMissing || SubjectMissing || MessageMissing)
        {
            return;
        }

        // Append the user's name, email, and source to the message
        Message += $"\n\n{Name}\n{Email}\nSent From {_localizer["ApplicationName"]}";
        await EmailManager.ShowComposeNewEmailAsync(new EmailMessage
        {
            Subject = Subject,
            Body = Message,
            To = { new EmailRecipient("contact@peytonsmith.dev", "Peyton Smith") },
            CC = { new EmailRecipient(Email, Name) }
        });
    }

}
