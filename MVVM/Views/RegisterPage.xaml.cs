using Point_of_Sales.MVVM.ViewModels;

namespace Point_of_Sales.MVVM.Views;

public partial class RegisterPage : ContentPage
{
    private readonly RegisterViewModel _viewModel;

    public RegisterPage()
    {
        InitializeComponent();

        _viewModel = new RegisterViewModel();
        BindingContext = _viewModel;

        // Wire alert to ViewModel
        _viewModel.ShowAlert = (title, message, cancel) =>
            DisplayAlert(title, message, cancel);

        // Wire navigation to ViewModel
        _viewModel.GoToLogin = () =>
            Application.Current!.Windows[0].Page = new LoginPage();
    }

    // Show Password Checkbox
    private void OnShowPasswordCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        _viewModel.IsPasswordVisible = e.Value;
    }
}