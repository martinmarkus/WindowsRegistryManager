using Microsoft.Win32;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;

namespace WindowsRegistryManager.Services.RegistryKeyInitializers
{
    internal interface IRegistryKeyInitializer
    {
        RegistryKey InitializeRegistryKey(WindowsRegistryAccess windowsRegistryAccess);
    }
}
