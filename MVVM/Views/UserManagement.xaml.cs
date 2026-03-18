namespace Point_of_Sales.MVVM.Views;

public partial class UserManagement : ContentPage
{
    public UserManagement()
    {
        InitializeComponent();
    }

    private void OnAddUserClicked(object sender, EventArgs e)
    {
        DisplayAlert("Add User", "Open Add User form here.", "OK");
    }

    private void OnEditUserClicked(object sender, EventArgs e)
    {
        DisplayAlert("Edit User", "Open Edit User form here.", "OK");
    }

    private void OnDeleteUserClicked(object sender, EventArgs e)
    {
        DisplayAlert("Delete User", "Are you sure you want to delete this user?", "Yes", "No");
    }
}