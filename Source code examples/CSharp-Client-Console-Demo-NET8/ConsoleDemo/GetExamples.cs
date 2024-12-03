using ConsoleDemo.ApiServices;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleDemo;

internal static class GetExamples
{
    public static async Task<string> RunGetDictionaries(IServiceProvider serviceProvider, CancellationToken ct)
    {
        var dictionaryApiService = serviceProvider.GetService<IDictionaryApi>();

        var result = await dictionaryApiService!.DictionaryGetAsync(ct: ct);

        return result.ToString();
    }
}