using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable AllUnderscoreLocalParameterName

[GitHubActions(
    "ci",
    GitHubActionsImage.UbuntuLatest,
    On = new[] { GitHubActionsTrigger.Push, GitHubActionsTrigger.PullRequest },
    InvokedTargets = new[] { nameof(Publish) },
    ImportSecrets = new[] { "NUGET_API_KEY" }
)]
class Build : NukeBuild
{
    // Define the solution file
    [Solution] readonly Solution Solution;

    // GitVersion for versioning
    [GitVersion] readonly GitVersion GitVersion;
    
    readonly Configuration Configuration = IsServerBuild
        ? Configuration.Release
        : Configuration.Debug;
    
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    
    public static int Main() => Execute<Build>(x => x.Publish);
    
    // Clean the project and artifacts
    Target Clean => _ => _
        .Executes(() =>
        {
            ArtifactsDirectory.CreateOrCleanDirectory();
            DotNetClean(s => s
                .SetProject(Solution));
        });

    // Restore dependencies
    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });
    
    // Compile the solution
    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });
    
    // Run unit tests
    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoBuild()
                .SetResultsDirectory(ArtifactsDirectory / "test-results"));
        });

    Target Pack => _ => _
        .DependsOn(Test)
        .Executes(() =>
        {
            DotNetPack(s => s
                .SetProject(Solution)
                .SetConfiguration(Configuration)
                .SetOutputDirectory(ArtifactsDirectory));
        });
    
    // Publish the NuGet package
    Target Publish => _ => _
        .DependsOn(Pack)
        .Executes(() =>
        {
            DotNetNuGetPush(s => s
                .SetTargetPath(ArtifactsDirectory / "*.nupkg")
                .SetSource("https://api.nuget.org/v3/index.json")
                .SetApiKey(EnvironmentInfo.GetVariable<string>("NUGET_API_KEY")));
        });
}