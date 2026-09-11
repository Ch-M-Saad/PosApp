using Microsoft.Extensions.Logging;
using PosApp.Data;
using PosApp.ViewModels;
using PosApp.Views;

namespace PosApp
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddSingleton<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<ProductsViewModel>();
            builder.Services.AddTransient<ProductsPage>();

            return builder.Build();
        }
    }
}
