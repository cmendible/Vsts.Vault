namespace Vsts.Vault.Console
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
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
