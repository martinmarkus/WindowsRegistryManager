using System;
using System.Collections.Generic;
using WindowsRegistryManager.DataObjects;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;

namespace WindowsRegistryManager.Services.WindowsRegistryWriters
{
    class WindowsRegistryWriter : IWindowsRegistryWriter
    {
        public WindowsRegistryAccess WindowsRegistryAccess { get; set; }

        public WindowsRegistryWriter(WindowsRegistryAccess windowsRegistryAccess)
        {
        }

        public WindowsRegistryAccess GetWindowsRegistryAccess()
        {
            return WindowsRegistryAccess;
        }

        public void SetWindowsRegistryAccess(WindowsRegistryAccess windowsRegistryAccess)
        {
            WindowsRegistryAccess = windowsRegistryAccess;
        }

        public void Write<T>(string name, RegistryEntity<T> registryEntity)
        {
            throw new NotImplementedException();
        }

        public void WriteAll<T>(IList<RegistryEntity<T>> registryEntities)
        {
            throw new NotImplementedException();
        }
    }
}
