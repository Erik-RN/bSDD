using ConsoleDemo.ApiClient;
using ConsoleDemo.Model;

namespace ConsoleDemo.ApiServices;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface ISearchApi
{
    /// <summary>
    /// Search bSDD using free text. Get list of Classes matching the text and optional additional filters.
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="ApiException">Thrown when fails to make API call</exception>
    /// <param name="searchText">The text to search for, minimum 3 characters (case and accent insensitive)</param>
    /// <param name="dictionaryUris">List of dictionaries to filter on.  For a class to be found it must be part of one of the given dictionaries (optional)</param>
    /// <param name="relatedIfcEntities">List of related IFC entities to filter on.  For a class to be found it must have at least one of the given Related Ifc Entities (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <returns>Task of ClassSearchResponseContractV1</returns>
    Task<ClassSearchResponseContractV1> ClassSearchGetAsync(string searchText, List<string>? dictionaryUris = null, List<string>? relatedIfcEntities = null, int? offset = null, int? limit = null, CancellationToken ct = default);

    /// <summary>
    /// Search the bSDD database, get list of Classes without details.  This version uses new naming and returns one Dictionary instead of a list with always one Dictionary.  This API replaces /api/SearchList.
    /// </summary>
    /// <remarks>
    /// The details can be requested per Class via the Class API
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="dictionaryUri">The uri of the Dictionary to filter on (required). The \&quot;latest\&quot; option is supported, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/latest</param>
    /// <param name="searchText">The text to search for (case and accent insensitive) (optional)</param>
    /// <param name="languageCode">The ISO language code to search in and to return the text in (case-sensitive)  If no language code specified or the text is not available in the requested language, the text will be returned in the default language of the dictionary.  If a language code has been given, the search takes place in texts of that language, otherwise searches will be done in the default language of the dictionary.  If an invalid or not supported language code is given, a Bad Request will be returned. (optional)</param>
    /// <param name="relatedIfcEntity">The official IFC entity name to filter on (case-sensitive) (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <returns>Task of SearchInDictionaryResponseContractV1</returns>
    Task<SearchInDictionaryResponseContractV1> SearchInDictionaryGetAsync(string dictionaryUri, string? searchText = null, string? languageCode = null, string? relatedIfcEntity = null, int? offset = null, int? limit = null, CancellationToken ct = default);

