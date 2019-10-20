using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManager.DataObjects.WindowsRegistryAccess
{
    public class WindowsRegistryAccess
    {
        public RootKey RootKey { get; set; }
        public string PathWithoutRoot { get; set; }

        public WindowsRegistryAccess(RootKey rootKey, string pathWithoutRoot)
        {
            RootKey = rootKey;
            PathWithoutRoot = pathWithoutRoot;
        }
    }
}
