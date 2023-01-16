using GymManagerApp.Models;

namespace GymManagerApp;

public partial class Login : ContentPage
{
	public Login()
	{
		BindingContext = new LoginData { UserName = "trainer@test.com", Password= "123Qwe!" };
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		var loginData = (LoginData)BindingContext;
		if (loginData.UserName != null && loginData.Password != null)
		{
			var token = await App.Database.LoginAsync(loginData);
			if (token != null)
			{
				App.Database.SetToken(token);
				Application.Current.MainPage = new AppShell();
			}
		}
    }
}