using Microsoft.Win32;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;

namespace WindowsRegistryManager.Facades.Factories.RegistryKeyInitializerFactories
{
    internal interface IRegistryKeyInitializerFactory
    {
        RegistryKey InitializeRegistryKey(WindowsRegistryAccess windowsRegistryAccess);
    }
}
