using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;

namespace WindowsRegistryManager.Services.WindowsRegistryOperators.RegistryReaders
{
    internal interface IWindowsRegistryOperator : IWindowsRegistryAccessHolder
    {
        T Read<T>(string registryPath) where T : class;
        void Write<T>(T value) where T : class;
        void Delete(string path);

        int GetActualItemCount();
    }
}
