using Uno.Resizetizer;

namespace peytonsmith.dev;

public partial class App : Application
{
    /// <summary>
    /// Initializes the singleton application object. This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    protected Window? MainWindow { get; private set; }
    protected IHost? Host { get; private set; }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var builder = this.CreateBuilder(args)
            // Add navigation support for toolkit controls such as TabBar and NavigationView
            .UseToolkitNavigation()
            .Configure(host => host
#if DEBUG
                // Switch to Development environment when running in DEBUG
                .UseEnvironment(Environments.Development)
#endif
                .UseLogging(configure: (context, logBuilder) =>
                {
                    // Configure log levels for different categories of logging
                    logBuilder
                        .SetMinimumLevel(
                            context.HostingEnvironment.IsDevelopment() ?
                                LogLevel.Information :
                                LogLevel.Warning)

                        // Default filters for core Uno Platform namespaces
                        .CoreLogLevel(LogLevel.Warning);

                    // Uno Platform namespace filter groups
                    // Uncomment individual methods to see more detailed logging
                    //// Generic Xaml events
                    // logBuilder.XamlLogLevel(LogLevel.Debug);
                    //// Layout specific messages
                    logBuilder.XamlLayoutLogLevel(LogLevel.Debug);
                    //// Storage messages
                    //logBuilder.StorageLogLevel(LogLevel.Debug);
                    //// Binding related messages
                    logBuilder.XamlBindingLogLevel(LogLevel.Debug);
                    //// Binder memory references tracking
                    //logBuilder.BinderMemoryReferenceLogLevel(LogLevel.Debug);
                    //// DevServer and HotReload related
                    //logBuilder.HotReloadCoreLogLevel(LogLevel.Information);
                    //// Debug JS interop
                    //logBuilder.WebAssemblyLogLevel(LogLevel.Debug);

                }, enableUnoLogging: true)
                .UseConfiguration(configure: configBuilder =>
                    configBuilder
                        .EmbeddedSource<App>()
                        .Section<AppConfig>()
                )
                // Enable localization (see appsettings.json for supported languages)
                .UseLocalization()
                // Register Json serializers (ISerializer and ISerializer)
                .UseSerialization((context, services) => services
                    .AddContentSerializer(context)
                    .AddJsonTypeInfo(WeatherForecastContext.Default.IImmutableListWeatherForecast))
                .UseHttp((context, services) => services
                    // Register HttpClient
#if DEBUG
                    // DelegatingHandler will be automatically injected into Refit Client
                    .AddTransient<DelegatingHandler, DebugHttpHandler>()
#endif
                    .AddSingleton<IWeatherCache, WeatherCache>()
                    .AddRefitClient<IApiClient>(context))

                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IQueryUserService, QueryUserService>();
                })
                .UseNavigation(RegisterRoutes)
            );

        MainWindow = builder.Window;

#if DEBUG
        MainWindow.UseStudio();
