namespace Vsts.Vault.TeamServices
{
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public interface ITeamServicesConsumer
    {
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string url);
    }
}
