// ReSharper disable All

namespace GitHub.Pages.Metrics.Extensions;

internal static class LoggerExtensions
{
    public static void LogGitHubPagesMetricsOptions(this ILogger logger, GitHubPagesMetricsOptions? options)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        if (options is { ValidGitHubPagesUrlsForCors: string[] validGitHubPagesUrlsForCors, ValidMeterNames: string[] validMeterNames })
        {
            logger.LogInformation(GitHubPagesMetricsConstants.LogFormat, string.Join("; ", validGitHubPagesUrlsForCors),
                string.Join("; ", [GitHubPagesMetricsConstants.SpecialMeterName, .. validMeterNames]));
        }
        else
        {
            logger.LogError(GitHubPagesMetricsConstants.ReadOptionsUnsuccessfulMessage);
        }
    }
}
