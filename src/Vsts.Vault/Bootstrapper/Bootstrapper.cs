namespace Vsts.Vault
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Vsts.Vault.Git;
    using Vsts.Vault.Logging;
    using Vsts.Vault.TeamServices;

    /// <summary>
    /// Bootstrapper that setups MEF and works as factory for the VaultService. 
    /// </summary>
    public static class Bootstrapper
    {
        /// <summary>
        /// The container
        /// </summary>
        private static IServiceProvider container;

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        internal static IServiceProvider Container
        {
            get
            {
                if (container == null)
                {
                    // Enable to app to read json setting files
                    var builder = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables();

                    // build the configuration
                    var configuration = builder.Build();

                    var services = new ServiceCollection();
                    services.AddOptions();
                    services.Configure<VaultConfiguration>(configuration.GetSection("VaultConfiguration"));
                    services.AddTransient<IVaultService, VaultService>();
                    services.AddTransient<ILogger, SeriLogLogger>();
                    services.AddTransient<IGitService, GitService>();
                    services.AddTransient<ITeamServicesConsumer, TeamServicesConsumer>();
                    container = services.BuildServiceProvider();
                }

                return container;
            }
        }

        /// <summary>
        /// Gets the vault service.
        /// </summary>
        /// <returns>An IVaultService instance</returns>
        public static IVaultService GetVaultService()
        {
            return Container.GetService<IVaultService>();
        }
    }
}