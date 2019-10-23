using Microsoft.Win32;
using StrategySupporter;
using System.Reflection;
using WindowsRegistryManager.Attributes;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;
using WindowsRegistryManager.Services.RegistryKeyInitializers;
using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManager.Facades.Factories.RegistryKeyInitializerFactories
{
    internal class RegistryKeyInitializerFactory : IRegistryKeyInitializerFactory
    {
        private IImplementationFactory _implementationFactory;
        private IRegistryKeyInitializer _registryKeyInitializer;

        private RootKey _rootKey;

        public RegistryKeyInitializerFactory()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            _implementationFactory = new ImplementationFactory(assembly);
        }

        private bool IdentifierFunc(RegistryAccessAttribute registryAccessAttribute)
        {
            return _rootKey == registryAccessAttribute.RootKey;
        }

        public RegistryKey InitializeRegistryKey(WindowsRegistryAccess windowsRegistryAccess)
        {
            _rootKey = windowsRegistryAccess.RootKey;
            _registryKeyInitializer = _implementationFactory.Create<IRegistryKeyInitializer, RegistryAccessAttribute>(IdentifierFunc);

            return _registryKeyInitializer.InitializeRegistryKey(windowsRegistryAccess);
        }
    }
}
