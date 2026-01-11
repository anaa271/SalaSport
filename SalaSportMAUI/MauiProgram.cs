using Microsoft.Extensions.Logging;
using SalaSportMAUI.Services;

namespace SalaSportMAUI;

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

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton(new HttpClient
        {
            BaseAddress = new Uri("https://10.0.2.2:7267/api/")
        });

        builder.Services.AddSingleton<MembersService>();
        builder.Services.AddSingleton<AppointmentsService>();
        builder.Services.AddSingleton<SubscriptionsService>();
        builder.Services.AddSingleton<TrainersService>();

        return builder.Build();
    }
}
