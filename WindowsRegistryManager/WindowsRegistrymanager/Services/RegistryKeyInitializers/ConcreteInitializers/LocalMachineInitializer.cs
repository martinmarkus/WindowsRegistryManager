using Microsoft.Win32;
using System;
using WindowsRegistryManager.Attributes;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;

namespace WindowsRegistryManager.Services.RegistryKeyInitializers.ConcreteInitializers
{
    [RegistryAccess(Utilities.RootKey.LocalMachine)]
    internal class LocalMachineInitializer : BaseRegistryKeyInitializer
    {
        public override RegistryKey InitializeRegistryKey(WindowsRegistryAccess windowsRegistryAccess)
        {
            throw new NotImplementedException();
        }
    }
}
