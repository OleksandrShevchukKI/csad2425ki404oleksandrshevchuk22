using Microsoft.Extensions.Logging;

namespace RockPaperScissorsClient
{
    /// <summary>
    /// The main class for configuring and creating the MAUI application.
    /// </summary>
    public static class MauiProgram
    {
        /// <summary>
        /// Creates the MAUI application.
        /// </summary>
        /// <returns>The created MAUI application.</returns>
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

            return builder.Build();
        }
    }
}
