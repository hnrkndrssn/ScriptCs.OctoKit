@echo off

".nuget\NuGet.exe" "install" "FAKE.Core" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "4.4.2"

:build
cls

SET TARGET="Default"
IF NOT [%1]==[] (SET TARGET="%1")

IF %TARGET%=="Default" (SET RunTests=runTests)
IF %TARGET%=="RunTests" (SET RunTests=runTests)

SET BUILDMODE="Release"
IF NOT %TARGET%=="PublishPackage" (
	IF NOT [%2]==[] (SET BUILDMODE="%2")
)

IF %TARGET%=="PublishPackage" (
	IF NOT [%2]==[] (SET NuGetApiKey="%2")
)

"tools\FAKE.Core\tools\FAKE.exe" "build.fsx" "target=%TARGET%" "buildMode=%BUILDMODE%" "nugetApiKey=%NuGetApiKey%"

:Quit
exit /b %errorlevel%
