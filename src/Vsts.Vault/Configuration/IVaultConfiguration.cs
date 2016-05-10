namespace Vsts.Vault
{
    public interface IVaultConfiguration
    {
        string Username { get; }
        string UserEmail { get; }
        string Password { get; }
        string Account { get; }
        string TargetFolder { get; }
    }
}