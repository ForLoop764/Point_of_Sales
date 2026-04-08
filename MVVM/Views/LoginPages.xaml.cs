using Point_of_Sales.MVVM.ViewModels;
using Point_of_Sales.MVVM.Views;

namespace Point_of_Sales.MVVM.Views;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModels _viewModel;

    public LoginPage()
    {
        InitializeComponent();

        _viewModel = new LoginViewModels();
        BindingContext = _viewModel;

        // Show alert
        _viewModel.ShowAlert = (title, message, cancel) =>
            DisplayAlert(title, message, cancel);

        // Navigate to Register
        _viewModel.GoToRegister = () =>
            Application.Current!.Windows[0].Page = new RegisterPage();

        // Navigate to Forgot Password
        _viewModel.GoToForgot = () =>
            Application.Current!.Windows[0].Page = new ForgotPasswordPage();

        // ?Navigate to TABBED ADMIN DASHBOARD
        _viewModel.GoToDashboard = () =>
            Application.Current!.Windows[0].Page = new AdminTabbedPage();
    }

    // Show Password Toggle
    private void OnShowPasswordCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        _viewModel.IsPasswordVisible = e.Value;
    }
}