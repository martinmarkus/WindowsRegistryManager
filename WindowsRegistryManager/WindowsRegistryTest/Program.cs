using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsRegistryManager.Services;
using WindowsRegistryManager.Utilities;

namespace WindowsRegistryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IWindowsRegistryService windowsRegistryService = new WindowsRegistryService();

            IList<string> list = new List<string>();
            list.Add("askjhgdsad");
            list.Add("asdjhgjkhgsad");
            list.Add("asoiuzö9dsad");
            list.Add("asdö98u98sad");

            windowsRegistryService.SetRegistryAccess(@"Test\Registry\Location\One\Two\Three");
            windowsRegistryService.AddAll(list);
        }
    }
}
