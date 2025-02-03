using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace EmergencyPrepper.Views;

public partial class MainPage : ContentPage
{
    private string _selectedEmergency;
    public ObservableCollection<string> EmergencyList { get; set; }
    public ObservableCollection<string> EarnedBadges { get; set; }

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

        // Set Picker items manually
        EmergencyPicker.ItemsSource = EmergencyList;

        // Initialize Earned Badges List
        EarnedBadges = new ObservableCollection<string>();

        BindingContext = this;
    }    

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshBadges(); //Ensure badges load when returning to MainPage
    }

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

    private void OnEmergencySelected(object sender, EventArgs e)
    {
        if (EmergencyPicker.SelectedIndex != -1)
        {
            _selectedEmergency = EmergencyPicker.Items[EmergencyPicker.SelectedIndex];
        }
    }

    private async void OnPrepareClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_selectedEmergency))
        {
            await DisplayAlert("Selection Required", "Please select an emergency before proceeding.", "OK");
            return;
        }

        // Navigate to GoBagPage and pass the selected emergency
        await Navigation.PushAsync(new GoBagPage(_selectedEmergency, this));
    }
}