    /// <summary>
    /// Search bSDD using free text, get list of Classes and/or Properties.  Pagination options for Classes and Properties are combined. If result contains 10 classes and 5 properties, TotalCount will be 15. Classes will be listed first. Use Offset&#x3D;10 and Limit&#x3D;5, to get the 5 properties only.
    /// </summary>
    /// <remarks>
    /// The details can be requested per Class via the Class API
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="searchText">The text to search for, minimum 2 characters (case and accent insensitive)</param>
    /// <param name="typeFilter">Type filter: can be \&quot;Class\&quot;, \&quot;Property\&quot;, \&quot;Material\&quot;, \&quot;GroupOfProperties\&quot; of a semicolon separated list of these. Empty means all (optional)</param>
    /// <param name="dictionaryUris">List of dictionaries to filter on (optional)</param>
    /// <param name="onlyLatestVersion"> (optional)</param>
    /// <param name="onlyVerified"> (optional)</param>
    /// <param name="includeInactive"> (optional)</param>
    /// <param name="includePreview"> (optional)</param>
    /// <param name="includeSearchDescriptions"> (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <returns>Task of TextSearchResponseContractV2</returns>
    Task<TextSearchResponseContractV2> TextSearchGetAsync(string searchText, bool onlyLatestVersion, bool onlyVerified, bool includeInactive, bool includePreview, bool includeSearchDescriptions, string? typeFilter = null, List<string>? dictionaryUris = null, int? offset = null, int? limit = null, CancellationToken ct = default);
}

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class SearchApi(BsddApiClient bsddApiClient) : ISearchApi
{
    /// <summary>
    /// Search bSDD using free text. Get list of Classes matching the text and optional additional filters. 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="searchText">The text to search for, minimum 3 characters (case and accent insensitive)</param>
    /// <param name="dictionaryUris">List of dictionaries to filter on.  For a class to be found it must be part of one of the given dictionaries (optional)</param>
    /// <param name="relatedIfcEntities">List of related IFC entities to filter on.  For a class to be found it must have at least one of the given Related Ifc Entities (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <returns>Task of ClassSearchResponseContractV1</returns>
    public async Task<ClassSearchResponseContractV1> ClassSearchGetAsync(string searchText, List<string>? dictionaryUris = null, List<string>? relatedIfcEntities = null, int? offset = null, int? limit = null, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            throw new BsddApiException(400, "Missing required parameter 'searchText' when calling SearchApi->ClassSearchGet");

        var localVarPath = "/api/Class/Search/v1";
        var localVarQueryParams = new List<KeyValuePair<string, string?>>();

        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("SearchText", searchText)); // query parameter
        if (dictionaryUris != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("DictionaryUris", dictionaryUris)); // query parameter
        if (relatedIfcEntities != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("RelatedIfcEntities", relatedIfcEntities)); // query parameter
        if (offset != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Offset", offset)); // query parameter
        if (limit != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Limit", limit)); // query parameter

        return await bsddApiClient.GetAsync<ClassSearchResponseContractV1>(localVarPath, localVarQueryParams, ct);
    }

    /// <summary>
    /// Search the bSDD database, get list of Classes without details.  This version uses new naming and returns one Dictionary instead of a list with always one Dictionary.  This API replaces /api/SearchList. The details can be requested per Class via the Class API
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="dictionaryUri">The uri of the Dictionary to filter on (required). The \&quot;latest\&quot; option is supported, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/latest</param>
    /// <param name="searchText">The text to search for (case and accent insensitive) (optional)</param>
    /// <param name="languageCode">The ISO language code to search in and to return the text in (case-sensitive)  If no language code specified or the text is not available in the requested language, the text will be returned in the default language of the dictionary.  If a language code has been given, the search takes place in texts of that language, otherwise searches will be done in the default language of the dictionary.  If an invalid or not supported language code is given, a Bad Request will be returned. (optional)</param>
    /// <param name="relatedIfcEntity">The official IFC entity name to filter on (case-sensitive) (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <returns>Task of SearchInDictionaryResponseContractV1</returns>
    public async Task<SearchInDictionaryResponseContractV1> SearchInDictionaryGetAsync (string dictionaryUri, string? searchText = null, string? languageCode = null, string? relatedIfcEntity = null, int? offset = null, int? limit = null, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(dictionaryUri))
            throw new BsddApiException(400, "Missing required parameter 'dictionaryUri' when calling SearchApi->SearchInDictionaryGet");

        var localVarPath = "/api/SearchInDictionary/v1";
        var localVarQueryParams = new List<KeyValuePair<string, string?>>();

        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("DictionaryUri", dictionaryUri)); // query parameter
        if (searchText != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("SearchText", searchText)); // query parameter
        if (languageCode != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("LanguageCode", languageCode)); // query parameter
        if (relatedIfcEntity != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("RelatedIfcEntity", relatedIfcEntity)); // query parameter
        if (offset != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Offset", offset)); // query parameter
        if (limit != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Limit", limit)); // query parameter

        return await bsddApiClient.GetAsync<SearchInDictionaryResponseContractV1>(localVarPath, localVarQueryParams, ct);
    }

    /// <summary>
    /// Search bSDD using free text, get list of Classes and/or Properties.  Pagination options for Classes and Properties are combined. If result contains 10 classes and 5 properties, TotalCount will be 15. Classes will be listed first. Use Offset&#x3D;10 and Limit&#x3D;5, to get the 5 properties only. The details can be requested per Class via the Class API
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="searchText">The text to search for, minimum 2 characters (case and accent insensitive)</param>
    /// <param name="typeFilter">Type filter: can be \&quot;Class\&quot;, \&quot;Property\&quot;, \&quot;Material\&quot;, \&quot;GroupOfProperties\&quot; of a semicolon separated list of these. Empty means all (optional)</param>
    /// <param name="dictionaryUris">List of dictionaries to filter on (optional)</param>
    /// <param name="onlyLatestVersion"> (optional)</param>
    /// <param name="onlyVerified"> (optional)</param>
    /// <param name="includeInactive"> (optional)</param>
    /// <param name="includePreview"> (optional)</param>
    /// <param name="includeSearchDescriptions"> (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <returns>Task of TextSearchResponseContractV2</returns>
    public async Task<TextSearchResponseContractV2> TextSearchGetAsync(string searchText, bool onlyLatestVersion, bool onlyVerified, bool includeInactive, bool includePreview, bool includeSearchDescriptions, string? typeFilter = null, List<string>? dictionaryUris = null, int? offset = null, int? limit = null, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            throw new BsddApiException(400, "Missing required parameter 'searchText' when calling SearchApi->TextSearchGet");

        var localVarPath = "/api/TextSearch/v2";
        var localVarQueryParams = new List<KeyValuePair<string, string?>>();

        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("SearchText", searchText)); // query parameter
        if (typeFilter != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("TypeFilter", typeFilter)); // query parameter
        if (dictionaryUris != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("DictionaryUris", dictionaryUris)); // query parameter
        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("OnlyLatestVersion", onlyLatestVersion)); // query parameter
        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("OnlyVerified", onlyVerified)); // query parameter
        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("IncludeInactive", includeInactive)); // query parameter
        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("IncludePreview", includePreview)); // query parameter
        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("IncludeSearchDescriptions", includeSearchDescriptions)); // query parameter
        if (offset != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Offset", offset)); // query parameter
        if (limit != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Limit", limit)); // query parameter

        return await bsddApiClient.GetAsync<TextSearchResponseContractV2>(localVarPath, localVarQueryParams, ct);
    }
}