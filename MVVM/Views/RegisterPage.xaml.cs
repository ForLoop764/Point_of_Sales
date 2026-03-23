using Point_of_Sales.MVVM.Models;
using Point_of_Sales.MVVM.Services;

namespace Point_of_Sales.MVVM.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    // ?? Create Account Button ?????????????????????????????????????????????????
    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text?.Trim();
        var password = PasswordEntry.Text;
        var confirm = ConfirmPasswordEntry.Text;

        // ?? Validation ????????????????????????????????????????????????????????
        if (string.IsNullOrWhiteSpace(username))
        {
            await DisplayAlert("Validation", "Username is required.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Validation", "Password is required.", "OK");
            return;
        }

        if (password.Length < 6)
        {
            await DisplayAlert("Validation", "Password must be at least 6 characters.", "OK");
            return;
        }

        if (password != confirm)
        {
            await DisplayAlert("Validation", "Passwords do not match.", "OK");
            return;
        }

        if (UserService.UsernameExists(username))
        {
            await DisplayAlert("Registration Failed",
                $"The username '{username}' is already taken. Please choose another.", "OK");
            return;
        }

        // ?? Register ??????????????????????????????????????????????????????????
        var model = new RegisterModel
        {
            Username = username,
            Password = password
        };

        bool success = UserService.Register(model);

        if (success)
        {
            await DisplayAlert("Account Created",
                $"Welcome, {username}! Your account has been created. Please sign in.", "OK");

            // Navigate back to Login
            Application.Current.MainPage = new LoginPage();
        }
        else
        {
            await DisplayAlert("Error", "Registration failed. Please try again.", "OK");
        }
    }

    // ?? Back to Login Button ??????????????????????????????????????????????????
    private void Button_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new LoginPage();
    }
}
