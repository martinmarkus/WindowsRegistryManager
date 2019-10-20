using Microsoft.Win32;
using System;
using System.Collections.Generic;
using WindowsRegistryManager.DataObjects;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;
using WindowsRegistryManager.Facades.Factories.RegistryKeyInitializerFactories;
using WindowsRegistryManager.Services.RegistryKeyInitializers;
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

        public RegistryValueKind RegistryValueKind { get; set; } = RegistryValueKind.DWord;

        public WindowsRegistryService(RootKey rootKey, string pathWithoutRoot)
        {
            WindowsRegistryAccess windowsRegistryAccess = GetAsWindowsRegistryAccess(rootKey, pathWithoutRoot);
            _registryKeyInitializerFactory = new RegistryKeyInitializerFactory();
            IRegistryKeyInitializer registryKeyInitializer = _registryKeyInitializerFactory.CreateInitializer(rootKey);
            _windowsRegistryWriter = new WindowsRegistryWriter(windowsRegistryAccess, registryKeyInitializer);
            _windowsRegistryReader = new WindowsRegistryReader(windowsRegistryAccess, registryKeyInitializer);

        }

        public T Get<T>(string name)
        {
            RegistryEntity<T> registryEntity = _windowsRegistryReader.Read<T>(name);
            return registryEntity.Value;
        }

        public IList<T> GetAll<T>()
        {
            IList<RegistryEntity<T>> registryEntities = _windowsRegistryReader.ReadAll<T>();
            IList<T> result = new List<T>();

            foreach(RegistryEntity<T> registryEntity in registryEntities)
            {
                result.Add(registryEntity.Value);
            }

            return result;
        }

        public void Add<T>(string name, T newValue)
        {
            RegistryEntity<T> registryEntity = new RegistryEntity<T>(name, newValue, RegistryValueKind);
            _windowsRegistryWriter.Write(registryEntity);
        }

        public void AddAll<T>(IList<T> newValues)
        {
            IList<RegistryEntity<T>> registryEntities = new List<RegistryEntity<T>>();
            int actualItemCount = _windowsRegistryReader.GetActualItemCount();

            for (int i = 0; i < newValues.Count; i++)
            {
                T newValue = newValues[i];

                string nextId = Convert.ToString(actualItemCount + i);
                RegistryEntity<T> registryEntity = new RegistryEntity<T>(nextId, newValue, RegistryValueKind);
                registryEntities.Add(registryEntity);
            }

            _windowsRegistryWriter.WriteAll(registryEntities);
        }

        public void Set<T>(string name, T updatedValue)
        {
            RegistryEntity<T> registryEntity = new RegistryEntity<T>(name, updatedValue, RegistryValueKind);
            _windowsRegistryWriter.Write(registryEntity);
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
