var octokit = Require<Octokit>();
var client = octokit.Create("ScriptCs.Octokit");
Console.WriteLine(client.User.Get("alfhenrik"));