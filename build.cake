//////////////////////////////////////////////////////////////////////
// TOOLS
//////////////////////////////////////////////////////////////////////
#tool "nuget:?package=GitVersion.CommandLine&prerelease"

using Path = System.IO.Path;
using IO = System.IO;
using Cake.Common.Tools;

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
var publishDir = "./publish";
var localPackagesDir = "C:/NuGet";
var artifactsDir = "./artifacts";

var projectName = "ScriptCs.Octokit";

var gitVersionInfo = GitVersion(new GitVersionSettings {
    OutputType = GitVersionOutput.Json
});

var nugetVersion = gitVersionInfo.NuGetVersion;

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////
Setup(context =>
{
    if(BuildSystem.IsRunningOnAppVeyor)
        BuildSystem.AppVeyor.UpdateBuildVersion(gitVersionInfo.NuGetVersion);
    Information($"Building {projectName} v{nugetVersion}");
});

Teardown(context =>
{
    Information("Finished running tasks.");
});

//////////////////////////////////////////////////////////////////////
//  PRIVATE TASKS
//////////////////////////////////////////////////////////////////////

Task("__Default")
    .IsDependentOn("__Clean")
    .IsDependentOn("__Restore")
    .IsDependentOn("__Build")
    .IsDependentOn("__Pack")
    .IsDependentOn("__Publish")
    .IsDependentOn("__CopyToLocalPackages")
    .IsDependentOn("__RunIntegrationTests");

Task("__Clean")
    .Does(() =>
{
    CleanDirectory(artifactsDir);
    CleanDirectory(publishDir);
    CleanDirectories("./src/**/bin");
    CleanDirectories("./src/**/obj");
});

Task("__Restore")
    .Does(() => DotNetCoreRestore("src", new DotNetCoreRestoreSettings
    {
        ArgumentCustomization = args => args.Append($"/p:Version={nugetVersion}")
    })
);


Task("__Build")
    .Does(() =>
{
    DotNetCoreBuild("./src", new DotNetCoreBuildSettings
    {
        Configuration = configuration,
        NoRestore = true,
        ArgumentCustomization = args => args.Append($"/p:Version={nugetVersion}")
    });
});

Task("__Test")
    .IsDependentOn("__Build")
    .Does(() =>
{
    Information("Tests goes here");
});

Task("__Pack")
    .Does(() => {
        DotNetCorePack(Path.Combine("src", "ScriptCs.Octokit"), new DotNetCorePackSettings
        {
            Configuration = configuration,
            OutputDirectory = artifactsDir,
            NoBuild = true,
            NoRestore = true,
            ArgumentCustomization = args => args.Append($"/p:Version={nugetVersion}")
        });
});

var isTag = !string.IsNullOrEmpty(EnvironmentVariable("APPVEYOR_REPO_TAG"));
var nugetApiKey = EnvironmentVariable("NUGET_ORG_API_KEY");
Task("__Publish")
    .WithCriteria(isTag)
    .WithCriteria(!string.IsNullOrEmpty(nugetApiKey))
    .WithCriteria(BuildSystem.IsRunningOnAppVeyor)
    .Does(() =>
{
    NuGetPush($"{artifactsDir}/{projectName}.{nugetVersion}.nupkg", new NuGetPushSettings {
        ApiKey = nugetApiKey
    });
});

Task("__CopyToLocalPackages")
    .WithCriteria(BuildSystem.IsLocalBuild)
    .IsDependentOn("__Pack")
    .Does(() =>
{
    CreateDirectory(localPackagesDir);
    CopyFiles(Path.Combine(artifactsDir, $"{projectName}.{nugetVersion}.nupkg"), localPackagesDir);
});

Task("__RunIntegrationTests")
    .IsDependentOn("__Publish")
    .Does(() => 
{
    var sampleDir = Path.Combine("src", "ScriptCs.Octokit.Sample");
    var exitCode = StartProcess("cmd", new ProcessSettings {
        Arguments = new ProcessArgumentBuilder()
            .Append(@"/C")
            .Append("run.cmd")
            .Append(nugetVersion),
        Timeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds,
        WorkingDirectory = sampleDir
    });
    if (exitCode != 0)
        throw new Exception($"Running integration tests failed, see above for failure details.");
});

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////
Task("Default")
    .IsDependentOn("__Default");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////
RunTarget(target);