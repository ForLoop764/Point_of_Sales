namespace Point_of_Sales.MVVM.Views;

public partial class ForgotPasswordPage : ContentPage
{
    public ForgotPasswordPage()
    {
        InitializeComponent();
    }

    //  Send Reset Link 
    private async void OnSendClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text?.Trim();

        // Validate
        if (string.IsNullOrWhiteSpace(email))
        {
            ShowEmailError("Email address is required.");
            return;
        }

        if (!email.Contains('@') || !email.Contains('.'))
        {
            ShowEmailError("Please enter a valid email address.");
            return;
        }

        ClearEmailError();

        // Show loading
        SendButton.IsVisible = false;
        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;
        EmailEntry.IsEnabled = false;

        try
        {
            // Simulate API call — replace with your real auth service
            await Task.Delay(1500);

            // TODO: await _authService.SendPasswordResetEmailAsync(email);

            // Show success
            ConfirmationLabel.Text = $"A reset link was sent to {email}";
            EmailCard.IsVisible = false;
            SuccessCard.IsVisible = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
            SendButton.IsVisible = true;
            EmailEntry.IsEnabled = true;
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }
    }

    // Try Again 
    private void OnTryAgainClicked(object sender, EventArgs e)
    {
        EmailEntry.Text = string.Empty;
        ClearEmailError();
        EmailEntry.IsEnabled = true;
        SuccessCard.IsVisible = false;
        EmailCard.IsVisible = true;
        SendButton.IsVisible = true;
    }

    // Back to Login 
    private async void OnSignInClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new LoginPage();
    }

    //Helpers 
    private void ShowEmailError(string message)
    {
        EmailErrorLabel.Text = message;
        EmailErrorLabel.IsVisible = true;
        EmailBorder.Stroke = new SolidColorBrush(Color.FromArgb("#E53935"));
    }

    private void ClearEmailError()
    {
        EmailErrorLabel.IsVisible = false;
        EmailErrorLabel.Text = string.Empty;
        EmailBorder.Stroke = new SolidColorBrush(Color.FromArgb("#E0E0E0"));
    }
}
