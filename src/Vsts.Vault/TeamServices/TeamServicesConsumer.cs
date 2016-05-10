namespace Vsts.Vault.TeamServices
{
    using System;
    using System.ComponentModel.Composition;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    [Export(typeof(ITeamServicesConsumer))]
    public class TeamServicesConsumer : ITeamServicesConsumer
    {
        private readonly IConfiguration configuration;

        [ImportingConstructor]
        public TeamServicesConsumer(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var authenticationHeaderValue = string.Format("{0}:{1}", this.configuration.VaultConfiguration.Username, this.configuration.VaultConfiguration.Password);

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