// ReSharper disable All

namespace GitHub.Pages.Metrics.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection ConfigureReverseProxy(this IServiceCollection services, Action<ForwardedHeadersOptions>? configure = default)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        if (configure is null)
        {
            return services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
        }

        return services.Configure(configure);
    }
}
