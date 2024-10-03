using ERozaniec.Data;
using ERozaniec.Pages;
using Microsoft.Extensions.Logging;

namespace ERozaniec
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IDatabaseService, DatabaseService>(_ => new("app.db"));
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<RosaryPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}