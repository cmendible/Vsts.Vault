namespace Vsts.Vault.TeamServices
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class Repositories
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int count { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public List<Repository> value { get; set; }
    }
}