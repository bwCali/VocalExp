using MoMo4.Platforms;
using Microsoft.Extensions.Logging;



namespace MoMo4;

public static class MauiProgram
{
    public static IServiceProvider Services { get; set; }
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

        builder.Services.AddTransient<MainPage>();
        

#if DEBUG
        builder.Logging.AddDebug();
#endif
       

#if ANDROID || IOS || MACCATALYST || WINDOWS
        builder.Services.AddSingleton<ISpeechToText, SpeechToTextImplementation>();
#endif

        var app = builder.Build();
        Services = app.Services;

        return app;
    }
}
