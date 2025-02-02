using Microsoft.Maui.Controls;
using System;

namespace EmergencyPrepper.Views;

public partial class MainPage : ContentPage
{
    private string _selectedEmergency;

    public MainPage()
    {
        InitializeComponent();
    }

    // Handle Picker Selection
    private void OnEmergencySelected(object sender, EventArgs e)
    {
        if (EmergencyPicker.SelectedIndex != -1)
        {
            _selectedEmergency = EmergencyPicker.Items[EmergencyPicker.SelectedIndex];
        }
    }

    // Handle Button Click
    private async void OnPrepareClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_selectedEmergency))
        {
            await DisplayAlert("Selection Required", "Please select an emergency before proceeding.", "OK");
            return;
        }

        // Ensure Navigation works
        await Navigation.PushAsync(new GoBagPage(_selectedEmergency));
    }
}
