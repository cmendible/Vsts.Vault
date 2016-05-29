namespace Vsts.Vault
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVaultConfiguration
    {
        /// <summary>
        /// Gets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        string Username { get; }

        /// <summary>
        /// Gets the user email.
        /// </summary>
        /// <value>
        /// The user email.
        /// </value>
        string UserEmail { get; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        string Password { get; }

        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <value>
        /// The account.
        /// </value>
        string Account { get; }

        /// <summary>
        /// Gets the target folder.
        /// </summary>
        /// <value>
        /// The target folder.
        /// </value>
        string TargetFolder { get; }
    }
}