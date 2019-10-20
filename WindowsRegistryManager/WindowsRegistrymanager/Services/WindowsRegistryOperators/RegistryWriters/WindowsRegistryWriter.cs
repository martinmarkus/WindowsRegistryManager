using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using WindowsRegistryManager.DataObjects;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;
using WindowsRegistryManager.Facades.Serializers;
using WindowsRegistryManager.Services.RegistryKeyInitializers;

namespace WindowsRegistryManager.Services.WindowsRegistryOperators.RegistryWriters
{
    internal class WindowsRegistryWriter : IWindowsRegistryWriter
    {
        private WindowsRegistryAccess _windowsRegistryAccess;
        private IRegistryKeyInitializer _registryKeyInitializer;

        private RegistryKey _registryKey;
        private IByteArraySerializer _byteArraySerializer;

        public WindowsRegistryWriter(WindowsRegistryAccess windowsRegistryAccess, IRegistryKeyInitializer registryKeyInitializer)
        {
            _registryKeyInitializer = registryKeyInitializer;
            _byteArraySerializer = new ByteArraySerializer();

            InitializeRegistryAccess(windowsRegistryAccess);
        }

        public void Write<T>(RegistryEntity<T> registryEntity)
        {
            byte[] serializedValue = _byteArraySerializer?.Serialize(registryEntity.Value);

            try
            {
                _registryKey?.SetValue(registryEntity.Name, serializedValue, registryEntity.RegistryValueKind);
            }
            catch (Exception e) when (e is ArgumentException || e is ObjectDisposedException
                || e is UnauthorizedAccessException || e is SecurityException || e is IOException)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void WriteAll<T>(IList<RegistryEntity<T>> registryEntities)
        {
            if (_registryKey == null)
            {
                return;
            }

            foreach(RegistryEntity<T> registryEntity in registryEntities)
            {
                Write(registryEntity);
            }
        }

        public void Remove(string name)
        {
            try
            {
                _registryKey?.DeleteValue(name);
            }
            catch (Exception e) when (e is ArgumentException || e is ObjectDisposedException
                || e is UnauthorizedAccessException || e is SecurityException || e is IOException)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void InitializeRegistryAccess(WindowsRegistryAccess windowsRegistryAccess)
        {
            _registryKey = _registryKeyInitializer?.InitializeRegistryKey(windowsRegistryAccess);
            _windowsRegistryAccess = windowsRegistryAccess;
        }
    }
}
