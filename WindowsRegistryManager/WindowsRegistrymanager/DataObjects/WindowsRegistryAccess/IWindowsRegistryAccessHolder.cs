namespace WindowsRegistryManager.DataObjects.WindowsRegistryAccess
{
    internal interface IWindowsRegistryAccessHolder
    {
        WindowsRegistryAccess WindowsRegistryAccess { get; set; }
        void InitializeRegistryAccess(WindowsRegistryAccess windowsRegistryAccess);
    }
}
