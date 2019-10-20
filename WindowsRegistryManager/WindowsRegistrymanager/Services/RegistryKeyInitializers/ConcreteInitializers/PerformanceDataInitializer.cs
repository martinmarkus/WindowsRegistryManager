using System;
using Microsoft.Win32;
using WindowsRegistryManager.Attributes;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;

namespace WindowsRegistryManager.Services.RegistryKeyInitializers.ConcreteInitializers
{
    [RegistryAccess(Utilities.RootKey.PerformanceData)]
    internal class PerformanceDataInitializer : BaseRegistryKeyInitializer
    {
        public override RegistryKey InitializeRegistryKey(WindowsRegistryAccess windowsRegistryAccess)
        {
            throw new NotImplementedException();
        }
    }
}
