using System.Collections.ObjectModel;
using ConsoleDemo.ApiClient;
using ConsoleDemo.Model;

namespace ConsoleDemo.ApiServices;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface IPropertyApi
{
    /// <summary>
    /// Get list of classes that uses the property (paginated)
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="propertyUri">URI of the property, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.1/prop/height</param>
    /// <param name="internalExternal">Option to get only classes of same dictionary as the property (internal, default), only classes of other dictionaries (external) or all classes (all) (optional)</param>
    /// <param name="searchText">Search text to filter classes (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <param name="languageCode">Specify language (case-sensitive). For those items the text is not available in the requested language, the text will be returned in the default language of the dictionary (optional)</param>
    /// <returns>Task of PropertyClassesContractV1</returns>
    Task<PropertyClassesContractV1> PropertyClassesGetAsync (string propertyUri, InternalExternalOptionV1 internalExternal, string? searchText = null, int? offset = null, int? limit = null, string? languageCode = null, CancellationToken ct = default);

    /// <summary>
    /// Get Property details
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="uri">URI of the property, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.1/prop/color</param>
    /// <param name="includeClasses">Set to true to get list of classes where property is used (only classes of the same dictionary as the property).              Maximum number of class properties returned is 2000. In the next version of the API this option probably will be removed.              Preferred way to get the classes is by using api/Property/Classes/v1 (optional)</param>
    /// <param name="languageCode">Specify language (case-sensitive). For those items the text is not available in the requested language, the text will be returned in the default language of the dictionary (optional)</param>
    /// <returns>Task of PropertyContractV4</returns>
    Task<PropertyContractV4> PropertyGetAsync (string uri, bool includeClasses, string? languageCode = null, CancellationToken ct = default);

    /// <summary>
    /// Get property relations or reverse relations (paginated)
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="propertyUri">URI of the property, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.1/prop/height</param>
    /// <param name="getReverseRelations">Get either the forward or the reverse relations</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <param name="languageCode">Specify language (case-sensitive). For those items the text is not available in the requested language, the text will be returned in the default language of the dictionary (optional)</param>
    /// <returns>Task of PropertyRelationsContractV1</returns>
    Task<PropertyRelationsContractV1> PropertyRelationsGetAsync (string propertyUri, bool getReverseRelations, int? offset = null, int? limit = null, string? languageCode = null, CancellationToken ct = default);

