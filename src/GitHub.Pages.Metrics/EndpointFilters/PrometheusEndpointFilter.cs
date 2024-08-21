// ReSharper disable All

namespace GitHub.Pages.Metrics.EndpointFilters;

internal sealed class PrometheusEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        HttpContext httpContent = context.HttpContext;

        IHeaderDictionary headers = httpContent.Request.Headers;

        ILogger<PrometheusEndpointFilter>? logger = httpContent.RequestServices.GetService<ILogger<PrometheusEndpointFilter>>();

        if (headers.Accept is [string accept] && headers.UserAgent is [string userAgent] &&
            accept.Contains(GitHubPagesMetricsConstants.PrometheusAccept) &&
            userAgent.Contains(GitHubPagesMetricsConstants.PrometheusUserAgent))
        {
            logger?.LogInformation(GitHubPagesMetricsConstants.PrometheusSuccessfulMessage);

            return await next(context);
        }

        logger?.LogError(GitHubPagesMetricsConstants.PrometheusUnsuccessfulMessage);

        return Results.BadRequest();
    }
}
