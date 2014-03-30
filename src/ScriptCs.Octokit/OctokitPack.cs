using Octokit;
using ScriptCs.Contracts;

namespace ScriptCs.Octokit
{
    public class OctokitPack : IScriptPackContext
    {
        public GitHubClient Create(string productHeaderValue)
        {
            var client = new GitHubClient(new ProductHeaderValue(productHeaderValue));

            return client;
        }

        public GitHubClient CreateWithBasicAuth(string productHeaderValue, string username, string password)
        {
            var client = new GitHubClient(new ProductHeaderValue(productHeaderValue));
            client.Credentials = new Credentials(username, password);
            return client;
        }

        public GitHubClient CreateWithOAuth(string productHeaderValue, string username, string oAuthToken)
        {
            var client = new GitHubClient(new ProductHeaderValue(productHeaderValue));
            client.Credentials = new Credentials(username, oAuthToken);

            return client;
        }
    }
}