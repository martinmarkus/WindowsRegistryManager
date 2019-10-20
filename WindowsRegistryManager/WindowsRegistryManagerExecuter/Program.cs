using System;
using WindowsRegistryManager.Services;
using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManagerExecuter
{
    class Program
    {
        static void Main(string[] args)
        {
            IWindowsRegistryService windowsRegistryService = new WindowsRegistryService(RootKey.CurrentUser, @"Exampe\SubFolder");

            Console.ReadKey();
        }
    }
}
