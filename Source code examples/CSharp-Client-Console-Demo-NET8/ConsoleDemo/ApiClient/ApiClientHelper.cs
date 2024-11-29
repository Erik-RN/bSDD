using System.Collections;
using System.Text;

namespace ConsoleDemo.ApiClient;

public static class ApiClientHelper
{
    /// <summary>
    /// Convert params to key/value pairs. 
    /// </summary>
    /// <param name="name">Key name.</param>
    /// <param name="value">Value object.</param>
    /// <returns>A list of KeyValuePairs</returns>
    public static IEnumerable<KeyValuePair<string, string?>> ParameterToKeyValuePairs(string name, object value)
    {
        var parameters = new List<KeyValuePair<string, string?>>();

        if (IsCollection(value))
        {
            var valueCollection = value as IEnumerable;
            parameters.AddRange(from object item in valueCollection select new KeyValuePair<string, string?>(name, ParameterToString(item)));
        }
        else
        {
            parameters.Add(new KeyValuePair<string, string?>(name, ParameterToString(value)));
        }

        return parameters;
    }

    public static string ComposeUrl(string url, List<KeyValuePair<string, string?>> queryParams)
    {
        var queryString = new StringBuilder();
        foreach (var kvp in queryParams)
        {
            if (kvp.Value != null)
            {
                queryString.Append("&");
                queryString.Append(kvp.Key);
                queryString.Append("=");
                queryString.Append(Uri.EscapeDataString(kvp.Value));
            }
        }

        if (queryString.Length > 0)
        {
            queryString[0] = '?';
        }

        return $"{url}{queryString}";
    }

    /// <summary>
    /// If parameter is DateTime, output in a formatted string (default ISO 8601), customizable with Configuration.DateTime.
    /// If parameter is a list, join the list with ",".
    /// Otherwise, just return the string.
    /// </summary>
    /// <param name="obj">The parameter (header, path, query, form).</param>
    /// <returns>Formatted string.</returns>
    private static string? ParameterToString(object obj)
    {
        if (obj is DateTime time)
            // Return a formatted date string - Can be customized with Configuration.DateTimeFormat
            // Defaults to an ISO 8601, using the known as a Round-trip date/time pattern ("o")
            // https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#Anchor_8
            // For example: 2009-06-15T13:45:30.0000000
            return time.ToString("yyyy-MM-dd");

        if (obj is DateTimeOffset offset)
            // Return a formatted date string - Can be customized with Configuration.DateTimeFormat
            // Defaults to an ISO 8601, using the known as a Round-trip date/time pattern ("o")
            // https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#Anchor_8
            // For example: 2009-06-15T13:45:30.0000000
            return offset.ToString("yyyy-MM-dd");

        if (obj is IList list)
        {
            var flattenedString = new StringBuilder();
            foreach (var param in list)
            {
                if (flattenedString.Length > 0)
                    flattenedString.Append(",");
                flattenedString.Append(param);
            }
            return flattenedString.ToString();
        }

        return Convert.ToString(obj);
    }

    /// <summary>
    /// Check if generic object is a collection.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>True if object is a collection type</returns>
    private static bool IsCollection(object value)
    {
        return value is IList || value is ICollection;
    }
}