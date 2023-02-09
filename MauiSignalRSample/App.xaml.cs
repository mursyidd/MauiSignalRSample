using MauiSignalRSample.Database;
using MauiSignalRSample.View;

namespace MauiSignalRSample;

public partial class App : Application
{
    //ConfigurationProviderService m_configProvider;
    public App()
	{
        //m_configProvider = configProvider;
        InitializeComponent();

        //LoginPage = new NavigationPage(new LoginPage());
        MainPage = new AppShell();
        //MainPage = new LoginPage();
    }

    protected override void OnStart()
    {
        //var task = InitAsync();

        //task.ContinueWith((task) =>
        //{
        //    MainThread.BeginInvokeOnMainThread(() =>
        //    {
        //        MainPage = new AppShell();

        //        // Choose navigation depending on init
        //        Shell.Current.GoToAsync(nameof(LoginPage));
        //    });
        //});

        //base.OnStart();
    }

    private async Task InitAsync()
    {
        //await m_configProvider.InitAsync();
    }


    static LoginDatabase database;

    public static LoginDatabase Database
    {
        get
        {
            if (database == null)
            {
                // UserName = admin, Password = 1234
                database = new LoginDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SQLiteSample.db"));
            }
            return database;
        }
    }
}
