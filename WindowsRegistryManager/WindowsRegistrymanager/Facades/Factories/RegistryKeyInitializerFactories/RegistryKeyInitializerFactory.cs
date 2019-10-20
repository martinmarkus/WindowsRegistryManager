using System;
using System.Reflection;
using WindowsRegistryManager.Attributes;
using WindowsRegistryManager.Services.RegistryKeyInitializers;
using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManager.Facades.Factories.RegistryKeyInitializerFactories
{
    internal class RegistryKeyInitializerFactory : IRegistryKeyInitializerFactory
    {
        public Type GetInitializerType(RootKey rootKey)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type initializerType = null;

            foreach (Type type in assembly.GetTypes())
            {
                RegistryAccessAttribute[] attributes = (RegistryAccessAttribute[])type.GetCustomAttributes(typeof(RegistryAccessAttribute));
                if (attributes == null || attributes.Length <= 0) continue;

                RegistryAccessAttribute attribute;
                attribute = attributes[0];

                if (attribute.RootKey == rootKey)
                {
                    initializerType = type;
                    break;
                }
            }

            return initializerType;
        }

        public IRegistryKeyInitializer CreateInitializer(RootKey rootKey)
        {
            IRegistryKeyInitializer initializer = null;
            Type type = GetInitializerType(rootKey);

            initializer = (IRegistryKeyInitializer)Activator.CreateInstance(type);

            return initializer;
        }
    }
}
