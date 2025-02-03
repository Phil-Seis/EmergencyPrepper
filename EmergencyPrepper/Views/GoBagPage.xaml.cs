using Microsoft.Maui.Controls;
using EmergencyPrepper.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmergencyPrepper.Views;

public partial class GoBagPage : ContentPage
{
    private readonly MainPage _mainPage;
    public ObservableCollection<ChecklistItem> ChecklistItems { get; set; }
    public ObservableCollection<string> HamFrequencies { get; set; }
    public ICommand CompleteChecklistCommand { get; set; }

    public GoBagPage(string emergencyType, MainPage mainPage)
    {
        InitializeComponent();
        _mainPage = mainPage;
        EmergencyLabel.Text = $"Preparing for: {emergencyType}";

        ChecklistItems = new ObservableCollection<ChecklistItem>();
        HamFrequencies = new ObservableCollection<string>();

        // Ensure BackgroundImage exists before setting it
        if (this.FindByName<Image>("BackgroundImage") is Image backgroundImage)
        {
            backgroundImage.Source = emergencyType switch
            {
                "Hurricane" => "hurricane.png",
                "Wildfire" => "wildfire.png",
                "Nuclear War" => "nuclear.png",
                "Zombie Apocalypse" => "apocalypse.png",
                _ => "default.png"
            };
        }

        // Load checklist based on emergency type
        if (emergencyType == "Nuclear War")
        {
            LoadNuclearWarChecklist();
        }
        else if (emergencyType == "Hurricane")
        {
            LoadHurricaneChecklist();
        }
        else if (emergencyType == "Wildfire")
        {
            LoadWildfireChecklist();
        }
        else if (emergencyType == "Zombie Apocalypse")
        {
            LoadZombieApocalypseChecklist();
        }

        CompleteChecklistCommand = new Command(() => CompleteChecklist(emergencyType));
        BindingContext = this;
    }

    private async void OnHamInfoClicked(object sender, EventArgs e)
    {
        Uri hamRadioUrl = new Uri("https://strykerradios.com/ham-radios/ham-radio-frequencies-common-uses-you-should-know/"); // Link to HAM radio info
        await Launcher.OpenAsync(hamRadioUrl);
    }


    private void LoadNuclearWarChecklist()
    {
        ChecklistItems.Add(new ChecklistItem { Name = "Gas Mask", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Medical Bag", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Iodine Tablets", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Water Supply (At least 2 weeks)", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Non-Perishable Food", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Radiation Detector", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Emergency Radio", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Extra Batteries", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Protective Clothing", IsChecked = false });
    }

    private void LoadHurricaneChecklist()
    {
        ChecklistItems.Add(new ChecklistItem { Name = "Emergency Weather Radio", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Flashlights with Extra Batteries", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Non-Perishable Food (3+ Days)", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Bottled Water (1 Gallon Per Person Per Day)", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "First Aid Kit", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Important Documents in a Waterproof Bag", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Cash (ATMs May Not Work)", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Portable Phone Charger", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Evacuation Plan & Contact List", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Rain Gear & Extra Clothing", IsChecked = false });
    }

    private void LoadWildfireChecklist()
    {
        ChecklistItems.Add(new ChecklistItem { Name = "Fire-Resistant Mask", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Evacuation Route Plan", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Emergency Go-Bag", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Fire Extinguisher", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Flashlights & Extra Batteries", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Portable Air Filter", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "First Aid Kit", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Emergency Radio", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Protective Clothing (Long Sleeves, Boots)", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Emergency Contact List", IsChecked = false });
    }

    private void LoadZombieApocalypseChecklist()
    {
        ChecklistItems.Add(new ChecklistItem { Name = "Baseball Bat or Melee Weapon", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Firearms & Ammunition", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Emergency Food & Water Supply", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "First Aid & Trauma Kit", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Gasoline & Backup Fuel", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Handheld Radio with Extra Batteries", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Survival Backpack", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Protective Clothing & Armor", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Escape Vehicle (Bike, Car, ATV)", IsChecked = false });
        ChecklistItems.Add(new ChecklistItem { Name = "Safehouse Plan", IsChecked = false });
    }

    private void LoadHamFrequencies()
    {
        HamFrequencies.Clear();
        HamFrequencies.Add("162.400 MHz - NOAA / National Weather Service");
        HamFrequencies.Add("27.185 MHz - Channel 19 on CB Radios");
        HamFrequencies.Add("462.675 MHz - GMRS emergency frequency");
        HamFrequencies.Add("157.100 MHz - Coast Guard Liaison");
        HamFrequencies.Add("155.160 MHz - Land Search and Rescue");
        HamFrequencies.Add("121.500 MHz - Air Search and Rescue");

        if (this.FindByName<ListView>("HamRadioFrequencies") is ListView hamListView)
        {
            hamListView.ItemsSource = HamFrequencies;
            hamListView.IsVisible = true;
        }
    }

    private void OnHamRadioToggled(object sender, ToggledEventArgs e)
    {
        if (this.FindByName<ListView>("HamRadioFrequencies") is ListView hamListView)
        {
            hamListView.IsVisible = e.Value;
            if (e.Value)
            {
                LoadHamFrequencies();
            }
        }
    }

    private async void CompleteChecklist(string emergencyType)
    {
        bool allChecked = ChecklistItems.All(item => item.IsChecked);

        if (allChecked)
        {
            await DisplayAlert("Checklist Complete!", "You have fully prepared for this disaster!", "OK");

            // Award the badge before returning to MainPage
            AwardBadge(emergencyType);

            // Navigate back to MainPage
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Checklist Incomplete", "Make sure you have all necessary items before proceeding.", "OK");
        }
    }

    private void AwardBadge(string emergencyType)
    {
        string badgeImage = emergencyType switch
        {
            "Hurricane" => "hurricanebadge.png",
            "Wildfire" => "wildfirebadge.png",
            "Nuclear War" => "nuclearbadge.png",
            "Zombie Apocalypse" => "zombiebadge.png",
            _ => null
        };

        if (badgeImage != null)
        {
            _mainPage.AddBadge(badgeImage);
        }
    }
}
