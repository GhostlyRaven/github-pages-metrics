// ReSharper disable All

namespace GitHub.Pages.Metrics.Models;

internal sealed record GitHubPagesMeterModel
{
    public GitHubPagesMeterModel()
    {
        MeterName = default;
    }

    public GitHubPagesMeterModel(string? meterName)
    {
        MeterName = meterName;
    }

    public GitHubPagesMeterModel(IFormCollection? form)
    {
        MeterName = form is not null && form.TryGetValue(nameof(MeterName), out StringValues value) ? value : default;
    }

    public string? MeterName { get; set; }
}
