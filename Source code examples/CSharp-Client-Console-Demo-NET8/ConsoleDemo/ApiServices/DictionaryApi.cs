using ConsoleDemo.ApiClient;
using ConsoleDemo.Model;

namespace ConsoleDemo.ApiServices;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface IDictionaryApi
{
    /// <summary>
    /// Get Dictionary with (tree of) classes
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="ApiException">Thrown when fails to make API call</exception>
    /// <param name="uri">The URI of the dictionary. The option \&quot;latest\&quot; is supported, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/latest</param>
    /// <param name="useNestedClasses">Set to true to get classes in a nested structure.  You can&#x27;t use this option if you are using pagination. (optional)</param>
    /// <param name="classType">Optional filter on class type. Possible values are \&quot;Class\&quot;, \&quot;GroupOfProperties\&quot;, \&quot;AlternativeUse\&quot; and \&quot;Material\&quot;. (optional)</param>
    /// <param name="searchText">Optional filter text.  Ignored when UseNestedClasses &#x3D; true (optional)</param>
    /// <param name="relatedIfcEntity">Optional filter on related IFC entity. It accepts an IFC entity code (e.g. IfcWall) or uri (e.g. https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcWall).  When a code is supplied, finding matching classes ignores the IFC version.  Ignored when UseNestedClasses &#x3D; true (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <param name="languageCode">Specify language (case-sensitive). For those items the text is not available in the requested language, the text will be returned in the default language of the dictionary (optional)</param>
    /// <returns>Task of DictionaryClassesResponseContractV1Classes</returns>
    Task<DictionaryClassesResponseContractV1Classes> DictionaryClassesGetWithClassesAsync (string uri, bool useNestedClasses = false, string? classType = null, string? searchText = null, string? relatedIfcEntity = null, int? offset = null, int? limit = null, string? languageCode = null, CancellationToken ct = default);

    /// <summary>
    /// Download a file with an export of a dictionary in format supported by Sketchup.  This API replaces /api/RequestExportFile/preview
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="dictionaryUri">The uri of the dictionary to be downloaded, including version number, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.1. You can replace the version number by \&quot;latest\&quot; to automatically get the latest (active) version of the dictionary</param>
    /// <returns>Task of byte[]</returns>
    Task<byte[]> DictionaryDownloadSketchupAsync (string dictionaryUri, CancellationToken ct = default);

    /// <summary>
    /// Get list of available Dictionaries with optional filtering.
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="uri">Optional parameter to filter by first part of URI. Use this one to get details of just one dictionary version  or, if you leave out the version number at the end, get all the versions of a dictionary.  Example: https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/ (optional)</param>
    /// <param name="includeTestDictionaries">Should test dictionaries be included in the result? By default it is set to false.  This option is ignored if you specify a URI. (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <returns>Task of DictionaryResponseContractV1</returns>
    Task<DictionaryResponseContractV1> DictionaryGetAsync (string? uri = null, bool? includeTestDictionaries = null, int? offset = null, int? limit = null, CancellationToken ct = default);

