#r @"tools\FAKE.Core\tools\FakeLib.dll"
open Fake
open System

let authors = ["Henrik Andersson"]

let projectName = "ScriptCs.Octokit"
let projectDescription = "Octokit Script Pack for ScriptCs."
let projectSummary = projectDescription

let buildDir = "./src/ScriptCs.Octokit/bin"
let packagingRoot = "./packaging/"
let packagingDir = packagingRoot @@ "ScriptCs.Octokit"
let localNuGet = "C:\NuGet"

let buildMode = getBuildParamOrDefault "buildMode" "Release"
let releaseNotes =
  ReadFile "ReleaseNotes.md"
  |> ReleaseNotesHelper.parseReleaseNotes

let private packageFileName project version = sprintf "%s.%s.nupkg" project version

MSBuildDefaults <- {
  MSBuildDefaults with
    ToolsVersion = Some "14.0"
    Verbosity = Some MSBuildVerbosity.Minimal
}

let setParams defaults = {
  defaults with
    ToolsVersion = Some("14.0")
    Targets = ["Build"]
    Properties =
      [
        "Configuration", buildMode
      ]
}

Target "Clean" (fun _ ->
    CleanDirs [buildDir; packagingDir; packagingRoot;]
)

Target "AssemblyInfo" (fun _ ->
    AssemblyInfoHelper.ReplaceAssemblyInfoVersions (fun p ->
        {p with
            OutputFileName = "./src/ScriptCs.Octokit/Properties/AssemblyInfo.cs"
            AssemblyVersion = releaseNotes.AssemblyVersion
            AssemblyFileVersion = releaseNotes.AssemblyVersion
            AssemblyInformationalVersion = releaseNotes.AssemblyVersion
        })
)

Target "BuildApp" (fun _ ->
    RestorePackages()
    build setParams "./ScriptCs.Octokit.sln"
        |> DoNothing
)

Target "UnitTests" (fun _ -> 
    trace "This is where unit tests should be run"
)

Target "EndToEndTests" (fun _ ->
    CopyFile localNuGet (packagingRoot @@ (packageFileName projectName releaseNotes.AssemblyVersion) )

    let result = ExecProcess(fun info ->
        info.FileName <- FullName "./src/ScriptCs.Octokit.Sample/run.cmd"
        info.UseShellExecute <- false
        info.WorkingDirectory <- FullName "./src/ScriptCs.Octokit.Sample/") (TimeSpan.FromMinutes 5.0)
    if result <> 0 then failwithf "run.cmd returned with a non-zero exit code"
)

Target "CreateNuGetPackage" (fun _ ->
  let net45Dir = packagingDir @@ "lib/net45"
  CleanDirs [net45Dir]

  CopyFile net45Dir (buildDir @@ "Release/ScriptCs.Octokit.dll")

  NuGet (fun p ->
    {p with
      Authors = authors
      Project = projectName
      Description = projectDescription
      OutputPath = packagingRoot
      Summary = projectSummary
      WorkingDir = packagingDir
      Version = releaseNotes.AssemblyVersion
      ReleaseNotes = toLines releaseNotes.Notes
      Dependencies =
        [
          "Octokit", GetPackageVersion "./packages/" "Octokit"
          "ScriptCs.Contracts", GetPackageVersion "./packages/" "ScriptCs.Contracts"
        ]
      Publish = false }) "./ScriptCs.Octokit.nuspec"
)

Target "PublishNuGetPackage" (fun _ ->
    NuGetPublish (fun p ->
    {p with
        OutputPath = packagingRoot
        WorkingDir = packagingDir
        Project = projectName
        Version = releaseNotes.AssemblyVersion
        AccessKey = getBuildParamOrDefault "nugetApiKey" ""
    })
)

Target "Default" DoNothing

Target "CreatePackage" DoNothing

Target "PublishPackage" DoNothing

"Clean"
  ==> "AssemblyInfo"
  ==> "BuildApp"
  ==> "UnitTests"
  ==> "CreateNuGetPackage"
  ==> "CreatePackage"
  ==> "EndToEndTests"
  ==> "Default"
  =?> ("PublishNuGetPackage", hasBuildParam "nugetApiKey")
  ==> "PublishPackage"

RunTargetOrDefault "Default"
