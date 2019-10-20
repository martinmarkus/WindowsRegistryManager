using System.Collections.Generic;
using WindowsRegistryManager.DataObjects;

namespace WindowsRegistryManager.Services.WindowsRegistryReaders
{
    internal interface IWindowsRegistryReader : IBaseWindowsRegistryManager
    {
        RegistryEntity<T> Get<T>(string name);
        IList<RegistryEntity<T>> GetAll<T>();
    }
}
