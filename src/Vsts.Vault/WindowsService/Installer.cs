namespace Vsts.Vault.WindowsService
{
    using System.Collections;
    using System.ComponentModel;
    using System.ServiceProcess;
    using Vsts.Vault.WindowsService.Properties;

    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        /// <summary>
        /// The service installer.
        /// </summary>
        private ServiceInstaller serviceInstaller;

        /// <summary>
        /// The service process installer.
        /// </summary>
        private ServiceProcessInstaller serviceProcessInstaller;

        /// <summary>
        /// Initializes a new instance of the <see cref="Installer"/> class.
        /// </summary>
        public Installer()
        {
            this.serviceProcessInstaller = new ServiceProcessInstaller()
            {
                Account = ServiceAccount.LocalSystem,
            };

            string description = string.Format("Vsts Vault {0}", Settings.Default.ServiceName);

            this.serviceInstaller = new ServiceInstaller()
            {
                DelayedAutoStart = true,
                Description = description,
                DisplayName = description,
                ServiceName = Settings.Default.ServiceName,
                StartType = ServiceStartMode.Automatic
            };

            this.Installers.Add(this.serviceProcessInstaller);
            this.Installers.Add(this.serviceInstaller);
        }

        /// <summary>
        /// Releases the unmanaged resources used by this instance and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.serviceInstaller != null)
                {
                    this.serviceInstaller.Dispose();
                    this.serviceInstaller = null;
                }

                if (this.serviceProcessInstaller != null)
                {
                    this.serviceProcessInstaller.Dispose();
                    this.serviceProcessInstaller = null;
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Raises the <see cref="System.Configuration.Install.TaskManagerInstaller.AfterInstall" /> event.
        /// </summary>
        /// <param name="savedState">An <see cref="System.Collections.IDictionary" /> that contains the state of the computer after repositories the installers contained in the <see cref="System.Configuration.Install.TaskManagerInstaller.Installers" /> property have completed their installations.</param>
        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);

            using (var serviceController = new ServiceController(Settings.Default.ServiceName))
            {
                if (serviceController.Status == ServiceControllerStatus.Stopped)
                {
                    serviceController.Start();
                }
            }
        }
    }
}
