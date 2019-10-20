using WindowsRegistryManager.Services.RegistryKeyInitializers;

namespace WindowsRegistryManager.DataObjects.WindowsRegistryAccess
{
    internal interface IWindowsRegistryAccessHolder
    {
        void InitializeRegistryAccess(WindowsRegistryAccess windowsRegistryAccess);
    }
}
