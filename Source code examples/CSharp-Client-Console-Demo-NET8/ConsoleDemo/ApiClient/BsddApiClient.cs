using System.Text.Json;
using ConsoleDemo.Settings;
using Microsoft.Extensions.Logging;

namespace ConsoleDemo.ApiClient;

/// <summary>
/// API client is mainly responsible for making the HTTP call to the API backend.
/// </summary>
public class BsddApiClient(IHttpClientFactory httpClientFactory, BsddApiOptions bsddApiOptions, ILogger<BsddApiClient> logger)
{
    public const string ApiHttpClientName = nameof(BsddApiClient);

    public async Task<T> GetAsync<T>(string endpoint, List<KeyValuePair<string, string?>> queryParams, CancellationToken cancellationToken = default)
    {
        var url = ApiClientHelper.ComposeUrl($"{bsddApiOptions}/{endpoint}", queryParams);
        logger.LogInformation($"bSDD api: GET {url}");

        var httpClient = httpClientFactory.CreateClient(ApiHttpClientName);
        string? errorMessage;
        try
        {
            var responseMsg = await httpClient.GetAsync(
                url,
                cancellationToken);

            if (responseMsg.IsSuccessStatusCode)
            {
                var contentString = await responseMsg.Content.ReadAsStringAsync(cancellationToken);
                if (string.IsNullOrWhiteSpace(contentString))
                {
                    throw new BsddApiException((int)responseMsg.StatusCode, "no content");
                }

                return JsonSerializer.Deserialize<T>(contentString)!;
            }

            var content = await responseMsg.Content.ReadAsStringAsync(cancellationToken);
            errorMessage = $"bSDD api: GET request failed, url: '{url}' - HttpCode {responseMsg.StatusCode}, Content {content}";

            throw new BsddApiException((int)responseMsg.StatusCode, errorMessage, content);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(GetAsync)} -  wevi api GET request failed, url: {url}");
            errorMessage = $"bSDD api: GET request failed, url: '{url}' - exception: {ex.Message}";

            throw new BsddApiException(-1, errorMessage);
        }
    }

    // TODO implement secure get
    public async Task<T> GetSecureAsync<T>(string endpoint, List<KeyValuePair<string, string?>> queryParams, CancellationToken cancellationToken = default)
    {
        var url = ApiClientHelper.ComposeUrl($"{bsddApiOptions}/{endpoint}", queryParams);
        logger.LogInformation($"bSDD api: GET {url}");

        var httpClient = httpClientFactory.CreateClient(ApiHttpClientName);
        string? errorMessage;
        try
        {
            var responseMsg = await httpClient.GetAsync(
                url,
                cancellationToken);

            if (responseMsg.IsSuccessStatusCode)
            {
                var contentString = await responseMsg.Content.ReadAsStringAsync(cancellationToken);
                if (string.IsNullOrWhiteSpace(contentString))
                {
                    throw new BsddApiException((int)responseMsg.StatusCode, "no content");
                }

                return JsonSerializer.Deserialize<T>(contentString)!;
            }

            var content = await responseMsg.Content.ReadAsStringAsync(cancellationToken);
            errorMessage = $"bSDD api: GET request failed, url: '{url}' - HttpCode {responseMsg.StatusCode}, Content {content}";

            throw new BsddApiException((int)responseMsg.StatusCode, errorMessage, content);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(GetAsync)} -  wevi api GET request failed, url: {url}");
            errorMessage = $"bSDD api: GET request failed, url: '{url}' - exception: {ex.Message}";

            throw new BsddApiException(-1, errorMessage);
        }
    }

    public async Task DeleteSecureAsync(string localVarPath, Dictionary<string, string?> localVarPathParams, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}