    /// <summary>
    /// Get Property Value details
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="uri">URI of the property value, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.1/prop/color/value/red</param>
    /// <param name="languageCode">Language Code (optional)</param>
    /// <returns>Task of PropertyValueContractV4</returns>
    Task<PropertyValueContractV4> PropertyValueGetAsync (string uri, string? languageCode = null, CancellationToken ct = default);
}

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class PropertyApi(BsddApiClient bsddApiClient) : IPropertyApi
{
    /// <summary>
    /// Get list of classes that uses the property (paginated) 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="propertyUri">URI of the property, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.1/prop/height</param>
    /// <param name="internalExternal">Option to get only classes of same dictionary as the property (internal, default), only classes of other dictionaries (external) or all classes (all) (optional)</param>
    /// <param name="searchText">Search text to filter classes (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <param name="languageCode">Specify language (case-sensitive). For those items the text is not available in the requested language, the text will be returned in the default language of the dictionary (optional)</param>
    /// <returns>Task of ApiResponse (PropertyClassesContractV1)</returns>
    public async Task<PropertyClassesContractV1> PropertyClassesGetAsync(string propertyUri, InternalExternalOptionV1 internalExternal, string? searchText = null, int? offset = null, int? limit = null, string? languageCode = null, CancellationToken ct = default)
    {
        // verify the required parameter 'propertyUri' is set
        if (string.IsNullOrWhiteSpace(propertyUri))
            throw new BsddApiException(400, "Missing required parameter 'propertyUri' when calling PropertyApi->PropertyClassesGet");

        var localVarPath = "/api/Property/Classes/v1";
        var localVarQueryParams = new List<KeyValuePair<string, string?>>();

        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("PropertyUri", propertyUri)); // query parameter
        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("InternalExternal", internalExternal)); // query parameter
        if (searchText != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("SearchText", searchText)); // query parameter
        if (offset != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Offset", offset)); // query parameter
        if (limit != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Limit", limit)); // query parameter
        if (languageCode != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("languageCode", languageCode)); // query parameter

        // make the HTTP request
        return await bsddApiClient.GetAsync<PropertyClassesContractV1>(localVarPath, localVarQueryParams, ct);
    }

    /// <summary>
    /// Get Property details 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="uri">URI of the property, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.1/prop/color</param>
    /// <param name="includeClasses">Set to true to get list of classes where property is used (only classes of the same dictionary as the property).              Maximum number of class properties returned is 2000. In the next version of the API this option probably will be removed.              Preferred way to get the classes is by using api/Property/Classes/v1 (optional)</param>
    /// <param name="languageCode">Specify language (case-sensitive). For those items the text is not available in the requested language, the text will be returned in the default language of the dictionary (optional)</param>
    /// <returns>Task of ApiResponse (PropertyContractV4)</returns>
    public async Task<PropertyContractV4> PropertyGetAsync(string uri, bool includeClasses, string? languageCode = null, CancellationToken ct = default)
    {
        // verify the required parameter 'uri' is set
        if (string.IsNullOrWhiteSpace(uri))
            throw new BsddApiException(400, "Missing required parameter 'uri' when calling PropertyApi->PropertyGet");

        var localVarPath = "/api/Property/v4";
        var localVarQueryParams = new List<KeyValuePair<string, string?>>();
        
        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("uri", uri)); // query parameter
        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("includeClasses", includeClasses)); // query parameter
        if (languageCode != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("languageCode", languageCode)); // query parameter

        // make the HTTP request
        return await bsddApiClient.GetAsync<PropertyContractV4>(localVarPath, localVarQueryParams, ct);
    }

    /// <summary>
    /// Get property relations or reverse relations (paginated) 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="propertyUri">URI of the property, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.1/prop/height</param>
    /// <param name="getReverseRelations">Get either the forward or the reverse relations</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <param name="languageCode">Specify language (case-sensitive). For those items the text is not available in the requested language, the text will be returned in the default language of the dictionary (optional)</param>
    /// <returns>Task of PropertyRelationsContractV1</returns>
    public async Task<PropertyRelationsContractV1> PropertyRelationsGetAsync(string propertyUri, bool getReverseRelations, int? offset = null, int? limit = null, string? languageCode = null, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(propertyUri))
            throw new BsddApiException(400, "Missing required parameter 'propertyUri' when calling PropertyApi->PropertyRelationsGet");

        var localVarPath = "/api/Property/Relations/v1";
        var localVarQueryParams = new List<KeyValuePair<string, string?>>();

        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("PropertyUri", propertyUri)); // query parameter
        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("GetReverseRelations", getReverseRelations)); // query parameter
        if (offset != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Offset", offset)); // query parameter
        if (limit != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Limit", limit)); // query parameter
        if (languageCode != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("languageCode", languageCode)); // query parameter

        return await bsddApiClient.GetAsync<PropertyRelationsContractV1>(localVarPath, localVarQueryParams, ct);
    }

    /// <summary>
    /// Get Property Value details 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="uri">URI of the property value, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.1/prop/color/value/red</param>
    /// <param name="languageCode">Language Code (optional)</param>
    /// <returns>Task of PropertyValueContractV4</returns>
    public async Task<PropertyValueContractV4> PropertyValueGetAsync(string uri, string? languageCode = null, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(uri))
            throw new BsddApiException(400, "Missing required parameter 'uri' when calling PropertyApi->PropertyValueGet");

        var localVarPath = "/api/PropertyValue/v2";
        var localVarQueryParams = new List<KeyValuePair<string, string?>>();

        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("uri", uri)); // query parameter
        if (languageCode != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("languageCode", languageCode)); // query parameter

        // make the HTTP request
        return await bsddApiClient.GetAsync<PropertyValueContractV4>(localVarPath, localVarQueryParams, ct);
    }
}