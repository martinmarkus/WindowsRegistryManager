using Microsoft.Win32;
using System.Collections.Generic;
using WindowsRegistryManager.DataObjects;

namespace WindowsRegistryManager.Services.WindowsRegistryWriters
{
    internal interface IWindowsRegistryWriter : IBaseWindowsRegistryManager
    {
        void Write<T>(string name, RegistryEntity<T> registryEntity);
        void WriteAll<T>(IList<RegistryEntity<T>> registryEntities);
    }
}
