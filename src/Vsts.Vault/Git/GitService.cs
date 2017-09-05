namespace Vsts.Vault.Git
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using LibGit2Sharp;
    using LibGit2Sharp.Handlers;
    using Microsoft.Extensions.Options;
    using Vsts.Vault.Logging;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vsts.Vault.Git.IGitService" />
    public class GitService : IGitService
    {
        private readonly VaultConfiguration configuration;
        private Credentials credentials;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logger">The logger.</param>
        public GitService(IOptions<VaultConfiguration> configuration, ILogger logger)
        {
            this.configuration = configuration.Value;
            this.logger = logger;
        }

        /// <summary>
        /// Clones the or pull.
        /// </summary>
        /// <param name="sourceUrl">The source URL.</param>
        /// <param name="path">The path.</param>
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

            this.CreateTrackingBranches(path);
        }

        /// <summary>
        /// Clones the specified source URL.
        /// </summary>
        /// <param name="sourceUrl">The source URL.</param>
        /// <param name="path">The path.</param>
        private void Clone(string sourceUrl, string path)
        {
            this.logger.InfoFormat("Cloning {0}", sourceUrl);
            Repository.Clone(HttpUtility.UrlPathEncode(sourceUrl), path, new CloneOptions
            {
                CredentialsProvider = this.CredentialsProvider
            });
        }

        /// <summary>
        /// Creats the tracking branch.
        /// </summary>
        /// <param name="repo">The repo.</param>
        /// <param name="branchName">Name of the branch.</param>
        /// <param name="trackedBranchName">Name of the tracked branch.</param>
        private void CreatTrackingBranch(Repository repo, string branchName, string trackedBranchName)
        {
            // Retrieve remote tracking branch
            Branch trackedBranch = repo.Branches[trackedBranchName];

            // Create local branch pointing at the same Commit
            Branch branch = repo.CreateBranch(branchName, trackedBranch.Tip);

            repo.Branches.Update(branch,
                b => b.TrackedBranch = trackedBranch.CanonicalName);
        }

        /// <summary>
        /// Creates the tracking branches.
        /// </summary>
        /// <param name="path">The path.</param>
        private void CreateTrackingBranches(string path)
        {
            using (var repo = new Repository(path))
            {
                var localBranches = repo.Branches.Where(b => !b.IsRemote).Select(b => b.FriendlyName).ToList();
                foreach (Branch b in repo.Branches.Where(b => b.IsRemote && !localBranches.Contains(this.ExtractBranchName(b.FriendlyName))))
                {
                    this.CreatTrackingBranch(repo, this.ExtractBranchName(b.FriendlyName), b.FriendlyName);
                }
            }
        }

        /// <summary>
        /// Credentialses the provider.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="usernameFromUrl">The username from URL.</param>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        private Credentials CredentialsProvider(string url, string usernameFromUrl, SupportedCredentialTypes types)
        {
            this.credentials = new UsernamePasswordCredentials
            {
                Username = this.configuration.Username,
                Password = this.configuration.Password
            };
            return this.credentials;
        }

        /// <summary>
        /// Extracts the name of the branch.
        /// </summary>
        /// <param name="remoteBranchName">Name of the remote branch.</param>
        /// <returns></returns>
        private string ExtractBranchName(string remoteBranchName)
        {
            return remoteBranchName.Split('/').Last();
        }

        /// <summary>
        /// Pulls the specified source URL.
        /// </summary>
        /// <param name="sourceUrl">The source URL.</param>
        /// <param name="path">The path.</param>
        private void Pull(string sourceUrl, string path)
        {
            this.logger.InfoFormat("Pulling {0}", sourceUrl);
            using (var repo = new Repository(path))
            {
                PullOptions options = new PullOptions();
                options.FetchOptions = new FetchOptions()
                {
                    Prune = this.configuration.Prune,
                    CredentialsProvider = this.CredentialsProvider
                };
                options.MergeOptions = new MergeOptions()
                {
                    FileConflictStrategy = CheckoutFileConflictStrategy.Theirs
                };

                Commands.Pull(
                    repo,
                    new LibGit2Sharp.Signature(this.configuration.Username, this.configuration.UserEmail, new DateTimeOffset(DateTime.Now)),
                    options);
            }
        }
    }
}