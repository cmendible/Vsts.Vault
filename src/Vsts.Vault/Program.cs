namespace Vsts.Vault.WindowsService
{
    using System.IO;
    using System.Reflection;
    using System.ServiceProcess;
    using System.Threading;
    using Serilog;

    /// <summary>
    /// Vsts.Vault Windows Service entry point 
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main program.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            var fileinfo = new FileInfo(Assembly.GetExecutingAssembly().Location);

            Log.Logger = new LoggerConfiguration()
#if (!DEBUG)
                .MinimumLevel.Error()
#else
                .MinimumLevel.Debug()
#endif
                .WriteTo.RollingFile(Path.Combine(fileinfo.DirectoryName, "logs", "log-{Date}.txt"))
                .CreateLogger();

            using (Service service = new Service())
            {
                Program.Start(service);
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Start(Service service)
        {
#if (!DEBUG)

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            {
                service
            };
            ServiceBase.Run(ServicesToRun);
#else
            AutoResetEvent eventPulse = new AutoResetEvent(false);
            service.BackUp();
            eventPulse.WaitOne();
#endif
        }
    }
}
