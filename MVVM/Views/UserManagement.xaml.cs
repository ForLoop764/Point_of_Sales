using Microsoft.Maui.Controls;
using Point_of_Sales.MVVM.ViewModels;

namespace Point_of_Sales.MVVM.Views;

public partial class UserManagement : ContentPage
{
    public UserManagement()
    {
        InitializeComponent();
        BindingContext = new UserManagementViewModel();
    }
}