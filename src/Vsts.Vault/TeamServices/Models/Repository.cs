namespace Vsts.Vault.TeamServices
{
    /// <summary>
    /// 
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string url { get; set; }

        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        /// <value>
        /// The project.
        /// </value>
        public Project project { get; set; }

        /// <summary>
        /// Gets or sets the default branch.
        /// </summary>
        /// <value>
        /// The default branch.
        /// </value>
        public string defaultBranch { get; set; }

        /// <summary>
        /// Gets or sets the remote URL.
        /// </summary>
        /// <value>
        /// The remote URL.
        /// </value>
        public string remoteUrl { get; set; }
    }
}
