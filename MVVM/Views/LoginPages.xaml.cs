using Point_of_Sales.MVVM.ViewModels;

namespace Point_of_Sales.MVVM.Views;

public partial class LoginPage : ContentPage  
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new Login();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new UserManagement();
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Application.Current.MainPage = new RegisterPage();

    }
}