using System.Collections.Generic;
using WindowsRegistryManager.DataObjects;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;

namespace WindowsRegistryManager.Services.WindowsRegistryReaders
{
    internal interface IWindowsRegistryReader : IWindowsRegistryAccessHolder
    {
        RegistryEntity<T> Get<T>(string name);
        IList<RegistryEntity<T>> GetAll<T>();
    }
}
