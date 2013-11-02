ScriptCs.Octokit
==============================

## About
This is a [Script Pack](https://github.com/scriptcs/scriptcs/wiki) for [scriptcs](https://github.com/scriptcs/scriptcs) that can be used to interact with the GitHub API using the newly released [octokit.net](https://github.com/octokit/octokit.net).

## Installation

Install the nuget package by running `scriptcs -install ScriptCs.Octokit`

## Usage

There's three different ways to create your GitHubClient:
- Anonymous - access to public information only
- Basic Auth - using your GitHub username/password
- OAuth Token - using a personal access token (Account Settings->Applications->Personal Access Token)

#### Anonymous
```csharp
var octokit = Require<Octokit>();
var gitHubClient = octokit.Create("MyAwesomeScriptCsGitHubClient");
Console.WriteLine(gitHubClient.User.Get("myusername"));
```

#### Basic Auth
```csharp
var octokit = Require<Octokit>();
var gitHubClient = octokit.CreateWithBasicAuth("MyAwesomeScriptCsGitHubClient", "myusername", "mypassword");
var userTask = client.User.Current();
var user = userTask.Result;
Console.WriteLine(user.Name);
```

#### OAuth Token
```csharp
var octokit = Require<Octokit>();
var gitHubClient = octokit.CreateWithOAuth("MyAwesomeScriptCsGitHubClient", "myusername", "myoauthtoken");
var userTask = client.User.Current();
var user = userTask.Result;
Console.WriteLine(user.Name);
```

##License

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
