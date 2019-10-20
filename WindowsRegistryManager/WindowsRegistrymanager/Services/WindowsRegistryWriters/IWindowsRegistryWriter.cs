using System.Collections.Generic;
using WindowsRegistryManager.DataObjects;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;

namespace WindowsRegistryManager.Services.WindowsRegistryWriters
{
    internal interface IWindowsRegistryWriter : IWindowsRegistryAccessHolder
    {
        void Write<T>(string name, RegistryEntity<T> registryEntity);
        void WriteAll<T>(IList<RegistryEntity<T>> registryEntities);
    }
}
