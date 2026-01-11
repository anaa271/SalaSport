using Microsoft.Extensions.Logging;
using SalaSportMAUI.Services;

#if ANDROID || IOS || MACCATALYST
using Plugin.LocalNotification;
#endif

namespace SalaSportMAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        var appBuilder = builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if ANDROID || IOS || MACCATALYST
        appBuilder.UseLocalNotification();
#endif

#if DEBUG
        builder.Logging.AddDebug();
#endif

#if ANDROID
        var baseUrl = "https://10.0.2.2:7267/";
#else
        // Windows / others: localhost normal
        var baseUrl = "https://localhost:7267/";
#endif

        builder.Services.AddSingleton(new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        });

        builder.Services.AddSingleton<MembersService>();
        builder.Services.AddSingleton<AppointmentsService>();
        builder.Services.AddSingleton<SubscriptionsService>();
        builder.Services.AddSingleton<TrainersService>();

        return builder.Build();
    }
}
