using System;
using WindowsRegistryManager.Services;
using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManagerExecuter
{
    class Program
    {
        static void Main(string[] args)
        {
            IWindowsRegistryManagerService a = new WindowsRegistryManagerService(RootKey.HKEY_CURRENT_USER, @"Exampe\SubFolder");

            Console.ReadKey();
        }
    }
}
