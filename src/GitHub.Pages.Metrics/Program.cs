// ReSharper disable All

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile(GitHubPagesMetricsConstants.ConfigFilePath, true, true);

builder.Services.Configure<GitHubPagesMetricsOptions>(builder.Configuration).ConfigureReverseProxy();

builder.Services.AddOpenTelemetry().WithMetrics(metricsBuilder =>
{
    metricsBuilder.AddMeter(GitHubPagesMetricsConstants.OpenTelemetryMeterName);

    metricsBuilder.AddPrometheusExporter();

#if DEBUG

    metricsBuilder.AddConsoleExporter((consoleExporterOptions, metricReaderOptions) =>
    {
        consoleExporterOptions.Targets = ConsoleExporterOutputTargets.Debug;
        metricReaderOptions.PeriodicExportingMetricReaderOptions.ExportIntervalMilliseconds = 60000;
    });

#endif
}).ConfigureResource(resourceBuilder =>
{
    resourceBuilder.AddService(GitHubPagesMetricsConstants.OpenTelemetryServiceName);
});

builder.Services.TryAddSingleton<GitHubPagesMetrics>();

GitHubPagesMetricsOptions? options = builder.Configuration.Get<GitHubPagesMetricsOptions>();

builder.Services.AddCors(corsBuilder =>
{
    corsBuilder.AddPolicy(GitHubPagesMetricsConstants.PolicyName, policyBuilder =>
    {
        if (options is { ValidGitHubPagesUrlsForCors: { Length: > 0 } validGitHubPagesUrlsForCors })
        {
            policyBuilder.WithOrigins(validGitHubPagesUrlsForCors)
                .WithMethods(GitHubPagesMetricsConstants.PolicyMethod)
                .AllowAnyHeader();
        }
    });
});

WebApplication app = builder.Build();

app.UseForwardedHeaders();

app.MapGitHubPagesMetricsEndpoint().WithDisplayName(GitHubPagesMetricsConstants.EndpointDisplayName);

app.MapPrometheusScrapingEndpoint().OnlyPrometheusRequest();

app.Logger.LogGitHubPagesMetricsOptions(options);

await app.RunAsync();
