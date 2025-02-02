using EmergencyPrepper.Views;

namespace EmergencyPrepper
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Ensure the app starts with a NavigationPage
            MainPage = new NavigationPage(new SplashPage());
        }
    }
}