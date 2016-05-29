namespace Vsts.Vault
{
    using System.ComponentModel.Composition.Hosting;

    /// <summary>
    /// Bootstrapper that setups MEF and works as factory for the VaultService. 
    /// </summary>
    public static class Bootstrapper
    {
        /// <summary>
        /// The container
        /// </summary>
        private static CompositionContainer container;

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
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

        /// <summary>
        /// Gets the vault service.
        /// </summary>
        /// <returns>An IVaultService instance</returns>
        public static IVaultService GetVaultService()
        {
           return Container.GetExportedValue<IVaultService>(); 
        }
    }   
}