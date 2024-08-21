// ReSharper disable All

namespace GitHub.Pages.Metrics.Extensions;

internal static class HttpRequestExtensions
{
    internal static bool IsContentType(this HttpRequest request, ContentType contentType)
    {
        return contentType switch
        {
            ContentType.Json => request.HasJsonContentType(),
            ContentType.Form => request.HasFormContentType,
            _ => false,
        };
    }
}

internal enum ContentType
{
    Json,
    Form
}
