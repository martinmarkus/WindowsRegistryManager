using System;
using System.Collections.Generic;
using WindowsRegistryManager.DataObjects;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;

namespace WindowsRegistryManager.Services.WindowsRegistryReaders
{
    class WindowsRegistryReader : IWindowsRegistryReader
    {
        public WindowsRegistryAccess WindowsRegistryAccess { get; set; }

        public WindowsRegistryReader(WindowsRegistryAccess windowsRegistryAccess)
        {
            WindowsRegistryAccess = windowsRegistryAccess;
        }

        public RegistryEntity<T> Get<T>(string name)
        {
            throw new NotImplementedException();
        }

        public IList<RegistryEntity<T>> GetAll<T>()
        {
            throw new NotImplementedException();
        }
    }
}
