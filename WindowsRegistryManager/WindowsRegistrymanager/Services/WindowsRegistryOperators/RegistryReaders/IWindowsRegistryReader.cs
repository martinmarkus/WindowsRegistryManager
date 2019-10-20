using System.Collections.Generic;
using WindowsRegistryManager.DataObjects;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;

namespace WindowsRegistryManager.Services.WindowsRegistryOperators.RegistryReaders
{
    internal interface IWindowsRegistryReader : IWindowsRegistryAccessHolder
    {
        RegistryEntity<T> Get<T>(string name);
        ICollection<RegistryEntity<T>> GetAll<T>();
    }
}
