using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManager.Services
{
    public interface IWindowsRegistryService
    {
        /// <summary>
        /// Returns an object from the Registry as T which is stored under the actual path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns name="T"></returns>
        /// <exception cref="System.Runtime.Serialization.SerializationException"></exception> 
        T Get<T>() where T : class;

        /// <summary>
        /// Adds the newValue to the Registry under the actual path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newValue">The value, which will be added to the Registry.</param>
        /// <exception cref="System.Runtime.Serialization.SerializationException"></exception>
        void Add<T>(T newValue) where T : class;

        /// <summary>
        /// Overwrites a value in the Registry.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updatedValue">The new content of the overwritten value.</param>
        /// <exception cref="System.Runtime.Serialization.SerializationException"></exception>
        void Set<T>(T updatedValue) where T : class;

        /// <summary>
        /// Sets the currently used RootKey and the Registry path.
        /// </summary>
        void SetRegistryAccess(RootKey rootKey, string pathWithoutRoot);

        /// <summary>
        /// Sets the currently used Registry path.
        /// </summary>
        void SetRegistryAccess(string pathWithoutRoot);

        /// <summary>
        /// Removes the value identified by the path.
        /// </summary>
        void Remove();

        /// <summary>
        /// Returns the count of the values inside the actually focused subkey.
        /// </summary>
        /// <returns></returns>
        int GetItemCount();

        string GetActualRegistryPath();
        RootKey GetActualRootKey();
    }
}
