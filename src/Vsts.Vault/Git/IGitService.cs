namespace Vsts.Vault.Git
{
    public interface IGitService
    {
        void Clone(string sourceUrl, string path);
        void Pull(string sourceUrl, string path);
        void CloneOrPull(string sourceUrl, string path);
    }
}