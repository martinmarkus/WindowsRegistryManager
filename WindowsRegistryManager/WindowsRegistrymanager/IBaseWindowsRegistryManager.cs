using Microsoft.Win32;
using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManager
{
    public interface IBaseWindowsRegistryManager
    {
        RegistryValueKind RegistryValueKind { get; set; }
        RootKey RootKey { get; set; }
        string PathWithoutRoot { get; set; }
    }
}
