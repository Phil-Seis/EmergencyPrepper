using Microsoft.Maui.Controls;

namespace EmergencyPrepper.Views;

public partial class GoBagPage : ContentPage
{
    private string _emergencyType;

    public GoBagPage(string emergencyType)
    {
        InitializeComponent();
        _emergencyType = emergencyType ?? "Unknown Emergency"; // Prevents null errors
        EmergencyLabel.Text = $"Preparing for: {_emergencyType}";
    }
}
