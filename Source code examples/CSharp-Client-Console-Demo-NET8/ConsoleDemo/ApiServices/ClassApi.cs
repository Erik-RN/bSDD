using ConsoleDemo.ApiClient;
using ConsoleDemo.Model;

namespace ConsoleDemo.ApiServices;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface IClassApi
{
    /// <summary>
    /// Get class details
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="uri">URI of the class, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.1/class/apple</param>
    /// <param name="includeClassProperties">Use this option to include properties of the class. By default, it is set to false (optional)</param>
    /// <param name="includeChildClassReferences">Use this option to include references to child classes. By default, it is set to false (optional)</param>
    /// <param name="includeClassRelations">Use this option to include loading relations of the class. By default, it is set to false (optional)</param>
    /// <param name="includeReverseRelations">Use this option to include loading reverse relations of the class, i.e. classes having a relation with this class. By default, it is set to false (optional)</param>
    /// <param name="reverseRelationDictionaryUris">When including reverse relations, you can specify which dictionaries to include. By default, all dictionaries are included (optional)</param>
    /// <param name="languageCode">Specify language (case-sensitive). For those items the text is not available in the requested language, the text will be returned in the default language of the dictionary (optional)</param>
    /// <returns>Task of ClassContractV1</returns>
    Task<ClassContractV1> ClassGetAsync (string uri, bool includeClassProperties = false, bool includeChildClassReferences = false, bool includeClassRelations = false, bool includeReverseRelations = false, List<string>? reverseRelationDictionaryUris = null, string? languageCode = null, CancellationToken ct = default);

    /// <summary>
    /// Get class properties (paginated)
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="classUri">URI of the class, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.1/class/apple</param>
    /// <param name="propertySet">Optional: Property set to filter the properties (optional)</param>
    /// <param name="propertyCode">Optional: Property code to filter the properties (optional)</param>
    /// <param name="searchText">Optional: Search text to filter the properties.  Search is done in the property name, property description and property code.  Cannot be used together with PropertySet or PropertyCode. (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <param name="languageCode">Specify language (case-sensitive). For those items the text is not available in the requested language, the text will be returned in the default language of the dictionary (optional)</param>
    /// <returns>Task of ClassPropertiesContractV1</returns>
    Task<ClassPropertiesContractV1> ClassPropertiesGetAsync (string classUri, string? propertySet = null, string? propertyCode = null, string? searchText = null, int? offset = null, int? limit = null, string? languageCode = null, CancellationToken ct = default);

