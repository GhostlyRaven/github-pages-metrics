// ReSharper disable All

namespace GitHub.Pages.Metrics.Extensions;

internal static class EndpointRouteBuilderExtensions
{
    internal static IEndpointConventionBuilder OnlyPrometheusRequest(this IEndpointConventionBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder.AddEndpointFilter<IEndpointConventionBuilder, PrometheusEndpointFilter>();
    }

    internal static IEndpointConventionBuilder MapGitHubPagesMetricsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints, nameof(endpoints));

        IApplicationBuilder builder = endpoints.CreateApplicationBuilder();

        builder.UseCors(GitHubPagesMetricsConstants.PolicyName);

        builder.UseMiddleware<GitHubPagesMetricsMiddleware>();

        return endpoints.MapPost(GitHubPagesMetricsConstants.EndpointUrl, builder.Build());
    }
}
