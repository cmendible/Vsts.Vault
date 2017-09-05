using Microsoft.Extensions.Configuration;

namespace Vsts.Vault
{
    /// <summary>
    /// 
    /// </summary>
    public class VaultConfiguration
    {
        /// <summary>
        /// Gets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the user email.
        /// </summary>
        /// <value>
        /// The user email.
        /// </value>
        public string UserEmail
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <value>
        /// The account.
        /// </value>
        public string Account
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the target folder.
        /// </summary>
        /// <value>
        /// The target folder.
        /// </value>
        public string TargetFolder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the target folder.
        /// </summary>
        /// <value>
        /// The target folder.
        /// </value>
        public bool Prune
        {
            get;
            set;
        }
    }
}