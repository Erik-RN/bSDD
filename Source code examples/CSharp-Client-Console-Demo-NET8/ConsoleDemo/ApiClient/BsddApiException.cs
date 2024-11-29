namespace ConsoleDemo.ApiClient;

/// <summary>
/// API Exception
/// </summary>
public class BsddApiException : Exception
{
    /// <summary>
    /// Gets or sets the error code (HTTP status code)
    /// </summary>
    /// <value>The error code (HTTP status code).</value>
    public int ErrorCode { get; set; }

    /// <summary>
    /// Gets or sets the error content (body json object)
    /// </summary>
    /// <value>The error content (Http response body).</value>
    public dynamic? ErrorContent { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsddApiException"/> class.
    /// </summary>
    /// <param name="errorCode">HTTP status code.</param>
    /// <param name="message">Error message.</param>
    public BsddApiException(int errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsddApiException"/> class.
    /// </summary>
    /// <param name="errorCode">HTTP status code.</param>
    /// <param name="message">Error message.</param>
    /// <param name="errorContent">Error content.</param>
    public BsddApiException(int errorCode, string message, dynamic errorContent) : base(message)
    {
        ErrorCode = errorCode;
        ErrorContent = errorContent;
    }
}