using Microsoft.Win32;
using System;
using System.IO;
using System.Security;
using WindowsRegistryManager.Attributes;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;

namespace WindowsRegistryManager.Services.RegistryKeyInitializers.ConcreteInitializers
{
    [RegistryAccess(Utilities.RootKey.LocalMachine)]
    internal class LocalMachineInitializer : RegistryKeyInitializer
    {
        public override RegistryKey InitializeRegistryKey(WindowsRegistryAccess windowsRegistryAccess)
        {
            RegistryKey registryKey = null;

            try
            {
                registryKey = Registry.LocalMachine.CreateSubKey(windowsRegistryAccess.PathWithoutRoot, true);
            }
            catch (Exception e) when (e is ArgumentNullException || e is SecurityException
                || e is ObjectDisposedException || e is UnauthorizedAccessException || e is IOException)
            {
                Console.WriteLine(e.ToString());
            }

            return registryKey;
        }
    }
}
