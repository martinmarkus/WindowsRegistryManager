using System.Collections.Generic;
using WindowsRegistryManager.DataObjects;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;

namespace WindowsRegistryManager.Services.WindowsRegistryOperators.RegistryWriters
{
    internal interface IWindowsRegistryWriter : IWindowsRegistryAccessHolder
    {
        void Write<T>(RegistryEntity<T> registryEntity);
        void WriteAll<T>(IList<RegistryEntity<T>> registryEntities);
    }
}
