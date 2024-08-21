// ReSharper disable All1

internal sealed class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Publish);

    [Solution(SuppressBuildProjectCheck = true)]
    private readonly Solution Solution;

    [Parameter("Configuration to build. Default is 'Release'.)")]
    private readonly Configuration Configuration = Configuration.Release;

    private Target Clear => target => target
        .Executes(() =>
        {
            RootDirectory.GlobDirectories("GitHub.Pages.Metrics.Artifacts/{bin,obj,package,publish,docker}")
                .DeleteDirectories();

            Log.Information("The «bin», «obj», «package», «publish» and «docker» directories has been cleared.");
        });

    private Configure<DotNetPublishSettings> PublishBase => settings => settings
        .EnableNoLogo()
        .SetConfiguration(Configuration)
        .SetRuntime(GlobalConstants.Runtime)
        .SetFramework(GlobalConstants.Framework)
        .EnableSelfContained()
        .EnablePublishSingleFile();

    private Target Publish => target => target
        .DependsOn(Clear)
        .Executes(() =>
        {
            Project project = Solution.AllProjects.FirstOrDefault(project => project.Name == "GitHub.Pages.Metrics")!;

            _ = DotNetPublish(settings => settings
                .Apply(PublishBase)
                .SetProject(project.Path));
        });
}
