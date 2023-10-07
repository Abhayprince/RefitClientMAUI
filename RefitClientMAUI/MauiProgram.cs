using Microsoft.Extensions.Logging;
using Refit;
using RefitClientMAUI.Services;

namespace RefitClientMAUI;
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

        builder.Services.AddSingleton<IPlatformHttpMessageHandler>(_ =>
        {
#if ANDROID
            return new AndroidHttpMessageHandler();
#elif IOS
            return new IosHttpMessageHandler();
#endif
            return null;
        });

        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddSingleton<TokenService>();

        ConfigureRefit(builder.Services);

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    static void ConfigureRefit(IServiceCollection services)
    {
        services.AddRefitClient<IPublicApi>(ConfigureRefitSettings)
            .ConfigureHttpClient(SetHttpClient);

        services.AddRefitClient<IPrivateApi>(ConfigureRefitSettings)
            .ConfigureHttpClient(SetHttpClient);

        static RefitSettings ConfigureRefitSettings(IServiceProvider sp)
        {
            var messageHandler = sp.GetRequiredService<IPlatformHttpMessageHandler>();
            var tokenService = sp.GetRequiredService<TokenService>();
            return new RefitSettings
            {
                HttpMessageHandlerFactory = () => messageHandler.GetHttpMessageHandler(),
                AuthorizationHeaderValueGetter = (_, __) => Task.FromResult(tokenService.Token ?? string.Empty)
            };
        }

        static void SetHttpClient(HttpClient httpClient)
        {
            var baseUrl = DeviceInfo.Platform == DevicePlatform.Android
                               ? "https://10.0.2.2:7130"
                               : "https://localhost:7130";

            httpClient.BaseAddress = new Uri(baseUrl);
        }
    }
}
