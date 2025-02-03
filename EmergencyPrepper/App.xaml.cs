using EmergencyPrepper.Views;
using Plugin.Maui.Audio;

namespace EmergencyPrepper
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();            

            //Start with the splash screen
            MainPage = new NavigationPage(new SplashPage());
        }
    }
}