    /// <summary>
    /// Get class relations or reverse relations (paginated)
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="classUri">URI of the class, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.1/class/apple</param>
    /// <param name="getReverseRelations">Get either the forward or the reverse relations</param>
    /// <param name="searchText">Optional: Search text to filter the relations.  Search is done in the class name only. (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <param name="languageCode">Specify language (case-sensitive). For those items the text is not available in the requested language, the text will be returned in the default language of the dictionary (optional)</param>
    /// <returns>Task of ClassRelationsContractV1</returns>
    Task<ClassRelationsContractV1> ClassRelationsGetAsync (string classUri, bool getReverseRelations = false, string? searchText = null, int? offset = null, int? limit = null, string? languageCode = null, CancellationToken ct = default);
}

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class ClassApi(BsddApiClient bsddApiClient) : IClassApi
{
    /// <summary>
    /// Get class details 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="uri">URI of the class, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.1/class/apple</param>
    /// <param name="includeClassProperties">Use this option to include properties of the class. By default, it is set to false (optional)</param>
    /// <param name="includeChildClassReferences">Use this option to include references to child classes. By default, it is set to false (optional)</param>
    /// <param name="includeClassRelations">Use this option to include loading relations of the class. By default, it is set to false (optional)</param>
    /// <param name="includeReverseRelations">Use this option to include loading reverse relations of the class, i.e. classes having a relation with this class. By default, it is set to false (optional)</param>
    /// <param name="reverseRelationDictionaryUris">When including reverse relations, you can specify which dictionaries to include. By default, all dictionaries are included (optional)</param>
    /// <param name="languageCode">Specify language (case-sensitive). For those items the text is not available in the requested language, the text will be returned in the default language of the dictionary (optional)</param>
    /// <returns>Task of ApiResponse (ClassContractV1)</returns>
    public async Task<ClassContractV1> ClassGetAsync(string uri, bool includeClassProperties = false, bool includeChildClassReferences = false, bool includeClassRelations = false, bool includeReverseRelations = false, List<string>? reverseRelationDictionaryUris = null, string? languageCode = null, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(uri))
            throw new BsddApiException(400, "Missing required parameter 'uri' when calling ClassApi->ClassGet");

        var localVarPath = "/api/Class/v1";
        var localVarQueryParams = new List<KeyValuePair<string, string?>>();

        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Uri", uri)); // query parameter
        if (includeClassProperties) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("IncludeClassProperties", includeClassProperties)); // query parameter
        if (includeChildClassReferences) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("IncludeChildClassReferences", includeChildClassReferences)); // query parameter
        if (includeClassRelations) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("IncludeClassRelations", includeClassRelations)); // query parameter
        if (includeReverseRelations) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("IncludeReverseRelations", includeReverseRelations)); // query parameter
        if (reverseRelationDictionaryUris != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("ReverseRelationDictionaryUris", reverseRelationDictionaryUris)); // query parameter
        if (languageCode != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("languageCode", languageCode)); // query parameter

        // make the HTTP request
        return await bsddApiClient.GetAsync<ClassContractV1>(localVarPath, localVarQueryParams, ct);
    }

    /// <summary>
    /// Get class properties (paginated) 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="classUri">URI of the class, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.1/class/apple</param>
    /// <param name="propertySet">Optional: Property set to filter the properties (optional)</param>
    /// <param name="propertyCode">Optional: Property code to filter the properties (optional)</param>
    /// <param name="searchText">Optional: Search text to filter the properties.  Search is done in the property name, property description and property code.  Cannot be used together with PropertySet or PropertyCode. (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <param name="languageCode">Specify language (case sensitive). For those items the text is not available in the requested language, the text will be returned in the default language of the dictionary (optional)</param>
    /// <returns>Task of ApiResponse (ClassPropertiesContractV1)</returns>
    public async Task<ClassPropertiesContractV1> ClassPropertiesGetAsync(string classUri, string? propertySet = null, string? propertyCode = null, string? searchText = null, int? offset = null, int? limit = null, string? languageCode = null, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(classUri))
            throw new BsddApiException(400, "Missing required parameter 'classUri' when calling ClassApi->ClassPropertiesGet");

        var localVarPath = "/api/Class/Properties/v1";
        var localVarQueryParams = new List<KeyValuePair<string, string?>>();

        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("ClassUri", classUri)); // query parameter
        if (propertySet != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("PropertySet", propertySet)); // query parameter
        if (propertyCode != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("PropertyCode", propertyCode)); // query parameter
        if (searchText != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("SearchText", searchText)); // query parameter
        if (offset != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Offset", offset)); // query parameter
        if (limit != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Limit", limit)); // query parameter
        if (languageCode != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("languageCode", languageCode)); // query parameter

        // make the HTTP request
        return await bsddApiClient.GetAsync<ClassPropertiesContractV1>(localVarPath,localVarQueryParams, ct);
    }

    /// <summary>
    /// Get class relations or reverse relations (paginated) 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="classUri">URI of the class, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.1/class/apple</param>
    /// <param name="getReverseRelations">Get either the forward or the reverse relations</param>
    /// <param name="searchText">Optional: Search text to filter the relations.  Search is done in the class name only. (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <param name="languageCode">Specify language (case sensitive). For those items the text is not available in the requested language, the text will be returned in the default language of the dictionary (optional)</param>
    /// <returns>Task of ApiResponse (ClassRelationsContractV1)</returns>
    public async Task<ClassRelationsContractV1> ClassRelationsGetAsync(string classUri, bool getReverseRelations = false, string? searchText = null, int? offset = null, int? limit = null, string? languageCode = null, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(classUri))
            throw new BsddApiException(400, "Missing required parameter 'classUri' when calling ClassApi->ClassRelationsGet");

        var localVarPath = "/api/Class/Relations/v1";
        var localVarQueryParams = new List<KeyValuePair<string, string?>>();

        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("ClassUri", classUri)); // query parameter
        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("GetReverseRelations", getReverseRelations)); // query parameter
        if (searchText != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("SearchText", searchText)); // query parameter
        if (offset != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Offset", offset)); // query parameter
        if (limit != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Limit", limit)); // query parameter
        if (languageCode != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("languageCode", languageCode)); // query parameter

        // make the HTTP request
        return await bsddApiClient.GetAsync<ClassRelationsContractV1>(localVarPath,localVarQueryParams, ct);
    }
}