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

let buildMode = getBuildParamOrDefault "buildMode" "Release"
let releaseNotes =
  ReadFile "ReleaseNotes.md"
  |> ReleaseNotesHelper.parseReleaseNotes

MSBuildDefaults <- {
  MSBuildDefaults with
    ToolsVersion = Some "12.0"
    Verbosity = Some MSBuildVerbosity.Minimal
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

let setParams defaults = {
  defaults with
    ToolsVersion = Some("12.0")
    Targets = ["Build"]
    Properties =
      [
        "Configuration", buildMode
        "RestorePackages", "true"
      ]
}

Target "BuildApp" (fun _ ->
  build setParams "./ScriptCs.Octokit.sln"
    |> DoNothing
)

Target "RunTests" (fun _ ->
  trace "This is where we will run some tests"
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
    trace "This is where we will publish the NuGet package to NuGet"
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
  ==> "RunTests"
  ==> "Default"
  ==> "CreateNuGetPackage"
  ==> "CreatePackage"
  =?> ("PublishNuGetPackage", hasBuildParam "nugetApiKey")
  ==> "PublishPackage"

RunTargetOrDefault "Default"
