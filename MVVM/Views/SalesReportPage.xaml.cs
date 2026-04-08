using Point_of_Sales.MVVM.ViewModels;

namespace Point_of_Sales.MVVM.Views
{
    public partial class SalesReportPage : ContentPage
    {
        public SalesReportPage()
        {
            InitializeComponent();
            BindingContext = MoviePOSViewModel.Instance; // ? same shared instance
        }
    }
}