using Microsoft.Win32;

namespace WindowsRegistryManager.DataObjects
{
    internal class RegistryEntity<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }
        public RegistryValueKind RegistryValueKind { get; set; }
    }
}
