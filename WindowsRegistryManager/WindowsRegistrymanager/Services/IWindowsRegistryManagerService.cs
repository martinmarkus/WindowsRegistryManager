using System.Collections.Generic;
using WindowsRegistryManager.DataObjects;
using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManager.Services
{
    public interface IWindowsRegistryManagerService
    {
        /// <summary>
        /// Returns an object from the Registry as T which is stored under the passed name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The Name for the searched value.</param>
        /// <returns name="T"></returns>
        T Get<T>(string name);

        /// <summary>
        /// Returns a collection of objects from the Registry.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IList<T> GetAll<T>();

        /// <summary>
        /// Adds the newValue to the Registry under the passed name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The newly added value will be found under this name in the Registry.</param>
        /// <param name="newValue">The value, which will be added to the Registry.</param>
        void Add<T>(string name, T newValue);

        /// <summary>
        /// Adds a collection of T to the Registry.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newValues">The collection of values, which will be added to the Registry.</param>
        void AddAll<T>(IList<T> newValues);

        /// <summary>
        /// Overwrites a value in the Registry.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The value, which is stored under this name will be overwritten.</param>
        /// <param name="updatedValue">The new content of the overwritten value.</param>
        void Set<T>(string name, T updatedValue);

        /// <summary>
        /// Sets the currently used Root key and the Registry path.
        /// </summary>
        void SetRegistryPath(RootKey rootKey, string pathWithoutRoot);
    }
}
