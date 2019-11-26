using Microsoft.Win32;
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

        public T Get<T>(string path) where T : class
        {
            T result = default(T);
            try
            {
                result = _windowsRegistryOperator.Read<T>(path);
            }
            catch (SerializationException e)
            {
                throw e;
            }
            return result;
        }

        public void Add<T>(string path, T newValue) where T : class
        {
            try
            {
                _windowsRegistryOperator.Write(newValue);
            }
            catch (SerializationException e)
            {
                throw e;
            }
        }

        public void Set<T>(string path, T updatedValue) where T : class
        {
            try
            {
                _windowsRegistryOperator.Write(updatedValue);
            }
            catch (SerializationException e)
            {
                throw e;
            }
        }

        public void Remove(string path)
        {
            _windowsRegistryOperator.Delete(path);
        }

        public int GetItemCount()
        {
            return _windowsRegistryOperator.GetActualItemCount();
        }

        private WindowsRegistryAccess GetAsWindowsRegistryAccess(RootKey rootKey, string pathWithoutRoot)
        {
            return new WindowsRegistryAccess(rootKey, pathWithoutRoot);
        }
    }
}
