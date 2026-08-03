using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TownBites.CustomerApp.Configuration;
using TownBites.CustomerApp.Interfaces;
using TownBites.CustomerApp.Services;
using TownBites.CustomerApp.ViewModels;
using TownBites.CustomerApp.Views;
using TownBites.CustomerApp.Views.Home;
namespace TownBites.CustomerApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            
            builder.UseMauiApp<App>().UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
            builder.Services.AddHttpClient<IAuthApiService, AuthApiService>(client =>
            {
                #if ANDROID
                                client.BaseAddress = new Uri("https://10.0.2.2:5001/");
                #else
                       client.BaseAddress = new Uri("https://localhost:7276/");
                #endif
            });
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<HomePage>();
#if DEBUG
            builder.Logging.AddDebug();
            #endif

            return builder.Build();
        }
    }
}
