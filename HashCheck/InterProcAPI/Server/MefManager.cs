using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace HashCheck.InterProcAPI.Server
{
    public class MefManager
    {
        public static CompositionContainer Container { get; private set; }

        public static void Initialize()
        {
            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));

            Container = new CompositionContainer(catalog);
        }
    }
}
