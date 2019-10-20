using System.Collections.Generic;
using WindowsRegistryManager.DataObjects;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;

namespace WindowsRegistryManager.Services.WindowsRegistryOperators.RegistryReaders
{
    internal interface IWindowsRegistryReader : IWindowsRegistryAccessHolder
    {
        RegistryEntity<T> Read<T>(string name);
        IList<RegistryEntity<T>> ReadAll<T>();

        int GetActualItemCount();
    }
}
