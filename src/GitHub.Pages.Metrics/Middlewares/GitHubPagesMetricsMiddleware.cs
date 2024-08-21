// ReSharper disable All

namespace GitHub.Pages.Metrics.Middlewares;

internal sealed class GitHubPagesMetricsMiddleware
{
    private readonly ILogger _logger;
    private readonly GitHubPagesMetrics _metrics;

    public GitHubPagesMetricsMiddleware(RequestDelegate _, ILogger<GitHubPagesMetricsMiddleware> logger, GitHubPagesMetrics metrics)
    {
        _logger = logger;
        _metrics = metrics;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        GitHubPagesMeterModel? model = default;
        KeyValuePair<string, object?>[]? tags = default;

        try
        {
            switch (context)
            {
                case { Request: HttpRequest request } when request.IsContentType(ContentType.Json):
                    {
                        model = await request.ReadFromJsonAsync<GitHubPagesMeterModel>();

                        break;
                    }
                case { Request: HttpRequest request } when request.IsContentType(ContentType.Form):
                    {
                        IFormCollection form = await request.ReadFormAsync();

                        model = new GitHubPagesMeterModel(form);

                        break;
                    }
                case { Request: HttpRequest request }:
                    {
                        using StreamReader bodyReader = new(request.Body);

                        model = new GitHubPagesMeterModel(await bodyReader.ReadToEndAsync());

                        break;
                    }

            }

            tags = [
                KeyValuePair.Create<string, object?>(GitHubPagesMetricsConstants.TagName1, context.Request.Headers.UserAgent),
                KeyValuePair.Create<string, object?>(GitHubPagesMetricsConstants.TagName2, context.Connection.RemoteIpAddress),
                KeyValuePair.Create<string, object?>(GitHubPagesMetricsConstants.TagName3, context.Connection.RemotePort),
            ];
        }
        catch (Exception error)
        {
            _logger.LogError(error, GitHubPagesMetricsConstants.ParseMeterRequestUnsuccessfulMessage);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            return;
        }

        _metrics.Write(model, tags);
    }
}
