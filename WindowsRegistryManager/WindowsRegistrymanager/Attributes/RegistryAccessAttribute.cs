using System;
using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManager.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    internal class RegistryAccessAttribute : Attribute
    {
        public RootKey RootKey { get; set; }

        public RegistryAccessAttribute(RootKey rootKey)
        {
            RootKey = rootKey;
        }
    }
}
