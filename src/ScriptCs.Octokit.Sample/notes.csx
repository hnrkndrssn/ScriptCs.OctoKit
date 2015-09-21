var owner = "OctopusDeploy";
var repo = "Issues";
var previousMilestone = "2.5.6";
var milestone = "2.5.7";
var labels = new Dictionary<string, string> { {"enhancement","New"}, {"bug","Fixed"}};
var username = "alfhenrik-test";
var oAuthToken = "c985de4f1b657563b0010a51906364b4b14e3c3c";

var client = Require<OctokitPack>().CreateWithOAuth("ScriptCs.ReleaseNotesScript", username, oAuthToken);
var issues = client.Issue.GetForRepository(owner, repo, new RepositoryIssueRequest { State = ItemState.Closed, }).Result;

System.Text.StringBuilder sb = new System.Text.StringBuilder();
Console.WriteLine("#### Changes between {0} and {1}", previousMilestone, milestone);
sb.AppendLine(String.Format("#### Changes between {0} and {1}", previousMilestone, milestone));
foreach(var issue in issues
  .Where(issue => issue.Milestone != null && issue.Milestone.Title == milestone && issue.Labels.Any(label => labels.Keys.Contains(label.Name)))
  .Select(issue => new { Number = issue.Number, Title = issue.Title, Url = issue.HtmlUrl })
  .OrderBy(issue => issue.Number))
{
  sb.AppendLine(String.Format("- [{0}]({1}) - {2}", issue.Number, issue.Url, issue.Title));
  Console.WriteLine("- [{0}]({1}) - {2}", issue.Number, issue.Url, issue.Title);
}
var filename = System.IO.Path.GetTempPath() + "ReleaseNotes_" + milestone + ".md";

try
{
  File.WriteAllText(filename, sb.ToString());
  Console.WriteLine("Release notes written to {0}", filename);
}
catch (Exception ex)
{
  Console.WriteLine("An error occurred writing file {0}.", filename);
  Console.WriteLine(ex.Message);
}

var markdownPadExe = @"C:\Program Files (x86)\MarkdownPad 2\MarkdownPad2.exe";
try
{
  System.Diagnostics.Process.Start(markdownPadExe, filename);
}
catch (Exception ex)
{
  Console.WriteLine("An error occurred starting {0} with file {1}", markdownPadExe, filename);
  Console.WriteLine(ex.Message);
}
