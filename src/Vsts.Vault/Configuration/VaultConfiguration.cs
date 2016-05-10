namespace Vsts.Vault
{
    using System.ComponentModel.Composition;
    using System.Configuration;

    [Export(typeof(IVaultConfiguration))]
    public class VaultConfiguration : ConfigurationSection, IVaultConfiguration
    {
        [ConfigurationProperty("Username", IsRequired = true)]
        public string Username
        {
            get
            {
                return this["Username"].ToString();
            }
        }

        [ConfigurationProperty("UserEmail", IsRequired = true)]
        public string UserEmail
        {
            get
            {
                return this["UserEmail"].ToString();
            }
        }

        [ConfigurationProperty("Password", IsRequired = true)]
        public string Password
        {
            get
            {
                return this["Password"].ToString();
            }
        }

        [ConfigurationProperty("Account", IsRequired = true)]
        public string Account
        {
            get
            {
                return this["Account"].ToString();
            }
        }

        [ConfigurationProperty("TargetFolder", IsRequired = true)]
        public string TargetFolder
        {
            get
            {
                return this["TargetFolder"].ToString();
            }
        }

        public static VaultConfiguration Load()
        {
            return (VaultConfiguration)ConfigurationManager.GetSection("VaultConfiguration");
        }        
    }
}