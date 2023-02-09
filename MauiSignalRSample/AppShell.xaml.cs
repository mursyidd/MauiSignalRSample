using MauiSignalRSample.View;

namespace MauiSignalRSample;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
    }
}
