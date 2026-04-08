using Point_of_Sales.MVVM.ViewModels;

namespace Point_of_Sales.MVVM.Views
{
    public partial class MoviePOSPage : ContentPage
    {
        public MoviePOSPage()
        {
            InitializeComponent();
            BindingContext = MoviePOSViewModel.Instance;
        }
    }
}