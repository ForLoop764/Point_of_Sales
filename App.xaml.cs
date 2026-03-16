using Point_of_Sales.MVVM.Views;
using Point_of_Sales.MVVM.Views;
namespace Point_of_Sales

{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new LoginPage());
        }
    }
}