namespace Vsts.Vault.Git
{
    using System;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Web;
    using LibGit2Sharp;
    using Vsts.Vault.Logging;

    [Export(typeof(IGitService))]
    public class GitService : IGitService
    {
        private readonly IConfiguration configuration;
        private Credentials credentials;
        private readonly ILogger logger;

        [ImportingConstructor]  
        public GitService(IConfiguration configuration, ILogger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public void Clone(string sourceUrl, string path)
        {
            this.logger.InfoFormat("Cloning {0}", sourceUrl);
            Repository.Clone(HttpUtility.UrlPathEncode(sourceUrl), path, new CloneOptions
            {
                CredentialsProvider = this.CredentialsProvider
            });
        }

        public void CloneOrPull(string sourceUrl, string path)
        {
            if (Directory.Exists(path))
            {
                this.Pull(sourceUrl, path);
            }
            else
            {
                this.Clone(sourceUrl, path);
            }
        }

        public void Pull(string sourceUrl, string path)
        {
            this.logger.InfoFormat("Pulling {0}", sourceUrl);
            using (var repo = new Repository(path))
            {
                PullOptions options = new PullOptions();
                options.FetchOptions = new FetchOptions();
                options.FetchOptions.CredentialsProvider = this.CredentialsProvider;
                repo.Network.Pull(new LibGit2Sharp.Signature(this.configuration.VaultConfiguration.Username, this.configuration.VaultConfiguration.UserEmail, new DateTimeOffset(DateTime.Now)), options);
            }
        }

        private Credentials CredentialsProvider(string url, string usernameFromUrl, SupportedCredentialTypes types)
        {
            this.credentials = new UsernamePasswordCredentials
            {
                Username = this.configuration.VaultConfiguration.Username,
                Password = this.configuration.VaultConfiguration.Password
            };
            return this.credentials;
        }
    }
}