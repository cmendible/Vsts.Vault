namespace Vsts.Vault
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Bootstrapper
    {
        private static CompositionContainer container;

        internal static CompositionContainer Container
        {
            get
            {
                if (container == null)
                {
                    var catalog =
                        new DirectoryCatalog(".", string.Format("{0}.*", typeof(Bootstrapper).Namespace));

                    container = new CompositionContainer(catalog);
                }

                return container;
            }
        }

        public static IVaultService GetVaultService()
        {
           return Container.GetExportedValue<IVaultService>(); 
        }
    }   
}