## :warning: :rotating_light: :warning: Due to `Octokit.net` moving to `net46` as of `v0.41.0`, no further updates can be made to this script pack as the dependency on `ScriptCs.Contracts` only allows the use of `net45` as a target framework. :broken_heart: :broken_heart: :broken_heart:

ScriptCs.Octokit
==============================
[![Build status](https://ci.appveyor.com/api/projects/status/j024yq10dbfovsxi/branch/master?svg=true)](https://ci.appveyor.com/project/hnrkndrssn/scriptcs-octokit/branch/master)
[![NuGet](https://img.shields.io/nuget/v/ScriptCs.Octokit.svg)](https://www.nuget.org/packages/ScriptCs.Octokit)

## About
This is a [Script Pack](https://github.com/scriptcs/scriptcs/wiki) for [scriptcs](https://github.com/scriptcs/scriptcs) that can be used to interact with the GitHub API using [octokit.net](https://github.com/octokit/octokit.net).

## Installation

Install the NuGet package by running `scriptcs -install ScriptCs.Octokit`

## Usage

There's three different ways to create your GitHubClient:
- Anonymous - access to public information only
- Basic Auth - using your GitHub username/password
- OAuth Token - using a personal access token (Account Settings->Applications->Personal Access Token)

#### Anonymous
```csharp
var octokit = Require<OctokitPack>();
var client = octokit.Create("MyAwesomeScriptCsGitHubClient");
var userTask = client.User.Get("alfhenrik");
var user = userTask.Result;
Console.WriteLine(user.Name);
```

#### Basic Auth
```csharp
var octokit = Require<OctokitPack>();
var gitHubClient = octokit.CreateWithBasicAuth("MyAwesomeScriptCsGitHubClient", "myusername", "mypassword");
var userTask = client.User.Current();
var user = userTask.Result;
Console.WriteLine(user.Name);
```

#### OAuth Token
```csharp
var octokit = Require<OctokitPack>();
var gitHubClient = octokit.CreateWithOAuth("MyAwesomeScriptCsGitHubClient", "myusername", "myoauthtoken");
var userTask = client.User.Current();
var user = userTask.Result;
Console.WriteLine(user.Name);
```

#### Build, create, test and publish NuGet packages

##### Build
```
git clone https://github.com/hnrkndrssn/ScriptCs.OctoKit.git
cd ScriptCs.Octokit
.\build.cmd
```

##### Prepare for a new release
 - Open the `ReleaseNotes.md` file in the root of the repository
 - Add a new entry to the file in the format `* x.y.z - Release notes`
 - Commit and push the change
 - Tag the latest commit with the next version `x.y.z`, and push the new tag to GitHub by running the following commands:
```
git tag x.y.z
git push origin x.y.z
```

AppVeyor will build the tag and publish the new release to NuGet.org

##### Create release on GitHub

- Download the new NuGet package from AppVeyor (or NuGet.org)
- Create a new release from tag `x.y.z` with `vX.Y.Z` as the `Release title`
- Add `- x.y.z - Release notes` to the `Release notes`
- Attach the NuGet package to the release
- Publish the release!

## License

The MIT License (MIT)

Copyright (c) 2013 Henrik Andersson

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
