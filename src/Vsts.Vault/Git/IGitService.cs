namespace Vsts.Vault.Git
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGitService
    {
        /// <summary>
        /// Clones the or pull.
        /// </summary>
        /// <param name="sourceUrl">The source URL.</param>
        /// <param name="path">The path.</param>
        void CloneOrPull(string sourceUrl, string path);
    }
}