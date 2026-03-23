using Point_of_Sales.MVVM.ViewModels;

namespace Point_of_Sales.MVVM.Views;

public partial class LoginPage : ContentPage
{
    private Login _viewModel;

    public LoginPage()
    {
        InitializeComponent();
        _viewModel = new Login();
        BindingContext = _viewModel;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            DisplayAlert("Error", "Please enter your username and password.", "OK");
            return;
        }

        if (username == "Admin" && password == "Admin123")
        {
            Application.Current.MainPage = new UserManagement();
        }
        else
        {
            DisplayAlert("Login Failed", "Invalid username or password.", "OK");
        }
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Application.Current.MainPage = new RegisterPage();
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        Application.Current.MainPage = new ForgotPasswordPage();
    }
}