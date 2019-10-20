﻿using System.Collections.Generic;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;
using WindowsRegistryManager.Services.WindowsRegistryOperators.RegistryReaders;
using WindowsRegistryManager.Services.WindowsRegistryOperators.RegistryWriters;
using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManager.Services
{
    public class WindowsRegistryService : IWindowsRegistryService
    {
        private IWindowsRegistryWriter _windowsRegistryWriter;
        private IWindowsRegistryReader _windowsRegistryReader;

        public WindowsRegistryService(RootKey rootKey, string pathWithoutRoot)
        {
            WindowsRegistryAccess windowsRegistryAccess = GetAsWindowsRegistryAccess(rootKey, pathWithoutRoot);

            _windowsRegistryWriter = new WindowsRegistryWriter(windowsRegistryAccess);
            _windowsRegistryReader = new WindowsRegistryReader(windowsRegistryAccess); 
        }

        public T Get<T>(string name)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<T> GetAll<T>()
        {
            throw new System.NotImplementedException();
        }

        public void Add<T>(string name, T newValue)
        {
            throw new System.NotImplementedException();
        }

        public void AddAll<T>(ICollection<T> newValues)
        {
            throw new System.NotImplementedException();
        }

        public void Set<T>(string name, T updatedValue)
        {
            throw new System.NotImplementedException();
        }

        public void SetRegistryPath(RootKey rootKey, string pathWithoutRoot)
        {
            WindowsRegistryAccess windowsRegistryAccess = GetAsWindowsRegistryAccess(rootKey, pathWithoutRoot);

            _windowsRegistryWriter.WindowsRegistryAccess = windowsRegistryAccess;
            _windowsRegistryReader.WindowsRegistryAccess = windowsRegistryAccess;
        }

        private WindowsRegistryAccess GetAsWindowsRegistryAccess(RootKey rootKey, string pathWithoutRoot)
        {
            return new WindowsRegistryAccess(rootKey, pathWithoutRoot); ;
        }
    }
}