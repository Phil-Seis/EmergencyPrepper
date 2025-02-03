using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Plugin.Maui.Audio;

namespace EmergencyPrepper
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()                
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("VIRUST.ttf", "VIRUST");
                    fonts.AddFont("Darkmode.ttf", "Darkmode");
                    fonts.AddFont("Colleged.ttf", "Colleged");
                    fonts.AddFont("DoctorGlitch.otf", "DoctorGlitch");
                    fonts.AddFont("Infected.ttf", "Infected");
                })

                .ConfigureMauiHandlers(handlers =>
                 {
                     handlers.AddHandler(typeof(Plugin.Maui.Audio.AudioManager), typeof(Plugin.Maui.Audio.AudioManager));
                 });

            builder.Services.AddSingleton(Plugin.Maui.Audio.AudioManager.Current);


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}