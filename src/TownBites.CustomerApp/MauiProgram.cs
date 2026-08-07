using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TownBites.CustomerApp.Configuration;
using TownBites.CustomerApp.Helpers;
using TownBites.CustomerApp.Interfaces;
using TownBites.CustomerApp.Services;
using TownBites.CustomerApp.ViewModels;
using TownBites.CustomerApp.Views;
using TownBites.CustomerApp.Views.Cart;
using TownBites.CustomerApp.Views.Checkout;
using TownBites.CustomerApp.Views.Home;
using TownBites.CustomerApp.Views.Menu;
using TownBites.CustomerApp.Views.Orders;
using TownBites.CustomerApp.Views.Restaurants;
namespace TownBites.CustomerApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // ViewModels
        builder.Services.AddTransient<LoginViewModel>();

        // Pages
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<RestaurantsPage>();

        // Shell
        builder.Services.AddSingleton<AppShell>();

        // JWT Handler
        builder.Services.AddTransient<AuthHttpMessageHandler>();

        // HttpClient
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
        builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
        var apiSettings = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>();
        builder.Services.AddHttpClient<IAuthApiService, AuthApiService>(client =>
        {
            client.BaseAddress = new Uri(apiSettings!.BaseUrl);
        })
        .AddHttpMessageHandler<AuthHttpMessageHandler>();
        builder.Services.AddHttpClient<IRestaurantService, RestaurantService>(client =>
        {
            client.BaseAddress = new Uri(apiSettings!.BaseUrl);
        })
        .AddHttpMessageHandler<AuthHttpMessageHandler>();

        builder.Services.AddTransient<RestaurantsViewModel>();
        builder.Services.AddTransient<RestaurantsPage>();
        builder.Services.AddHttpClient<IMenuService, MenuService>(client =>
            {
                client.BaseAddress = new Uri(apiSettings.BaseUrl);
            })
            .AddHttpMessageHandler<AuthHttpMessageHandler>();

        builder.Services.AddTransient<IMenuService, MenuService>();

        builder.Services.AddHttpClient<IMenuService, MenuService>(client =>
        {
            client.BaseAddress = new Uri(apiSettings.BaseUrl);
        })
        .AddHttpMessageHandler<AuthHttpMessageHandler>();

        builder.Services.AddTransient<MenuViewModel>();
        builder.Services.AddTransient<MenuPage>();

        builder.Services
            .AddHttpClient<IMenuService, MenuService>(client =>
            {
                client.BaseAddress = new Uri(apiSettings.BaseUrl);
            })
            .AddHttpMessageHandler<AuthHttpMessageHandler>();
        builder.Services.AddSingleton<ICartService, CartService>();

        builder.Services.AddTransient<CartViewModel>();
        builder.Services.AddTransient<CartPage>();
        builder.Services.AddHttpClient<IOrderService, OrderService>(client =>
        {
            client.BaseAddress = new Uri(apiSettings.BaseUrl);
        })
        .AddHttpMessageHandler<AuthHttpMessageHandler>();


        builder.Services.AddTransient<CheckoutViewModel>();
        builder.Services.AddTransient<CheckoutPage>();
        builder.Services.AddTransient<OrderSuccessViewModel>();
        builder.Services.AddTransient<OrderSuccessPage>();
        builder.Services.AddTransient<OrdersViewModel>();
        builder.Services.AddTransient<OrdersPage>();

#if DEBUG
        builder.Logging.AddDebug();
        #endif

        return builder.Build();
    }
}