using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;
using WindowsRegistryManager.Facades.Factories.RegistryKeyInitializerFactories;
using WindowsRegistryManager.Services.WindowsRegistryOperators.RegistryReaders;
using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManager.Services
{
    public class WindowsRegistryService : IWindowsRegistryService
    {
        private IWindowsRegistryOperator _windowsRegistryOperator;
        private IRegistryKeyInitializerFactory _registryKeyInitializerFactory;

        public WindowsRegistryService()
        {
            _registryKeyInitializerFactory = new RegistryKeyInitializerFactory();
        }

        public void SetRegistryAccess(RootKey rootKey, string pathWithoutRoot)
        {
            WindowsRegistryAccess windowsRegistryAccess = GetAsWindowsRegistryAccess(rootKey, pathWithoutRoot);
            RegistryKey registryKey = _registryKeyInitializerFactory.InitializeRegistryKey(windowsRegistryAccess);

            if (_windowsRegistryOperator == null)
            {
                _windowsRegistryOperator = new WindowsRegistryOperator(windowsRegistryAccess);
            }
            else
            {
                _windowsRegistryOperator.InitializeRegistryAccess(windowsRegistryAccess);
            }
        }

        public void SetRegistryAccess(string pathWithoutRoot)
        {
            if (_windowsRegistryOperator == null)
            {
                SetRegistryAccess(RootKey.CurrentUser, pathWithoutRoot);
            }
            else
            {
                SetRegistryAccess(_windowsRegistryOperator.WindowsRegistryAccess.RootKey, pathWithoutRoot);
            }
        }

        public T Get<T>() where T : class
        {
            T result = default(T);
            try
            {
                result = _windowsRegistryOperator?.Read<T>();
            }
            catch (SerializationException e)
            {
                throw e;
            }
            return result;
        }

        public void Add<T>(T newValue) where T : class
        {
            try
            {
                _windowsRegistryOperator?.Write(newValue);
            }
            catch (SerializationException e)
            {
                throw e;
            }
        }

        public void AddAll<T>(IList<T> values) where T : class
        {
            _windowsRegistryOperator.WriteAll(values);
        }

        public void Set<T>(T updatedValue) where T : class
        {
            try
            {
                _windowsRegistryOperator?.Write(updatedValue);
            }
            catch (SerializationException e)
            {
                throw e;
            }
        }

        public void Remove()
        {
            _windowsRegistryOperator?.Delete();
        }

        public int GetItemCount()
        {
            if (_windowsRegistryOperator == null)
            {
                return 0;
            }

            return _windowsRegistryOperator.GetActualItemCount();
        }

        private WindowsRegistryAccess GetAsWindowsRegistryAccess(RootKey rootKey, string pathWithoutRoot)
        {
            return new WindowsRegistryAccess(rootKey, pathWithoutRoot);
        }

        public string GetActualRegistryPath()
        {
            return _windowsRegistryOperator?.WindowsRegistryAccess?.PathWithoutRoot;
        }

        public RootKey GetActualRootKey()
        {
            if (_windowsRegistryOperator == null)
            {
                return RootKey.CurrentUser;
            }

            return _windowsRegistryOperator.WindowsRegistryAccess.RootKey;
        }

    }
}
