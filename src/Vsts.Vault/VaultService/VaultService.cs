namespace Vsts.Vault
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using Microsoft.Extensions.Options;
    using Vsts.Vault.Git;
    using Vsts.Vault.Logging;
    using Vsts.Vault.TeamServices;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vsts.Vault.IVaultService" />
    public class VaultService : IVaultService
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly VaultConfiguration configuration;
        /// <summary>
        /// The team services consumer
        /// </summary>
        private readonly ITeamServicesConsumer teamServicesConsumer;
        /// <summary>
        /// The git service
        /// </summary>
        private readonly IGitService gitService;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="VaultService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="teamServicesConsumer">The team services consumer.</param>
        /// <param name="gitService">The git service.</param>
        /// <param name="logger">The logger.</param>
        public VaultService(IOptions<VaultConfiguration> configuration, ITeamServicesConsumer teamServicesConsumer, IGitService gitService, ILogger logger)
        {
            this.configuration = configuration.Value;
            this.teamServicesConsumer = teamServicesConsumer;
            this.gitService = gitService;
            this.logger = logger;
        }

        /// <summary>
        /// Safes the deposit.
        /// </summary>
        public void SafeDeposit()
        {
            try
            {

                var startTime = DateTime.UtcNow;
                var timer = System.Diagnostics.Stopwatch.StartNew();

                var repositories = this.GetRepositories();

                if (repositories == null)
                {
                    return;
                }

                var repositoriesGroupedByTeamProject = repositories.GroupBy(m => m.project.name).ToList();

                this.logger.InfoFormat("Vsts.Vault backup started at {0}", startTime.ToString());
                foreach (var teamProject in repositoriesGroupedByTeamProject)
                {
                    this.CreateDirectory(teamProject.Key);
                    foreach (var repo in teamProject)
                    {
                        var path = Path.Combine(teamProject.Key, repo.name);

                        VaultService.InvokeOrRetry(
                            () => this.gitService.CloneOrPull(repo.remoteUrl, this.configuration.TargetFolder + path),
                            (ex) => this.logger.ErrorFormat("Error on {0} with message {1}... Retrying...", repo.remoteUrl, ex.Message),
                            () => this.logger.FatalFormat("Error on {0} ... Abort...\n", repo.remoteUrl));
                    }
                }

                timer.Stop();

                this.logger.InfoFormat("Readed {0} team projects from Visual Studio Team Services", repositoriesGroupedByTeamProject.Count);
                this.logger.InfoFormat("Readed {0} repositories from Visual Studio Team Services", repositories.Count);
                this.logger.InfoFormat("Vsts.Vault backup ended at {0}. Time elapsed {1}", DateTime.UtcNow.ToString(), timer.Elapsed);
            }
            catch (Exception ex)
            {
                this.logger.Fatal(ex);
            }
        }

        /// <summary>
        /// Creates the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        private void CreateDirectory(string path)
        {
            var fullPath = Path.Combine(this.configuration.TargetFolder, path);
            if (!Directory.Exists(fullPath))
            {
                this.logger.DebugFormat("Created directory '{0}'", fullPath);
                Directory.CreateDirectory(fullPath);
            }
        }

        private IList<Repository> GetRepositories()
        {
            IList<Repository> result = null;

            VaultService.InvokeOrRetry(
                () =>
                {
                    string accountUrl = string.Format(
                        "https://{0}.visualstudio.com/DefaultCollection/_apis/git/repositories?api-version=1.0",
                        this.configuration.Account);

                    var task = teamServicesConsumer.GetAsync<Repositories>(accountUrl);
                    task.Wait(new TimeSpan(0, 0, 30));
                    result = task.Result.value;
                },
                (ex) => this.logger.FatalFormat(
                            "An error occurred while getting repositories from VSTS\nTime: {0}\nError: {1}\n",
                            DateTime.Now.ToString(),
                            ex.ToString()),
                () => this.logger.Info("Canceling attempt to get repostories from VSTS.\n"));

            return result;
        }

        private static void InvokeOrRetry(Action action, Action<Exception> attemptExceptionAction, Action circuitOpenAction)
        {
            int retryCount = 0;
            bool retry = false;

            do
            {
                retry = false;
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    retry = true;
                    retryCount++;
                    Thread.Sleep(3000);

                    attemptExceptionAction(ex);
                }

            } while ((retry == true) && (retryCount < 3));

            if (retryCount == 3)
            {
                circuitOpenAction();
            }
        }
    }
}