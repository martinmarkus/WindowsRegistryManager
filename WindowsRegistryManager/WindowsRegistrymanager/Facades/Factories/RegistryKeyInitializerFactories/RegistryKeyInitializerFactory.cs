using System;
using System.Reflection;
using System.Runtime.InteropServices;
using WindowsRegistryManager.Attributes;
using WindowsRegistryManager.Services.RegistryKeyInitializers;
using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManager.Facades.Factories.RegistryKeyInitializerFactories
{
    internal class RegistryKeyInitializerFactory : IRegistryKeyInitializerFactory
    {
        public IRegistryKeyInitializer CreateInitializer(RootKey rootKey)
        {
            IRegistryKeyInitializer initializer = null;
            Type type = GetInitializerType(rootKey);

            try
            {
                initializer = (IRegistryKeyInitializer)Activator.CreateInstance(type);
            }
            catch(Exception e) when (e is ArgumentException || e is ArgumentNullException || e is NotSupportedException
                || e is TargetInvocationException || e is MethodAccessException || e is MemberAccessException
                || e is InvalidComObjectException || e is COMException || e is MissingMethodException || e is TypeLoadException)
            {
                Console.WriteLine(e.ToString());
            }

            return initializer;
        }

        private Type GetInitializerType(RootKey rootKey)
        {
            Type initializerType = null;
            Type[] assemblyTypes = GetAssemblyTypes();

            foreach (Type type in assemblyTypes)
            {
                RegistryAccessAttribute[] attributes = GetRegistryAccessAttributes(type);
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

        private Type[] GetAssemblyTypes()
        {
            Type[] types = null;
            Assembly assembly = Assembly.GetExecutingAssembly();

            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                Console.WriteLine(e.ToString());
            }

            return types;
        }

        private RegistryAccessAttribute[] GetRegistryAccessAttributes(Type type)
        {
            RegistryAccessAttribute[] attributes = null;

            try
            {
                attributes = (RegistryAccessAttribute[])type?.GetCustomAttributes(typeof(RegistryAccessAttribute));
            }
            catch(Exception e) when (e is ArgumentNullException || e is ArgumentException || e is NotSupportedException || e is TypeLoadException)
            {
                Console.WriteLine(e.ToString());
            }

            return attributes;
        }
    }
}
