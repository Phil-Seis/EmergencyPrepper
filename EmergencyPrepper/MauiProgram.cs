//Entry point of my program
//Sets up app's services and dependencies as well as fonts and logging before the launch
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

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
                //configure the fonts for the app. I added a few extra custom fonts.
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

                //I added this when I was fumbling around trying to get background audio to work. Not necessary right now but wanted to save for future.
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