using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using ConsoleDemo.ApiClient;
using ConsoleDemo.ApiServices;
using ConsoleDemo.Settings;

namespace ConsoleDemo.DI;

internal static class ServiceProviderHelper
{
    public static IServiceProvider CreateServiceProvider(AppSettings appSettings)
    {
        var services = new ServiceCollection();
        services.AddHttp();
        services.AddBsddApiServices();

        services.AddSingleton(appSettings.BsddApi);

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }

    private static void AddHttp(this ServiceCollection services)
    {
        services.AddHttpClient();

        services.AddHttpClient(BsddApiClient.ApiHttpClientName, client =>
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }).AddStandardResilienceHandler();
    }

    private static void AddBsddApiServices(this ServiceCollection services)
    {
        services.AddScoped<IClassApi, ClassApi>();
        services.AddScoped<IDictionaryApi, DictionaryApi>();
        services.AddScoped<IDictionaryUpdateApi, DictionaryUpdateApi>();
        services.AddScoped<IPopularDictionaryApi, PopularDictionaryApi>();
        services.AddScoped<IPrivateDictionarySpecificAPIsApi, PrivateDictionarySpecificAPIsApi>();
        services.AddScoped<IPropertyApi, PropertyApi>();
        services.AddScoped<ISearchApi, SearchApi>();
        services.AddScoped<IZLookupDataApi, ZLookupDataApi>();
    }
}