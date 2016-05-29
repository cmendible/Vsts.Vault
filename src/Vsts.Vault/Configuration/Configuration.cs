namespace Vsts.Vault
{
    using System.ComponentModel.Composition;
    using System.Configuration;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vsts.Vault.IConfiguration" />
    [Export(typeof(IConfiguration))] 
    public class Configuration : IConfiguration
    {
        /// <summary>
        /// The vault configuration
        /// </summary>
        private readonly IVaultConfiguration vaultConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            this.vaultConfiguration = (VaultConfiguration)ConfigurationManager.GetSection("VaultConfiguration");
        }

        /// <summary>
        /// Gets the vault configuration.
        /// </summary>
        /// <value>
        /// The vault configuration.
        /// </value>
        public IVaultConfiguration VaultConfiguration
        {
            get { return vaultConfiguration; }
        }
    }
}