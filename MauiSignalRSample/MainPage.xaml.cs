using MauiSignalRSample.View;
using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace MauiSignalRSample;

[QueryProperty(nameof(Name), "Username")]
[QueryProperty(nameof(Password), "Password")]

public partial class MainPage : ContentPage
{
    string name;
    string password;
    private readonly HubConnection _connection;
	private DateTime _startTime;

    public string Name
    {
        get => name;
        set
        {
            name = value;
            WelomeTxt.Text = $"Welcome {name.ToUpper()}!";
        }
    }

    public string Password
    {
        get => password;
        set
        {
            password = value;
            //WelomeTxt.Text = $"Welcome {name.ToUpper()} with password of {password.ToUpper()}!";
        }
    }

    public MainPage()
	{
        StringBuilder sb = new StringBuilder();
        _startTime = DateTime.Now;


		InitializeComponent();
		_connection = new HubConnectionBuilder()
			//.WithUrl(@"http://192.168.1.85:5296/chat")
			.WithUrl(@"http://desktop-2lcidp7:5296/chat")
			.Build();



		_connection.On<string>("MessageReceived", (message) =>
		{
			if (string.IsNullOrEmpty(sb.ToString()))
				sb.Append($"{message}");
			else
				sb.Append($"{Environment.NewLine}{message}");
			chatMessages.Text = sb.ToString();
        });

		Task.Run(() =>
		{
			Dispatcher.Dispatch(async () =>
			await _connection.StartAsync());


   //         Task hubConnection = _connection.StartAsync();
   //         Task timeout = Task.Delay(3000);

   //         Dispatcher.Dispatch(async () =>
   //         await Task.WhenAny(hubConnection, timeout));

			//if (_connection.State != HubConnectionState.Connected)
			//{
			//	return false;
			//}
			//else
			//	return true;

        });

		//Connectionlbl.Text = "Connection: -";
		//Connectionlbl.Text = string.Empty;
		//timer();
	}
	
    private async void timer()
	{
		while(true)
		{
            var elapsedTime = (DateTime.Now - _startTime);
            int secondsRemaining = (int)(elapsedTime.TotalMilliseconds) / 1000;

            if (_connection.State == HubConnectionState.Connected)
            {
                //Connectionlbl.Text = "Connection: CONNECTED";

            }
			else if (_connection.State == HubConnectionState.Connecting)
			{
                //Connectionlbl.Text = "Connection: ESTABLISHING CONNECTION";
            }
            else
			{
                //Connectionlbl.Text = "Connection: CONNECTION LOST";

				//if (secondsRemaining % 10 == 0)
				//{
				//	await Task.Run(() =>
				//	{
				//		//Dispatcher.Dispatch(async () =>
				//		//await _connection.StartAsync());


				//		Task hubConnection = _connection.StartAsync();
				//		Task timeout = Task.Delay(3000);

				//		Dispatcher.Dispatch(async () =>
				//		await Task.WhenAny(hubConnection, timeout));

				//		if (_connection.State != HubConnectionState.Connected)
				//		{
				//			return false;
				//		}
				//		else
				//			return true;
				//	});
				//}
            }

            await Task.Delay(1000);
		}
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		await _connection.InvokeCoreAsync("SendMessage", args: new[] { myChatMessage.Text });

		myChatMessage.Text = String.Empty;
	}

	private async void onExitClicked(object sender, EventArgs e)
	{
		try
		{
			Application.Current.Quit();
			//await Shell.Current.GoToAsync($"LoginPage");
			//await Shell.Current.GoToAsync(nameof(LoginPage), true);
			//await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}
		catch (Exception)
		{

		}
	}

    private async void chatMessages_TextChanged(object sender, TextChangedEventArgs e)
    {
        await scrollView.ScrollToAsync(chatMessages, ScrollToPosition.End, true);
    }
}