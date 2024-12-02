using ConsoleDemo.ApiClient;
using ConsoleDemo.Model;

namespace ConsoleDemo.ApiServices;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface IZLookupDataApi
{
    /// <summary>
    /// Get list of all Countries
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <returns>Task of List&lt;CountryContractV1&gt;</returns>
    Task<List<CountryContractV1>> CountryGetAsync(CancellationToken ct = default);

    /// <summary>
    /// Get list of available Languages
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <returns>Task of List&lt;LanguageContractV1&gt;</returns>
    Task<List<LanguageContractV1>> LanguageGetAsync(CancellationToken ct = default);

    /// <summary>
    /// Get list of all ReferenceDocuments
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <returns>Task of List&lt;ReferenceDocumentContractV1&gt;</returns>
    Task<List<ReferenceDocumentContractV1>> ReferenceDocumentGetAsync(CancellationToken ct = default);

    /// <summary>
    /// Get list of all units
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <returns>Task of List&lt;UnitContractV1&gt;</returns>
    Task<List<UnitContractV1>> UnitGetAsync(CancellationToken ct = default);
}

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class ZLookupDataApi(BsddApiClient bsddApiClient) : IZLookupDataApi
{
    /// <summary>
    /// Get list of all Countries 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <returns>Task of List&lt;CountryContractV1&gt;</returns>
    public async Task<List<CountryContractV1>> CountryGetAsync(CancellationToken ct = default)
    {
        var localVarPath = "/api/Country/v1";

        return await bsddApiClient.GetAsync<List<CountryContractV1>>(localVarPath, [], ct);
    }

    /// <summary>
    /// Get list of available Languages 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <returns>Task of List&lt;LanguageContractV1&gt;</returns>
    public async Task<List<LanguageContractV1>> LanguageGetAsync(CancellationToken ct = default)
    {
        var localVarPath = "/api/Language/v1";

        return await bsddApiClient.GetAsync<List<LanguageContractV1>>(localVarPath, [], ct);
    }

    /// <summary>
    /// Get list of all ReferenceDocuments 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <returns>Task of List&lt;ReferenceDocumentContractV1&gt;</returns>
    public async Task<List<ReferenceDocumentContractV1>> ReferenceDocumentGetAsync(CancellationToken ct = default)
    {
        var localVarPath = "/api/ReferenceDocument/v1";

        return await bsddApiClient.GetAsync<List<ReferenceDocumentContractV1>>(localVarPath, [], ct);
    }

    /// <summary>
    /// Get list of all units 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <returns>Task of List&lt;UnitContractV1&gt;</returns>
    public async Task<List<UnitContractV1>> UnitGetAsync(CancellationToken ct = default)
    {
        var localVarPath = "/api/Unit/v1";

        return await bsddApiClient.GetAsync<List<UnitContractV1>>(localVarPath, [], ct);
    }
}