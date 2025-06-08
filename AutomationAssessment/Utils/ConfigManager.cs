using Microsoft.Extensions.Configuration;

namespace AutomationAssessment.Utils
{
    public static class ConfigManager
    {
        private static IConfigurationRoot config;

        static ConfigManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            config = builder.Build();
        }

        public static string Browser => config["Browser"] ?? "chromium";
        public static string BaseUrl => config["BaseUrl"] ?? "https://demoqa.com/automation-practice-form";
        public static string ApiBaseUrl => config["ApiBaseUrl"] ?? "https://reqres.in";
        public static bool Headless => bool.TryParse(config["Headless"], out var headless) && headless;
    }
}
