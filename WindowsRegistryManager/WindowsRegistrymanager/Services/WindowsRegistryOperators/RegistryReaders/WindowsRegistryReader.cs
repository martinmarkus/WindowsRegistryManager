using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WindowsRegistryManager.DataObjects;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;
using WindowsRegistryManager.Facades.Serializers;
using WindowsRegistryManager.Services.RegistryKeyInitializers;

namespace WindowsRegistryManager.Services.WindowsRegistryOperators.RegistryReaders
{
    internal class WindowsRegistryReader : IWindowsRegistryReader
    {
        private WindowsRegistryAccess _windowsRegistryAccess;
        private IRegistryKeyInitializer _registryKeyInitializer;

        private RegistryKey _registryKey;
        private IByteArraySerializer _byteArraySerializer;

        public WindowsRegistryReader(WindowsRegistryAccess windowsRegistryAccess, IRegistryKeyInitializer registryKeyInitializer)
        {
            _registryKeyInitializer = registryKeyInitializer;
            _byteArraySerializer = new ByteArraySerializer();

            InitializeRegistryAccess(windowsRegistryAccess);
        }

        public RegistryEntity<T> Read<T>(string name)
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
            catch (Exception e) when (e is SerializationException || e is InvalidCastException)
            {
                Console.WriteLine(e.ToString());
            }

            return new RegistryEntity<T>(name, result, registryValueKind); ;
        }

        public IList<RegistryEntity<T>> ReadAll<T>()
        {
            string[] names = _registryKey.GetValueNames();

            IList<RegistryEntity<T>> registryEntities = new List<RegistryEntity<T>>();

            foreach (string name in names)
            {
                RegistryEntity<T> registryEntity = Read<T>(name);
                registryEntities.Add(registryEntity);
            }

            return registryEntities;
        }

        public void InitializeRegistryAccess(WindowsRegistryAccess windowsRegistryAccess)
        {
            _registryKey = _registryKeyInitializer.InitializeRegistryKey(windowsRegistryAccess);
            _windowsRegistryAccess = windowsRegistryAccess;
        }

        public int GetActualItemCount()
        {
            return _registryKey.GetValueNames().Length;
        }
    }
}
