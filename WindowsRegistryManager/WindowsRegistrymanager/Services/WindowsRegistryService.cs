using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WindowsRegistryManager.DataObjects;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;
using WindowsRegistryManager.Facades.Factories.RegistryKeyInitializerFactories;
using WindowsRegistryManager.Services.WindowsRegistryOperators.RegistryReaders;
using WindowsRegistryManager.Services.WindowsRegistryOperators.RegistryWriters;
using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManager.Services
{
    public class WindowsRegistryService : IWindowsRegistryService
    {
        private IWindowsRegistryWriter _windowsRegistryWriter;
        private IWindowsRegistryReader _windowsRegistryReader;
        private IRegistryKeyInitializerFactory _registryKeyInitializerFactory;
        private RegistryValueKind _registryValueKind = RegistryValueKind.Binary;

        public WindowsRegistryService(RootKey rootKey, string pathWithoutRoot)
        {
            _registryKeyInitializerFactory = new RegistryKeyInitializerFactory();
            WindowsRegistryAccess windowsRegistryAccess = GetAsWindowsRegistryAccess(rootKey, pathWithoutRoot);

            RegistryKey registryKey = _registryKeyInitializerFactory.InitializeRegistryKey(windowsRegistryAccess);

            _windowsRegistryWriter = new WindowsRegistryWriter(windowsRegistryAccess, registryKey);
            _windowsRegistryReader = new WindowsRegistryReader(windowsRegistryAccess, registryKey);
        }

        public T Get<T>(string name)
        {
            RegistryEntity<T> registryEntity = null;
            try
            {
                registryEntity = _windowsRegistryReader.Read<T>(name);
            }
            catch (SerializationException e)
            {
                throw e;
            }
            return registryEntity.Value;
        }

        public IList<T> GetAll<T>()
        {
            IList<RegistryEntity<T>> registryEntities = _windowsRegistryReader.ReadAll<T>();
            IList<T> result = new List<T>();

            try
            {
                registryEntities = _windowsRegistryReader.ReadAll<T>();
            }
            catch (SerializationException e)
            {
                throw e;
            }

            foreach (RegistryEntity<T> registryEntity in registryEntities)
            {
                result.Add(registryEntity.Value);
            }

            return result;
        }

        public void Add<T>(string name, T newValue)
        {
            RegistryEntity<T> registryEntity = new RegistryEntity<T>(name, newValue, _registryValueKind);

            try
            {
                _windowsRegistryWriter.Write(registryEntity);
            }
            catch (SerializationException e)
            {
                throw e;
            }
        }

        public void AddAll<T>(IList<T> newValues)
        {
            IList<RegistryEntity<T>> registryEntities = new List<RegistryEntity<T>>();
            int actualItemCount = _windowsRegistryReader.GetActualItemCount();

            try
            {
                for (int i = 0; i < newValues.Count; i++)
                {
                    T newValue = newValues[i];

                    string nextId = Convert.ToString(actualItemCount + i);
                    RegistryEntity<T> registryEntity = new RegistryEntity<T>(nextId, newValue, _registryValueKind);
                    registryEntities.Add(registryEntity);
                }

                _windowsRegistryWriter.WriteAll(registryEntities);
            }
            catch (SerializationException e)
            {
                throw e;
            }
        }

        public void Set<T>(string name, T updatedValue)
        {
            RegistryEntity<T> registryEntity = new RegistryEntity<T>(name, updatedValue, _registryValueKind);

            try
            {
                _windowsRegistryWriter.Write(registryEntity);
            }
            catch (SerializationException e)
            {
                throw e;
            }
        }

        public void Remove(string name)
        {
            _windowsRegistryWriter.Remove(name);
        }

        public void SetRegistryAccess(RootKey rootKey, string pathWithoutRoot)
        {
            WindowsRegistryAccess windowsRegistryAccess = GetAsWindowsRegistryAccess(rootKey, pathWithoutRoot);

            _windowsRegistryWriter.InitializeRegistryAccess(windowsRegistryAccess);
            _windowsRegistryReader.InitializeRegistryAccess(windowsRegistryAccess);          
        }

        public int GetItemCount()
        {
            return _windowsRegistryReader.GetActualItemCount();
        }

        private WindowsRegistryAccess GetAsWindowsRegistryAccess(RootKey rootKey, string pathWithoutRoot)
        {
            return new WindowsRegistryAccess(rootKey, pathWithoutRoot); ;
        }
    }
}
