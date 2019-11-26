using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManager.Services
{
    public interface IWindowsRegistryService
    {
        /// <summary>
        /// Returns an object from the Registry as T which is stored under the passed name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path for the searched value.</param>
        /// <returns name="T"></returns>
        /// <exception cref="System.Runtime.Serialization.SerializationException"></exception> 
        T Get<T>(string path) where T : class;

        /// <summary>
        /// Adds the newValue to the Registry under the passed name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The newly added value will be found under this path in the Registry.</param>
        /// <param name="newValue">The value, which will be added to the Registry.</param>
        /// <exception cref="System.Runtime.Serialization.SerializationException"></exception>
        void Add<T>(string path, T newValue) where T : class;

        /// <summary>
        /// Overwrites a value in the Registry.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The value, which is stored under this path will be overwritten.</param>
        /// <param name="updatedValue">The new content of the overwritten value.</param>
        /// <exception cref="System.Runtime.Serialization.SerializationException"></exception>
        void Set<T>(string path, T updatedValue) where T : class;

        /// <summary>
        /// Sets the currently used RootKey and the Registry path.
        /// </summary>
        void SetRegistryAccess(RootKey rootKey, string pathWithoutRoot);

        /// <summary>
        /// Removes the value identified by the passed path.
        /// </summary>
        /// <param name="path"></param>
        void Remove(string path);

        /// <summary>
        /// Returns the count of the values inside the actually focused subkey.
        /// </summary>
        /// <returns></returns>
        int GetItemCount();
    }
}
