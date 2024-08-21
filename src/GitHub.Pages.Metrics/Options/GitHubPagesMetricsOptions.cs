// ReSharper disable All

namespace GitHub.Pages.Metrics.Options;

internal sealed class GitHubPagesMetricsOptions
{
    public string[] MeterNames { get; set; } = [];

    public string[] GitHubPagesUrlsForCors { get; set; } = [];

    public string[] ValidMeterNames => MeterNames?.Where(meterName => !string.IsNullOrWhiteSpace(meterName)).ToArray() ?? [];

    public string[] ValidGitHubPagesUrlsForCors => GitHubPagesUrlsForCors?.Where(gitHubPagesUrlForCors => !string.IsNullOrWhiteSpace(gitHubPagesUrlForCors)).ToArray() ?? [];
}
