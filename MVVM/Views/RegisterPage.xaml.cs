namespace Point_of_Sales.MVVM.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new LoginPage();

    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        
    }
}