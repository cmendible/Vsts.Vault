namespace Vsts.Vault
{
    using System;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using Vsts.Vault.Git;
    using Vsts.Vault.Logging;
    using Vsts.Vault.TeamServices;

    [Export(typeof(IVaultService))]
    public class VaultService : IVaultService
    {
        private readonly IConfiguration configuration;
        private readonly ITeamServicesConsumer teamServicesConsumer;
        private readonly IGitService gitService;
        private readonly ILogger logger;

        [ImportingConstructor]  
        public VaultService(IConfiguration configuration, ITeamServicesConsumer teamServicesConsumer, IGitService gitService, ILogger logger)
        {
            this.configuration = configuration;
            this.teamServicesConsumer = teamServicesConsumer;
            this.gitService = gitService;
            this.logger = logger;
        }

        public void SafeDeposit()
        {
            var startTime = DateTime.UtcNow;
            var timer = System.Diagnostics.Stopwatch.StartNew();

            string accountUrl = string.Format(
                "https://{0}.visualstudio.com/DefaultCollection/_apis/git/repositories?api-version=1.0",
                this.configuration.VaultConfiguration.Account);

            var task = teamServicesConsumer.GetAsync<Repositories>(accountUrl);
            task.Wait(new TimeSpan(0, 0, 30));
            var result = task.Result;

            var repositories = result.value;
            var repositoriesGroupedByTeamProject = repositories.GroupBy(m => m.project.name).ToList();

            this.logger.InfoFormat("Vsts.Vault backup started at {0}", startTime.ToString());
            foreach (var teamProject in repositoriesGroupedByTeamProject)
            {
                this.CreateDirectory(teamProject.Key);
                foreach (var repo in teamProject)
                {
                    var path =  Path.Combine(teamProject.Key, repo.name);
                    try
                    {
                        this.gitService.CloneOrPull(repo.remoteUrl, this.configuration.VaultConfiguration.TargetFolder + path);
                    }
                    catch (Exception ex)
                    {
                        this.logger.ErrorFormat("Error on {0} with message {1}", repo.remoteUrl, ex.Message);
                    }
                }
            }

            timer.Stop();

            this.logger.InfoFormat("Readed {0} team projects from Visual Studio Team Services", repositoriesGroupedByTeamProject.Count);
            this.logger.InfoFormat("Readed {0} repositories from Visual Studio Team Services", repositories.Count);
            this.logger.InfoFormat("Vsts.Vault backup ended at {0}. Time elapsed {1}", DateTime.UtcNow.ToString(), timer.Elapsed);
        }

        public void CreateDirectory(string path)
        {
            var fullPath = Path.Combine(this.configuration.VaultConfiguration.TargetFolder, path);
            if (!Directory.Exists(fullPath))
            {
                this.logger.DebugFormat("Created directory '{0}'", fullPath);
                Directory.CreateDirectory(fullPath);
            }
        }
    }
}