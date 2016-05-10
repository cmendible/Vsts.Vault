namespace Vsts.Vault.WindowsService
{
    using System.ServiceProcess;
    using System.Timers;
    using Vsts.Vault.WindowsService.Properties;

    public partial class Service : ServiceBase
    {
        private IVaultService vault;
        private Timer timer;

        public Service()
        {
            InitializeComponent();
            this.vault = Bootstrapper.GetVaultService();
        }

        protected override void OnStart(string[] args)
        {
            this.BackUp();
        }

        protected override void OnStop()
        {
            this.timer.Stop();
        }

        public void BackUp()
        {
            this.timer = new Timer(Settings.Default.BackupInterval);
            this.timer.Elapsed += timer_Elapsed;
            this.timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.timer.Stop();
            this.vault.SafeDeposit();
            this.timer.Start();
        }
    }
}
