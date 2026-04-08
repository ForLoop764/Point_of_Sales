using Point_of_Sales.MVVM.ViewModels;

namespace Point_of_Sales.MVVM.Views;

public partial class DashboardAdmin : ContentPage
{
    public DashboardAdmin()
    {
        InitializeComponent();
        BindingContext = new DashboardAdminViewModels();
    }

   
}