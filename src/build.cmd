@echo off

".nuget\NuGet.exe" "install" "FAKE.Core" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "4.4.2"

:build
cls

SET TARGET="Default"

IF NOT [%1]==[] (SET TARGET="%1")

SET BUILDMODE="Release"
IF NOT [%2]==[] (SET BUILDMODE="%2")

IF %TARGET%=="CreatePackage" (SET RunBuild=1)

IF NOT "%RunBuild%"=="" (
  "tools\FAKE.Core\tools\FAKE.exe" "build.fsx" "target=BuildApp" "buildMode=%BUILDMODE%"
)

"tools\FAKE.Core\tools\FAKE.exe" "build.fsx" "target=%TARGET%" "buildMode=%BUILDMODE%"

:Quit
exit /b %errorlevel%
