using Microsoft.Win32;
using WindowsRegistryManager.Services.WindowsRegistryReaders;
using WindowsRegistryManager.Services.WindowsRegistryWriters;
using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManager.Services
{
    public class WindowsRegistryManagerService : IWindowsRegistryManagerService
    {
        public RegistryValueKind RegistryValueKind { get; set; } = RegistryValueKind.DWord;
        public RootKey RootKey { get; set; } = RootKey.HKEY_CURRENT_USER;
        public string PathWithoutRoot { get; set; }

        private IWindowsRegistryWriter _windowsRegistryWriter;
        private IWindowsRegistryReader _windowsRegistryReader;

        public WindowsRegistryManagerService(string pathWithoutRoot)
        {
            PathWithoutRoot = pathWithoutRoot;
        }

        public T Get<T>(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Add<T>(string name, T newValue)
        {
            throw new System.NotImplementedException();
        }

        public void Add<T>(string name, T newValue, RegistryValueKind valueKind)
        {
            throw new System.NotImplementedException();
        }

        public void Set<T>(string name, T updatedValue)
        {
            throw new System.NotImplementedException();
        }

        public void Set<T>(string name, T updatedValue, RegistryValueKind valueKind)
        {
            throw new System.NotImplementedException();
        }

        public void SetRegistryPath(RootKey rootKey, string pathWithoutRoot)
        {
            RootKey = rootKey;
            PathWithoutRoot = pathWithoutRoot;
        }
    }
}
