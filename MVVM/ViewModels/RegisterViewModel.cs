using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Point_of_Sales.MVVM.Models;
using Point_of_Sales.MVVM.Services;

namespace Point_of_Sales.MVVM.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        // ── Properties ──
        private string _username = string.Empty;
        public string Username
        {   
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        private string _confirmPassword = string.Empty;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        private bool _isPasswordVisible = false;
        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set
            {
                _isPasswordVisible = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsPasswordHidden));
            }
        }

        // Inverse for IsPassword binding
        public bool IsPasswordHidden => !_isPasswordVisible;

        // ── Commands ──
        public ICommand RegisterCommand { get; }
        public ICommand GoToLoginCommand { get; }
        public ICommand TogglePasswordCommand { get; }

        // ── Actions wired from View ──
        public Func<string, string, string, Task>? ShowAlert { get; set; }
        public Action? GoToLogin { get; set; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(async () => await RegisterAsync());
            GoToLoginCommand = new Command(() => GoToLogin?.Invoke());
            TogglePasswordCommand = new Command(() => IsPasswordVisible = !IsPasswordVisible);
        }

        // ── Register Logic ──
        private async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                await ShowAlert!("Validation", "Username is required.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Email))
            {
                await ShowAlert!("Validation", "Email is required.", "OK");
                return;
            }
            if (!IsValidEmail(Email))
            {
                await ShowAlert!("Validation", "Please enter a valid email address (e.g. example@gmail.com).", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                await ShowAlert!("Validation", "Password is required.", "OK");
                return;
            }
            if (Password.Length < 6)
            {
                await ShowAlert!("Validation", "Password must be at least 6 characters.", "OK");
                return;
            }
            if (Password != ConfirmPassword)
            {
                await ShowAlert!("Validation", "Passwords do not match.", "OK");
                return;
            }
            if (UserService.UsernameExists(Username))
            {
                await ShowAlert!("Registration Failed",
                    $"The username '{Username}' is already taken. Please choose another.", "OK");
                return;
            }

            var model = new RegisterModel
            {
                Username = Username,
                Email = Email,
                Password = Password
            };

            bool success = UserService.Register(model);

            if (success)
            {
                await ShowAlert!("Account Created",
                    $"Welcome, {Username}! Your account has been created. Please sign in.", "OK");
                GoToLogin?.Invoke();
            }
            else
            {
                await ShowAlert!("Error", "Registration failed. Please try again.", "OK");
            }
        }

        // ── Email Validation ──
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
    }
}