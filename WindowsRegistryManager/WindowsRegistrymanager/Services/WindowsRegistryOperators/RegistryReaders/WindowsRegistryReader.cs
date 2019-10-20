using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using WindowsRegistryManager.DataObjects;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;
using WindowsRegistryManager.Facades.Serializers;

namespace WindowsRegistryManager.Services.WindowsRegistryOperators.RegistryReaders
{
    class WindowsRegistryReader : IWindowsRegistryReader
    {
        public WindowsRegistryAccess WindowsRegistryAccess { get; set; }

        private RegistryKey _registryKey;
        private IByteArraySerializer _byteArraySerializer;

        public WindowsRegistryReader(WindowsRegistryAccess windowsRegistryAccess)
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

        public RegistryEntity<T> Get<T>(string name)
        {
            T result = default(T);
            byte[] byteArray = (byte[])_registryKey.GetValue(name);
            RegistryValueKind registryValueKind = _registryKey.GetValueKind(name);

            if (byteArray == null)
            {
                return default(RegistryEntity<T>);
            }

            try
            {
                result = _byteArraySerializer.Deserialize<T>(byteArray);
            }
            catch (SerializationException e)
            {
                //Remove(name);
                Console.WriteLine(e.ToString());
            }

            return new RegistryEntity<T>(name, result, registryValueKind); ;
        }

        public ICollection<RegistryEntity<T>> GetAll<T>()
        {
            string[] names = _registryKey.GetValueNames();

            ICollection<RegistryEntity<T>> registryEntities = new List<RegistryEntity<T>>();

            foreach (string name in names)
            {
                RegistryEntity<T> registryEntity = Get<T>(name);
                registryEntities.Add(registryEntity);
            }

            return registryEntities;
        }
    }
}
