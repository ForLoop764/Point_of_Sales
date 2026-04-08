using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Point_of_Sales.MVVM.Services;

namespace Point_of_Sales.MVVM.ViewModels
{
    public class LoginViewModels : INotifyPropertyChanged
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

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(); }
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

        public bool IsPasswordHidden => !_isPasswordVisible;

        //  Commands 
        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }
        public ICommand GoToForgotCommand { get; }

        //  Actions wired from View 
        public Func<string, string, string, Task>? ShowAlert { get; set; }
        public Action? GoToRegister { get; set; }
        public Action? GoToForgot { get; set; }
        public Action? GoToDashboard { get; set; }

        public LoginViewModels()
        {
            LoginCommand = new Command(async () => await LoginAsync());
            GoToRegisterCommand = new Command(() => GoToRegister?.Invoke());
            GoToForgotCommand = new Command(() => GoToForgot?.Invoke());
        }

        //  Login Logic 
        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await ShowAlert!("Login Failed", "Please enter your username and password.", "OK");
                return;
            }

            IsBusy = true;

            try
            {
               

                // Check regular user
                var user = UserService.ValidateLogin(Username, Password);
                if (user != null)
                {
                    GoToDashboard?.Invoke();
                }
                else
                {
                    await ShowAlert!("Login Failed",
                        "Incorrect username or password. Please try again.", "OK");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}