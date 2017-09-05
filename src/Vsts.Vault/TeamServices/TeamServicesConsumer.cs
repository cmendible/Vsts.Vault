namespace Vsts.Vault.TeamServices
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Vsts.Vault.TeamServices.ITeamServicesConsumer" />
    public class TeamServicesConsumer : ITeamServicesConsumer
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly VaultConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamServicesConsumer"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public TeamServicesConsumer(IOptions<VaultConfiguration> configuration)
        {
            this.configuration = configuration.Value;
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var authenticationHeaderValue = string.Format("{0}:{1}", this.configuration.Username, this.configuration.Password);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationHeaderValue)));

                string responseBody;
                using (var response = await client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
                }

                var retVal = JsonConvert.DeserializeObject<T>(responseBody);
                return retVal;
            }
        }
    }
}