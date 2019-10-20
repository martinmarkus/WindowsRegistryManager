using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using WindowsRegistryManager.DataObjects;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;
using WindowsRegistryManager.Facades.Serializers;

namespace WindowsRegistryManager.Services.WindowsRegistryOperators.RegistryWriters
{
    class WindowsRegistryWriter : IWindowsRegistryWriter
    {
        public WindowsRegistryAccess WindowsRegistryAccess { get; set; }

        private RegistryKey _registryKey;
        private IByteArraySerializer _byteArraySerializer;

        public WindowsRegistryWriter(WindowsRegistryAccess windowsRegistryAccess)
        {
            WindowsRegistryAccess = windowsRegistryAccess;
            _byteArraySerializer = new ByteArraySerializer();

            InitializeRegistryKey();
        }

        private void InitializeRegistryKey()
        {
            try
            {
                _registryKey = Registry.CurrentUser.CreateSubKey(WindowsRegistryAccess.PathWithoutRoot);
            }
            catch (Exception e) when (e is ArgumentException || e is ObjectDisposedException
                || e is UnauthorizedAccessException || e is SecurityException || e is IOException)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Write<T>(RegistryEntity<T> registryEntity)
        {
            byte[] serializedValue = _byteArraySerializer.Serialize(registryEntity.Value);

            try
            {
                _registryKey.SetValue(registryEntity.Name, serializedValue, registryEntity.RegistryValueKind);
            }
            catch (Exception e) when (e is ArgumentException || e is ObjectDisposedException
                || e is UnauthorizedAccessException || e is SecurityException || e is IOException)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void WriteAll<T>(ICollection<RegistryEntity<T>> registryEntities)
        {
            foreach(RegistryEntity<T> registryEntity in registryEntities)
            {
                Write(registryEntity);
            }
        }
    }
}
