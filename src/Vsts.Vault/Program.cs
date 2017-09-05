namespace Vsts.Vault
{
    using System;
    using System.IO;
    using System.Reflection;
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

            try
            {
                IVaultService vault = Bootstrapper.GetVaultService();
                vault.SafeDeposit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Done");
            }
        }
    }
}