using System;
using System.IO;
using System.Security;
using Microsoft.Win32;
using WindowsRegistryManager.Attributes;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;

namespace WindowsRegistryManager.Services.RegistryKeyInitializers.ConcreteInitializers
{
    [RegistryAccess(Utilities.RootKey.PerformanceData)]
    internal class PerformanceDataInitializer : RegistryKeyInitializer
    {
        public override RegistryKey InitializeRegistryKey(WindowsRegistryAccess windowsRegistryAccess)
        {
            RegistryKey registryKey = null;

            try
            {
                registryKey = Registry.PerformanceData.CreateSubKey(windowsRegistryAccess.PathWithoutRoot);
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
