namespace Point_of_Sales.MVVM.Views;

public partial class AdminProfilePage : ContentPage
{
	public AdminProfilePage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Application.Current.MainPage = new LoginPage();
    
    }
}