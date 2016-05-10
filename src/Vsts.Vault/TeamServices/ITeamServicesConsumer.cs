namespace Vsts.Vault.TeamServices
{
    using System.Threading.Tasks;

    public interface ITeamServicesConsumer
    {
        Task<T> GetAsync<T>(string url);
    }
}
