using Point_of_Sales.MVVM.Models;
using Point_of_Sales.MVVM.Services;

namespace Point_of_Sales.MVVM.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    // Show Password Checkbox
    private void OnShowPasswordCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        PasswordEntry.IsPassword        = !e.Value;
        ConfirmPasswordEntry.IsPassword = !e.Value;
    }

    // Create Account Button
    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text?.Trim();
        var email    = EmailEntry.Text?.Trim();
        var password = PasswordEntry.Text;
        var confirm  = ConfirmPasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username))
        {
            await DisplayAlert("Validation", "Username is required.", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Validation", "Email is required.", "OK");
            return;
        }
        if (!IsValidEmail(email))
        {
            await DisplayAlert("Validation", "Please enter a valid email address (e.g. example@gmail.com).", "OK");
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
            Application.Current!.Windows[0].Page = new LoginPage();
        }
        else
        {
            await DisplayAlert("Error", "Registration failed. Please try again.", "OK");
        }
    }

    // Email validation helper
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    // Back to Login Button
    private void Button_Clicked(object sender, EventArgs e)
    {
        Application.Current!.Windows[0].Page = new LoginPage();
    }
}
