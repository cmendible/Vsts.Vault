namespace Vsts.Vault
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets the vault configuration.
        /// </summary>
        /// <value>
        /// The vault configuration.
        /// </value>
        IVaultConfiguration VaultConfiguration { get; }
    }
}