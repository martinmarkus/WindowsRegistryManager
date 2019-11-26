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

            windowsRegistryService.SetRegistryAccess(RootKey.CurrentUser, @"Test\Registry\Location\One\Two");

            Foo foo = new Foo();
            foo.Value1 = 10;
            foo.Value2 = 10.3f;
            foo.Value3 = true;
            foo.Value4 = "hello world";

            IList<string> list = new List<string>();
            list.Add("asdsad");
            list.Add("wtrrw");
            list.Add("rewfre");
            foo.List = list;

            windowsRegistryService.Add<Foo>(foo);
        }
    }
}
