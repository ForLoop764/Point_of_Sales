using Microsoft.Maui.Controls;
using Point_of_Sales.MVVM.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Point_of_Sales.MVVM.ViewModels
{
    public class UserManagementViewModel : BindableObject
    {
        public ObservableCollection<User> Users { get; set; } = new();

        public ICommand AddUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand EditUserCommand { get; }

        public UserManagementViewModel()
        {
            // Sample Data
            Users.Add(new User { Name = "Edge Flores", Role = "Admin", Status = "Active" });
            Users.Add(new User { Name = "Samler Telebangco", Role = "Cashier", Status = "Active" });

            AddUserCommand = new Command(async () => await AddUser());
            DeleteUserCommand = new Command<User>(DeleteUser);
            EditUserCommand = new Command<User>(async (user) => await EditUser(user));
        }

        // ➕ ADD USER WITH ROLE + STATUS
        private async Task AddUser()
        {
            string name = await Application.Current.MainPage.DisplayPromptAsync(
                "New User", "Enter name:");

            if (string.IsNullOrWhiteSpace(name)) return;

            // 🔽 Role Selection
            string role = await Application.Current.MainPage.DisplayActionSheet(
                "Select Role", "Cancel", null,
                "Admin", "Manager", "Cashier", "Staff");

            if (role == "Cancel") return;

            // 🔽 Status Selection
            string status = await Application.Current.MainPage.DisplayActionSheet(
                "Select Status", "Cancel", null,
                "Active", "Inactive", "Pending");

            if (status == "Cancel") return;

            Users.Add(new User
            {
                Name = name,
                Role = role,
                Status = status
            });
        }

        // ❌ DELETE USER
        private void DeleteUser(User user)
        {
            if (user == null) return;

            Application.Current.MainPage.DisplayAlert(
                "Delete",
                $"Delete {user.Name}?",
                "Yes",
                "No"
            ).ContinueWith(t =>
            {
                if (t.Result)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Users.Remove(user);
                    });
                }
            });
        }

        // EDIT USER (NAME + ROLE + STATUS)
        private async Task EditUser(User user)
        {
            if (user == null) return;

            // 1️⃣ Edit Name
            string newName = await Application.Current.MainPage.DisplayPromptAsync(
                "Edit Name",
                "Enter new name:",
                initialValue: user.Name);

            if (string.IsNullOrWhiteSpace(newName))
                return;

            user.Name = newName;

            // 2️⃣ Edit Role
            string role = await Application.Current.MainPage.DisplayActionSheet(  
                "Select Role",
                "Cancel",
                null,
                "Admin", "Manager", "Cashier", "Staff");

            if (role == "Cancel" || string.IsNullOrEmpty(role))
                return;

            user.Role = role;

            // 3️⃣ Edit Status
            string status = await Application.Current.MainPage.DisplayActionSheet(
                "Select Status",
                "Cancel",
                null,
                "Active", "Inactive", "Pending");

            if (status == "Cancel" || string.IsNullOrEmpty(status))
                return;

            user.Status = status;
        }
    }
}