#endif
        MainWindow.SetWindowIcon();

        Host = await builder.NavigateAsync<Shell>();
    }


    private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
    {
        var messageDialog = new MessageDialogViewMap(
            Title: "This is Uno",
            Content: "Hello Uno.Extensions",
            Buttons:
            [
                new DialogAction(Label: "Ok"),
                new DialogAction(Label: "Cancel")
            ]
        );

        views.Register(
            // new ViewMap(ViewModel: typeof(ShellViewModel)),
            new ViewMap<Shell, ShellViewModel>(),
            new ViewMap<RootPage, RootViewModel>(),
            new ViewMap<HomePage, HomeViewModel>(),
            new ViewMap<AboutPage, AboutViewModel>(),
            new ViewMap<ContactPage, ContactViewModel>(),
            new ViewMap<MainPage, MainViewModel>(),
            new ViewMap<CControlNavigationPage>(),
            new DataViewMap<CControlRightPage, CControlRightViewModel, Entity>(),
            new ViewMap<TabBarNavigationPage>(),
            new ViewMap<TabBarItem3>(),
            new ViewMap<TabBarWithDataPage, TabBarWithDataViewModel>(),
            new DataViewMap<FirstTabBarItemWithDataPage, FirstTabBarItemWithDataViewModel, Entity>(),
            new DataViewMap<SecondTabBarItemWithDataPage, SecondTabBarItemWithDataViewModel, Entity>(),
            new ViewMap<RequestValueMainPage, RequestValueMainViewModel>(),
            new ResultDataViewMap<RequestValueSecondPage, RequestValueSecondViewModel, Entity>(),
            new ViewMap<PageNavigation, PageNavigationViewModel>(),
            new ViewMap<SamplePage>(),
            new ViewMap<MessageDialogPage, MessageDialogViewModel>(),
            new ViewMap<ModalDialogPage, ModalDialogViewModel>(),
            new ViewMap<ModalDialogSecondPage>(),
            new ViewMap<ModalContentDialog>(),
            new ViewMap<ComplexFlyoutPage>(ResultData: typeof(DialogsFlyoutsData)),
            new ViewMap<ComplexFlyoutPageOne, ComplexFlyoutOneViewModel>(),
            new ViewMap<ComplexFlyoutPageTwo, ComplexFlyoutTwoViewModel>(),
            new ViewMap<FlyoutDrawerPage>(),
            new ViewMap<NavFlyout>(),
            new ViewMap<FirstPage>(),
            new ViewMap<SecondPage>(),
            new ViewMap<ThirdPage>(),
            messageDialog,
            new ViewMap<ToFromQueryMainPage, ToFromQueryMainViewModel>(),
            // FIXME: Using the URL address bar to navigate doesn't work
            // eg: http://localhost:5000/Main/ToFromQuery?QueryUser.Id=2b64071a-2c8a-45e4-9f48-3eb7d7aace41
            // https://github.com/unoplatform/uno.extensions/issues/2488
            new DataViewMap<ToFromQueryPage, ToFromQueryViewModel, QueryUser>(
                ToQuery: user => new Dictionary<string, string>
                {
                    { "QueryUser.Id", $"{user.Id}" }
                },
                FromQuery: async (sp, query) =>
                {
                    var userService = sp.GetRequiredService<IQueryUserService>();

                    if (Guid.TryParse($"{query["QueryUser.Id"]}", out var guid))
                    {
                        var user = userService.GetById(guid);
                        return user ?? new QueryUser(guid, "User not found");
                    }

                    return new QueryUser(guid, "User not found");
                }
            )
        );

        routes.Register(
            new RouteMap("", View: views.FindByViewModel<ShellViewModel>(),
                Nested:
                [
                    // new ("Root", View: views.FindByViewModel<RootViewModel>(),
                    //     Nested:
                    //     [
                    //         new ("Home", View: views.FindByViewModel<HomeViewModel>(), IsDefault: true),
                    //         new ("About", View: views.FindByViewModel<AboutViewModel>()),
                    //         new ("Contact", View: views.FindByViewModel<ContactViewModel>()),
                    //     ]
                    // ),

                    new ("Home", View: views.FindByViewModel<HomeViewModel>()),
                    new ("About", View: views.FindByViewModel<AboutViewModel>()),
                    new ("Contact", View: views.FindByViewModel<ContactViewModel>()),

#if DEBUG
					new ("Main", View: views.FindByViewModel<MainViewModel>(),
                        Nested:
                        [
                            // new ("Home", View: views.FindByViewModel<HomeViewModel>()),
                            // new ("About", View: views.FindByViewModel<AboutModel>()),
                            // new ("Contact", View: views.FindByViewModel<ContactModel>()),
							#region Page Navigation
							new ("PageNavigation", View: views.FindByViewModel<PageNavigationViewModel>(), IsDefault: true),
                            new ("Sample", View: views.FindByView<SamplePage>(), DependsOn: "PageNavigation"),
							#endregion

							#region ContentControl Navigation
							new ("CControlNavigation", View: views.FindByView<CControlNavigationPage>()),
                            new ("CControlRight", View: views.FindByView<CControlRightPage>(), DependsOn: "CControlNavigation"),
							#endregion

							#region TabBar Navigation
							new ("TabBarNavigation", View: views.FindByView<TabBarNavigationPage>(),
                                Nested:
                                [
                                    new ("TBOne"),
                                    new ("TBTwo", IsDefault: true),
                                    new ("TBThree", View: views.FindByView<TabBarItem3>())
                                ]
                            ),
							#endregion

							#region TabBar with Data Navigation
							new ("TabBarWithData", View: views.FindByViewModel<TabBarWithDataViewModel>(),
                                Nested:
                                [
                                    new ("TBDataOne", View: views.FindByViewModel<FirstTabBarItemWithDataViewModel>()),
                                    new ("TBDataTwo", View: views.FindByViewModel<SecondTabBarItemWithDataViewModel>())
                                ]
                            ),
							#endregion

							#region Request a Value
							new ("RequestValueMain", View: views.FindByViewModel<RequestValueMainViewModel>()),
                            new ("RequestValueSecond", View: views.FindByViewModel<RequestValueSecondViewModel>(), DependsOn: "RequestValueMain"),
							#endregion

							#region Message Dialog
							new ("MessageDialog", View: views.FindByViewModel<MessageDialogViewModel>()),
                            new ("MyMessage", View: messageDialog),
							#endregion

							#region Modal Dialog
							new ("ModalDialog", View: views.FindByViewModel<ModalDialogViewModel>()),
                            new ("ModalDialogSecond", View: views.FindByView<ModalDialogSecondPage>()),
                            new ("ModalContentDialog", View: views.FindByView<ModalContentDialog>()),
                            new ("ComplexFlyout", View: views.FindByView<ComplexFlyoutPage>(), Nested:
                            [
                                new ("ComplexFlyoutOne", View: views.FindByViewModel<ComplexFlyoutOneViewModel>(), IsDefault:true),
                                new ("ComplexFlyoutSecond", View: views.FindByViewModel<ComplexFlyoutTwoViewModel>(), DependsOn: "ComplexFlyoutOne")
                            ]),
							#endregion

							#region ToFromQuery
							new ("ToFromQueryMain", View: views.FindByViewModel<ToFromQueryMainViewModel>()),
                            new ("ToFromQuery", View: views.FindByViewModel<ToFromQueryViewModel>(), DependsOn: "ToFromQueryMain"),
							#endregion

							#region Flyout Drawer
							new ("FlyoutDrawer", View: views.FindByView<FlyoutDrawerPage>(),
                                Nested:
                                [
                                    new ("First", View: views.FindByView<FirstPage>()),
                                    new ("Second", View: views.FindByView<SecondPage>()),
                                    new ("Third", View: views.FindByView<ThirdPage>())
                                ]),
                            new ("NavFlyout", View: views.FindByView<NavFlyout>()),
							#endregion
						]
                    )
#endif
                ]
            )
        );
    }
}
