using ConsoleDemo.ApiClient;
using ConsoleDemo.Model;

namespace ConsoleDemo.ApiServices;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface IPopularDictionaryApi
{
    /// <summary>
    /// Get list of popular Dictionaries
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <returns>Task of PopularDictionariesResponseContractV1</returns>
    Task<PopularDictionariesResponseContractV1> PopularDictionaryGetAsync (CancellationToken ct = default);
}

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class PopularDictionaryApi(BsddApiClient bsddApiClient) : IPopularDictionaryApi
{
    /// <summary>
    /// Get list of popular Dictionaries 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <returns>Task of ApiResponse (PopularDictionariesResponseContractV1)</returns>
    public async Task<PopularDictionariesResponseContractV1> PopularDictionaryGetAsync (CancellationToken ct = default)
    {
        var localVarPath = "/api/Dictionary/Popular/v1";

        // make the HTTP request
        return await bsddApiClient.GetAsync<PopularDictionariesResponseContractV1>(localVarPath, [], ct);
    }
}