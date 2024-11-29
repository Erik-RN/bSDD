using Microsoft.Extensions.Configuration;

namespace ConsoleDemo.Settings;

public static class AppSettingsReader
{
    public static T GetAppSettings<T>(string baseSection = "", bool errorOnUnknown = true) where T : class
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        return GetAppSettings<T>(config, baseSection, errorOnUnknown);
    }

    public static T GetAppSettings<T>(IConfiguration configuration, string baseSection = "", bool errorOnUnknown = true) where T : class
    {
        var settings = Activator.CreateInstance<T>();

        configuration.GetSection(baseSection).Bind(settings, options => options.ErrorOnUnknownConfiguration = errorOnUnknown);
        return settings;
    }
}