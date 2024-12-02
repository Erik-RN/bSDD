using System.Collections.ObjectModel;
using ConsoleDemo.ApiClient;
using ConsoleDemo.Model;

namespace ConsoleDemo.ApiServices;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface IPrivateDictionarySpecificAPIsApi
{
    /// <summary>
    /// Add access for e-mail domain or user
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="body"></param>
    /// <param name="organizationCode"></param>
    /// <param name="dictionaryCode"></param>
    /// <returns>Task of void</returns>
    Task OrganizationManagePrivateAccessAddAsync (PrivateDictionaryAccessContractV1 body, string organizationCode, string dictionaryCode, CancellationToken ct = default);

    /// <summary>
    /// Delete access for e-mail domain or user
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="body"></param>
    /// <param name="organizationCode"></param>
    /// <param name="dictionaryCode"></param>
    /// <returns>Task of void</returns>
    Task OrganizationManagePrivateAccessDeleteAsync (PrivateDictionaryAccessContractV1? body, string organizationCode, string dictionaryCode, CancellationToken ct = default);
}

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class PrivateDictionarySpecificAPIsApi(BsddApiClient bsddApiClient) : IPrivateDictionarySpecificAPIsApi
{
    /// <summary>
    /// Add access for e-mail domain or user 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="body"></param>
    /// <param name="organizationCode"></param>
    /// <param name="dictionaryCode"></param>
    /// <returns>Task of ApiResponse</returns>
    public async Task OrganizationManagePrivateAccessAddAsync(PrivateDictionaryAccessContractV1 body, string organizationCode, string dictionaryCode, CancellationToken ct = default)
    {
        // verify the required parameter 'body' is set
        if (body == null)
            throw new BsddApiException(400, "Missing required parameter 'body' when calling PrivateDictionarySpecificAPIsApi->OrganizationManagePrivateAccessAdd");
        // verify the required parameter 'organizationCode' is set
        if (string.IsNullOrWhiteSpace(organizationCode))
            throw new BsddApiException(400, "Missing required parameter 'organizationCode' when calling PrivateDictionarySpecificAPIsApi->OrganizationManagePrivateAccessAdd");
        // verify the required parameter 'dictionaryCode' is set
        if (string.IsNullOrWhiteSpace(dictionaryCode))
            throw new BsddApiException(400, "Missing required parameter 'dictionaryCode' when calling PrivateDictionarySpecificAPIsApi->OrganizationManagePrivateAccessAdd");

        var localVarPath = "/api/Organization/v1/{organizationCode}/{dictionaryCode}/PrivateAccess";
        var localVarPathParams = new Dictionary<string, string?>
        {
            { "organizationCode", ApiClientHelper.ParameterToString(organizationCode) }, // path parameter
            { "dictionaryCode", ApiClientHelper.ParameterToString(dictionaryCode) } // path parameter
        };

        await bsddApiClient.PostSecureAsync(localVarPath, localVarPathParams, body, ct);
    }

    /// <summary>
    /// Delete access for e-mail domain or user 
    /// </summary>
    /// <exception cref="BsddApiException">Thrown when fails to make API call</exception>
    /// <param name="body"></param>
    /// <param name="organizationCode"></param>
    /// <param name="dictionaryCode"></param>
    /// <returns>Task of ApiResponse</returns>
    public async Task OrganizationManagePrivateAccessDeleteAsync(PrivateDictionaryAccessContractV1? body, string organizationCode, string dictionaryCode, CancellationToken ct = default)
    {
        // verify the required parameter 'body' is set
        if (body == null)
            throw new BsddApiException(400, "Missing required parameter 'body' when calling PrivateDictionarySpecificAPIsApi->OrganizationManagePrivateAccessDelete");
        // verify the required parameter 'organizationCode' is set
        if (string.IsNullOrWhiteSpace(organizationCode))
            throw new BsddApiException(400, "Missing required parameter 'organizationCode' when calling PrivateDictionarySpecificAPIsApi->OrganizationManagePrivateAccessDelete");
        // verify the required parameter 'dictionaryCode' is set
        if (string.IsNullOrWhiteSpace(dictionaryCode))
            throw new BsddApiException(400, "Missing required parameter 'dictionaryCode' when calling PrivateDictionarySpecificAPIsApi->OrganizationManagePrivateAccessDelete");

        var localVarPath = "/api/Organization/v1/{organizationCode}/{dictionaryCode}/PrivateAccess";
        var localVarPathParams = new Dictionary<string, string?>
        {
            { "organizationCode", ApiClientHelper.ParameterToString(organizationCode) }, // path parameter
            { "dictionaryCode", ApiClientHelper.ParameterToString(dictionaryCode) } // path parameter
        };

        // make the HTTP request
        await bsddApiClient.DeleteSecureAsync(localVarPath, localVarPathParams, body, ct);
    }
}