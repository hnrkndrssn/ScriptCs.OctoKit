System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
var octokit = Require<OctokitPack>();
var client = octokit.Create("ScriptCs.Octokit");
var userTask = client.User.Get("hnrkndrssn");
var user = userTask.Result;
Console.WriteLine(user.Name);

var repoTask = client.Repository.Branch.GetAll("hnrkndrssn", "octokit.net");
var branches = repoTask.Result;
Console.WriteLine(branches.Count);
foreach(var branch in branches)
{
	Console.WriteLine(branch.Name);
}