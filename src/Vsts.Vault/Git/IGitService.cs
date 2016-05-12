namespace Vsts.Vault.Git
{
    public interface IGitService
    {
        void CloneOrPull(string sourceUrl, string path);
    }
}