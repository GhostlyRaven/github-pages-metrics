// ReSharper disable All

namespace GitHub.Pages.Metrics.Constants;

internal static class GitHubPagesMetricsConstants
{
#if DEBUG

    internal const string ConfigFilePath = "Configs/github.pages.metrics.json";

#else

    internal const string ConfigFilePath = "configs/github.pages.metrics.json";

#endif

    internal const string PolicyName = "GitHubPagesMetrics";

    internal const string PolicyMethod = "POST";

    internal const string PrometheusAccept = "application/openmetrics-text";

    internal const string PrometheusUserAgent = "Prometheus";

    internal const string PrometheusSuccessfulMessage = "The successful request to Prometheus.";

    internal const string PrometheusUnsuccessfulMessage = "The unsuccessful request to Prometheus.";

    internal const string ReadOptionsUnsuccessfulMessage = "The options read unsuccessful from config.";

    internal const string ParseMeterRequestUnsuccessfulMessage = "Failed to parse meter request from HTTP request.";

    internal const string OpenTelemetryMeterName = "GitHubPagesMetrics";

    internal const string OpenTelemetryServiceName = "GitHub.Pages.Metrics";

    internal const string EndpointUrl = "/githubpages/metrics";

    internal const string EndpointDisplayName = "GitHub Pages Metrics";

    internal const string SpecialMeterName = "undefined";

    internal const string MeterUnit = "link_click";

    internal const string TagName1 = "request_headers_user_agent";

    internal const string TagName2 = "request_connection_remote_ip_address";

    internal const string TagName3 = "request_connection_remote_port";

    internal const string LogFormat = """
                                The GitHub Pages url for CORS: {0}
                                The meter names: {1}
                                """;
}
