namespace Vsts.Vault.Console
{
    using System;
    using System.IO;
    using System.Reflection;
    using Serilog;

    /// <summary>
    /// Vsts.Vault.Console can be used to make a backup from a console window
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main program.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            try
            {
                var fileinfo = new FileInfo(Assembly.GetExecutingAssembly().Location);

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.LiterateConsole()
                    .CreateLogger();

                IVaultService vault = Bootstrapper.GetVaultService();
                vault.SafeDeposit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}