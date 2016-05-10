namespace Vsts.Vault.TeamServices
{
    public class Repository
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Project project { get; set; }
        public string defaultBranch { get; set; }
        public string remoteUrl { get; set; }
    }
}
