var octokit = Require<OctokitPack>();
var client = octokit.Create("ScriptCs.Octokit");
var userTask = client.User.Get("alfhenrik");
var user = userTask.Result;
Console.WriteLine(user.Name);