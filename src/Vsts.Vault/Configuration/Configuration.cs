namespace Vsts.Vault
{
    using System.ComponentModel.Composition;
    using System.Configuration;

    [Export(typeof(IConfiguration))] 
    public class Configuration : IConfiguration
    {
        private readonly IVaultConfiguration vaultConfiguration;

        public Configuration()
        {
            this.vaultConfiguration = (VaultConfiguration)ConfigurationManager.GetSection("VaultConfiguration");
        }

        public IVaultConfiguration VaultConfiguration
        {
            get { return vaultConfiguration; }
        }
    }
}