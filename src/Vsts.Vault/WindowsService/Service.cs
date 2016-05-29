namespace Vsts.Vault.WindowsService
{
    using System.ServiceProcess;
    using System.Timers;
    using Vsts.Vault.WindowsService.Properties;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ServiceProcess.ServiceBase" />
    public partial class Service : ServiceBase
    {
        private IVaultService vault;
        private Timer timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        public Service()
        {
            InitializeComponent();
            this.vault = Bootstrapper.GetVaultService();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            this.BackUp();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            this.timer.Stop();
        }

        /// <summary>
        /// Backs up.
        /// </summary>
        public void BackUp()
        {
            this.timer = new Timer(Settings.Default.BackupInterval);
            this.timer.Elapsed += timer_Elapsed;
            this.timer.Start();
        }

        /// <summary>
        /// Handles the Elapsed event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.timer.Stop();
            this.vault.SafeDeposit();
            this.timer.Start();
        }
    }
}
