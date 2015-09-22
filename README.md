ScriptCs.Octokit
==============================

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

_This is purely here as a reminder for me to remember how to build, create and publish new versions of ScriptCs.Octokit to NuGet._

##### Build
```
git clone https://github.com/alfhenrik/ScriptCs.Octokit.git
cd ScriptCs.Octokit
.\build.cmd
```

##### Create NuGet package
 - Open the ReleaseNotes.md file in the root of the repository
 - Add a new entry to the file in the format `* x.y.z - Release notes`

Then run the following command in a shell from the root of the repository.
```
.\build.cmd CreatePackage
```

Once the NuGet package has been created run the following commands to test the new package.
```
.\build.cmd RunTests
```

##### Publish NuGet package to NuGet.org
To publish the NuGet package to NuGet.org, run the following command.
```
.\build.cmd PublishPackage "NuGetApiKey"
```

##### Create release on GitHub

*TODO*

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