    /// <summary>
    /// Get Dictionary with its properties
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="uri">The URI of the dictionary. The option \&quot;latest\&quot; is supported, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/latest</param>
    /// <param name="searchText">Optional filter text. (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <param name="languageCode">Specify language (case-sensitive). For those items the text is not available in the requested language, the text will be returned in the default language of the dictionary (optional)</param>
    /// <returns>Task of DictionaryPropertiesResponseContractV1</returns>
    Task<DictionaryPropertiesResponseContractV1> DictionaryGetWithPropertiesAsync (string uri, string? searchText = null, int? offset = null, int? limit = null, string? languageCode = null, CancellationToken ct = default);
}

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class DictionaryApi(BsddApiClient bsddApiClient) : IDictionaryApi
{
    /// <summary>
    /// Get Dictionary with (tree of) classes 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="uri">The URI of the dictionary. The option \&quot;latest\&quot; is supported, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/latest</param>
    /// <param name="useNestedClasses">Set to true to get classes in a nested structure.  You can&#x27;t use this option if you are using pagination. (optional)</param>
    /// <param name="classType">Optional filter on class type. Possible values are \&quot;Class\&quot;, \&quot;GroupOfProperties\&quot;, \&quot;AlternativeUse\&quot; and \&quot;Material\&quot;. (optional)</param>
    /// <param name="searchText">Optional filter text.  Ignored when UseNestedClasses &#x3D; true (optional)</param>
    /// <param name="relatedIfcEntity">Optional filter on related IFC entity. It accepts an IFC entity code (e.g. IfcWall) or uri (e.g. https://identifier.buildingsmart.org/uri/buildingsmart/ifc/4.3/class/IfcWall).  When a code is supplied, finding matching classes ignores the IFC version.  Ignored when UseNestedClasses &#x3D; true (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <param name="languageCode">Specify language (case-sensitive). For those items the text is not available in the requested language, the text will be returned in the default language of the dictionary (optional)</param>
    /// <returns>Task of ApiResponse (DictionaryClassesResponseContractV1Classes)</returns>
    public async Task<DictionaryClassesResponseContractV1Classes> DictionaryClassesGetWithClassesAsync (string uri, bool useNestedClasses = false, string? classType = null, string? searchText = null, string? relatedIfcEntity = null, int? offset = null, int? limit = null, string? languageCode = null, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(uri))
            throw new BsddApiException(400, "Missing required parameter 'uri' when calling DictionaryApi->DictionaryClassesGetWithClasses");

        var localVarPath = "/api/Dictionary/v1/Classes";
        var localVarQueryParams = new List<KeyValuePair<string, string?>>();

        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Uri", uri)); // query parameter
        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("UseNestedClasses", useNestedClasses)); // query parameter
        if (classType != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("ClassType", classType)); // query parameter
        if (searchText != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("SearchText", searchText)); // query parameter
        if (relatedIfcEntity != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("RelatedIfcEntity", relatedIfcEntity)); // query parameter
        if (offset != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Offset", offset)); // query parameter
        if (limit != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Limit", limit)); // query parameter
        if (languageCode != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("languageCode", languageCode)); // query parameter

        return await bsddApiClient.GetAsync<DictionaryClassesResponseContractV1Classes>(localVarPath, localVarQueryParams, ct);
    }

    /// <summary>
    /// Download a file with an export of a dictionary in format supported by Sketchup.  This API replaces /api/RequestExportFile/preview 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="dictionaryUri">The uri of the dictionary to be downloaded, including version number, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/1.1. You can replace the version number by \&quot;latest\&quot; to automatically get the latest (active) version of the dictionary</param>
    /// <returns>Task of ApiResponse (byte[])</returns>
    public async Task<byte[]> DictionaryDownloadSketchupAsync (string dictionaryUri, CancellationToken ct = default)
    {
        // verify the required parameter 'dictionaryUri' is set
        if (string.IsNullOrWhiteSpace(dictionaryUri))
            throw new BsddApiException(400, "Missing required parameter 'dictionaryUri' when calling DictionaryApi->DictionaryDownloadSketchup");

        var localVarPath = "/api/DictionaryDownload/sketchup/v1";
        var localVarQueryParams = new List<KeyValuePair<string, string?>>();

        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("DictionaryUri", dictionaryUri)); // query parameter

        // make the HTTP request
        return await bsddApiClient.GetSecureAsync<byte[]>(localVarPath, localVarQueryParams, ct);
    }

    /// <summary>
    /// Get list of available Dictionaries with optional filtering. 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="uri">Optional parameter to filter by first part of URI. Use this one to get details of just one dictionary version  or, if you leave out the version number at the end, get all the versions of a dictionary.  Example: https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/ (optional)</param>
    /// <param name="includeTestDictionaries">Should test dictionaries be included in the result? By default it is set to false.  This option is ignored if you specify a URI. (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <returns>Task of ApiResponse (DictionaryResponseContractV1)</returns>
    public async Task<DictionaryResponseContractV1> DictionaryGetAsync (string? uri = null, bool? includeTestDictionaries = null, int? offset = null, int? limit = null, CancellationToken ct = default)
    {
        var localVarPath = "/api/Dictionary/v1";
        var localVarQueryParams = new List<KeyValuePair<string, string?>>();

        if (uri != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Uri", uri)); // query parameter
        if (includeTestDictionaries != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("IncludeTestDictionaries", includeTestDictionaries)); // query parameter
        if (offset != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Offset", offset)); // query parameter
        if (limit != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Limit", limit)); // query parameter

        return await bsddApiClient.GetAsync<DictionaryResponseContractV1>(localVarPath, localVarQueryParams, ct);
    }

    /// <summary>
    /// Get Dictionary with its properties 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="uri">The URI of the dictionary. The option \&quot;latest\&quot; is supported, e.g. https://identifier.buildingsmart.org/uri/bs-agri/fruitvegs/latest</param>
    /// <param name="searchText">Optional filter text. (optional)</param>
    /// <param name="offset">Zero-based offset of the first item to be returned. Default is 0. (optional)</param>
    /// <param name="limit">Limit number of items to be returned. The default and maximum number of items returned is 1000. When Offset is specified, then the default limit is 100. (optional)</param>
    /// <param name="languageCode">Specify language (case-sensitive). For those items the text is not available in the requested language, the text will be returned in the default language of the dictionary (optional)</param>
    /// <returns>Task of ApiResponse (DictionaryPropertiesResponseContractV1)</returns>
    public async Task<DictionaryPropertiesResponseContractV1> DictionaryGetWithPropertiesAsync (string uri, string? searchText = null, int? offset = null, int? limit = null, string? languageCode = null, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(uri))
            throw new BsddApiException(400, "Missing required parameter 'uri' when calling DictionaryApi->DictionaryGetWithProperties");

        var localVarPath = "/api/Dictionary/v1/Properties";
        var localVarQueryParams = new List<KeyValuePair<string, string?>>();

        localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Uri", uri)); // query parameter
        if (searchText != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("SearchText", searchText)); // query parameter
        if (offset != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Offset", offset)); // query parameter
        if (limit != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("Limit", limit)); // query parameter
        if (languageCode != null) localVarQueryParams.AddRange(ApiClientHelper.ParameterToKeyValuePairs("languageCode", languageCode)); // query parameter

        return await bsddApiClient.GetAsync<DictionaryPropertiesResponseContractV1>(localVarPath, localVarQueryParams, ct);
    }
}