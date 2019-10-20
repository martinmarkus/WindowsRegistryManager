using Microsoft.Win32;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;

namespace WindowsRegistryManager.Services.RegistryKeyInitializers
{
    internal abstract class BaseRegistryKeyInitializer : IBaseRegistryKeyInitializer
    {
        public abstract RegistryKey InitializeRegistryKey(WindowsRegistryAccess windowsRegistryAccess);
    }
}
