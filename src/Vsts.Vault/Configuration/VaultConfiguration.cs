namespace Vsts.Vault
{
    using System.ComponentModel.Composition;
    using System.Configuration;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Configuration.ConfigurationSection" />
    /// <seealso cref="Vsts.Vault.IVaultConfiguration" />
    [Export(typeof(IVaultConfiguration))]
    public class VaultConfiguration : ConfigurationSection, IVaultConfiguration
    {
        /// <summary>
        /// Gets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [ConfigurationProperty("Username", IsRequired = true)]
        public string Username
        {
            get
            {
                return this["Username"].ToString();
            }
        }

        /// <summary>
        /// Gets the user email.
        /// </summary>
        /// <value>
        /// The user email.
        /// </value>
        [ConfigurationProperty("UserEmail", IsRequired = true)]
        public string UserEmail
        {
            get
            {
                return this["UserEmail"].ToString();
            }
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [ConfigurationProperty("Password", IsRequired = true)]
        public string Password
        {
            get
            {
                return this["Password"].ToString();
            }
        }

        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <value>
        /// The account.
        /// </value>
        [ConfigurationProperty("Account", IsRequired = true)]
        public string Account
        {
            get
            {
                return this["Account"].ToString();
            }
        }

        /// <summary>
        /// Gets the target folder.
        /// </summary>
        /// <value>
        /// The target folder.
        /// </value>
        [ConfigurationProperty("TargetFolder", IsRequired = true)]
        public string TargetFolder
        {
            get
            {
                return this["TargetFolder"].ToString();
            }
        }

        /// <summary>
        /// Gets the target folder.
        /// </summary>
        /// <value>
        /// The target folder.
        /// </value>
        [ConfigurationProperty("Prune", DefaultValue = true)]
        public bool Prune
        {
            get
            {
                return (bool)this["Prune"];
            }
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns></returns>
        public static VaultConfiguration Load()
        {
            return (VaultConfiguration)ConfigurationManager.GetSection("VaultConfiguration");
        }
    }
}