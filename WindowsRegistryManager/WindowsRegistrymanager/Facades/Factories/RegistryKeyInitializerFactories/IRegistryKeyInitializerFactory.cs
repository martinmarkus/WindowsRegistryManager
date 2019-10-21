using System;
using WindowsRegistryManager.Services.RegistryKeyInitializers;
using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManager.Facades.Factories.RegistryKeyInitializerFactories
{
    internal interface IRegistryKeyInitializerFactory
    {
        IRegistryKeyInitializer CreateInitializer(RootKey rootKey);
    }
}
