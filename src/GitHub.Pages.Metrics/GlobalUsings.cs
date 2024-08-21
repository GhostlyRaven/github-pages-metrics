global using OpenTelemetry.Metrics;
global using OpenTelemetry.Resources;
global using System.Diagnostics.Metrics;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Primitives;
global using GitHub.Pages.Metrics.Models;
global using GitHub.Pages.Metrics.Options;
global using Microsoft.AspNetCore.HttpOverrides;
global using GitHub.Pages.Metrics.Services;
global using GitHub.Pages.Metrics.Constants;
global using GitHub.Pages.Metrics.Extensions;
global using GitHub.Pages.Metrics.Middlewares;
global using GitHub.Pages.Metrics.EndpointFilters;
global using Microsoft.Extensions.DependencyInjection.Extensions;

#if DEBUG

global using OpenTelemetry.Exporter;

#endif
