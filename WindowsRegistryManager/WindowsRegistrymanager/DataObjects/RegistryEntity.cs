using Microsoft.Win32;

namespace WindowsRegistryManager.DataObjects
{
    internal class RegistryEntity<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }
        public RegistryValueKind RegistryValueKind { get; set; }

        public RegistryEntity(string name, T value, RegistryValueKind registryValueKind)
        {
            Name = name;
            Value = value;
            RegistryValueKind = registryValueKind;
        }
    }
}
