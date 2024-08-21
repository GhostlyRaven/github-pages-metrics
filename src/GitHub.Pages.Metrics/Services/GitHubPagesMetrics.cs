// ReSharper disable All

namespace GitHub.Pages.Metrics.Services;

internal sealed class GitHubPagesMetrics
{
    private readonly Dictionary<string, Counter<long>> _counters;

    public GitHubPagesMetrics(IOptions<GitHubPagesMetricsOptions> options, IMeterFactory meterFactory)
    {
        string[] meterNames = [GitHubPagesMetricsConstants.SpecialMeterName, .. options.Value.ValidMeterNames];

        Meter meter = meterFactory.Create(GitHubPagesMetricsConstants.OpenTelemetryMeterName);

        _counters = new Dictionary<string, Counter<long>>(meterNames.Length);

        foreach (string meterName in meterNames)
        {
            if (_counters.ContainsKey(meterName))
            {
                continue;
            }

            _counters.Add(meterName, meter.CreateCounter<long>(meterName, GitHubPagesMetricsConstants.MeterUnit));
        }
    }

    public void Write(GitHubPagesMeterModel? model, ReadOnlySpan<KeyValuePair<string, object?>> tags)
    {
        Counter<long> counter = model switch
        {
            { MeterName: string meterName } => _counters[_counters.ContainsKey(meterName) ? meterName : GitHubPagesMetricsConstants.SpecialMeterName],
            _ => _counters[GitHubPagesMetricsConstants.SpecialMeterName]
        };

        counter.Add(1, tags);
    }
}
