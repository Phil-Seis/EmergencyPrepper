using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace EmergencyPrepper.Views;

public partial class SplashPage : ContentPage
{
    //Animations in loading/spash screen
    private readonly string[] _imageSequence = { "apocalypse.png", "hurricane.png", "nuclear.png", "wildfire.png" };
    private int _currentImageIndex = 0;
    private bool _isAnimating = true;

    public SplashPage()
    {
        InitializeComponent();        
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        StartImageAnimation(); //Start animation after page has fully loaded
        StartLoading(); //Start the countdown to transition to MainPage
    }

    private async void StartImageAnimation()
    {
        while (_isAnimating)
        {
            if (RotatingImage != null)
            {
                //Fade out current image
                await RotatingImage.FadeTo(0, 500);

                //Change to the next image
                _currentImageIndex = (_currentImageIndex + 1) % _imageSequence.Length;
                RotatingImage.Source = _imageSequence[_currentImageIndex];

                //Fade in new image
                await RotatingImage.FadeTo(1, 500);
            }

            // Wait before changing again
            await Task.Delay(1000);
        }
    }

    private async void StartLoading()
    {
        await Task.Delay(8000); //Wait for 8 seconds. Chose this time to allow the user to see the rotating images and read the text.
        _isAnimating = false; //Stop animation

        //Navigate to MainPage
        Application.Current.MainPage = new NavigationPage(new MainPage());
    }
}
