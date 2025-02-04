using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace EmergencyPrepper.Views;

public partial class MainPage : ContentPage
{
    private string _selectedEmergency;
    public ObservableCollection<string> EmergencyList { get; set; }//List of emergency names
    public ObservableCollection<string> EarnedBadges { get; set; } //List of earned badges

    public MainPage()
    {
        InitializeComponent();        
        
        // Initialize the emergency options
        EmergencyList = new ObservableCollection<string>
        {
            "Hurricane",
            "Wildfire",
            "Nuclear War",
            "Zombie Apocalypse"
        };

        //Set Picker items
        EmergencyPicker.ItemsSource = EmergencyList;

        //Initialize Earned Badges List
        EarnedBadges = new ObservableCollection<string>();

        //Bind UI elements in XAML to this code-behind
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshBadges(); //Ensure badges load when returning to MainPage
    }

    //method to add badges
    public void AddBadge(string badgeImage)
    {
        if (!EarnedBadges.Contains(badgeImage))
        {
            EarnedBadges.Add(badgeImage);
            RefreshBadges(); //Refresh the badge container
        }
    }

    private void RefreshBadges()
    {
        if (BadgesContainer == null) return; //Avoid errors if BadgesContainer is missing

        // Clear old badges
        BadgesContainer.Children.Clear();

        foreach (var badge in EarnedBadges)
        {
            BadgesContainer.Children.Add(new Image
            {
                Source = badge,
                WidthRequest = 80,
                HeightRequest = 80
            });
        }
    }

    //Event handler for when an emergency is selected
    private void OnEmergencySelected(object sender, EventArgs e)
    {
        if (EmergencyPicker.SelectedIndex != -1)
        {
            _selectedEmergency = EmergencyPicker.Items[EmergencyPicker.SelectedIndex];
        }
    }

    //Event handler for when the Prepare button is clicked
    private async void OnPrepareClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_selectedEmergency))
        {
            await DisplayAlert("Selection Required", "Please select an emergency before proceeding.", "OK");
            return;
        }

        //Navigate to GoBagPage and pass the selected emergency
        await Navigation.PushAsync(new GoBagPage(_selectedEmergency, this));
    }

    //Event handler for GitHub button
    private async void OnGitHubClicked(object sender, EventArgs e)
    {
        Uri githubUrl = new Uri("https://github.com/Phil-Seis");
        await Launcher.OpenAsync(githubUrl);
    }

    //Event handler for LinkedIn button
    private async void OnLinkedInClicked(object sender, EventArgs e)
    {
        Uri linkedInUrl = new Uri("https://www.linkedin.com/in/philseis/");
        await Launcher.OpenAsync(linkedInUrl);
    }

    //Event handler for information page popup
    private async void OnPageInfoClicked(object sender, EventArgs e)
    {
        await DisplayAlert("***About this application***",
            "EMERGENCY PREPPER APP\n" +
            "by Philip Seisman\n\n" +
            "Hey everyone! This application was originally designed to help users prepare for various emergencies with a checklist-based system. Now, although this application is fully functional, there are additional features that will be added in the future! Your goal is to collect all of the badges to become fully prepared for any emergency! Good Luck!\n\n" +
            "Frameworks Used:\n" +
            "- .NET MAUI\n" +
            "- C#\n\n" +
            "Upcoming features:\n"+
            "- Login and password data storage\n" +
            "- MVVM Architecture\n\n" +
            "Version: 1.0",
            "OK");
    }
}
