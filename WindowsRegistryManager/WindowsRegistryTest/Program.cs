using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsRegistryManager.Services;

namespace WindowsRegistryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IWindowsRegistryService windowsRegistryService = new WindowsRegistryService();

            IList<Bar> list = new List<Bar>();
            list.Add(new Bar("fgdsgfds"));
            list.Add(new Bar("gfdsuztrfiu"));
            list.Add(new Bar("oiuzniuz"));
            list.Add(new Bar("8765guzt"));

            windowsRegistryService.SetRegistryAccess(@"Test");
            windowsRegistryService.AddAll(list);
        }
    }
}
