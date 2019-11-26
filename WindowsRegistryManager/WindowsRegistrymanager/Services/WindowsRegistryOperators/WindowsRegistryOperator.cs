using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;
using WindowsRegistryManager.Facades.Factories.RegistryKeyInitializerFactories;

namespace WindowsRegistryManager.Services.WindowsRegistryOperators.RegistryReaders
{
    internal class WindowsRegistryOperator : IWindowsRegistryOperator
    {
        public WindowsRegistryAccess WindowsRegistryAccess { get; private set; }
        private RegistryKey _registryKey;
        private IRegistryKeyInitializerFactory _registryKeyInitializerFactory;
        private int _rangeIndex = 0;

        public WindowsRegistryOperator(WindowsRegistryAccess windowsRegistryAccess)
        {
            _registryKeyInitializerFactory = new RegistryKeyInitializerFactory();
            InitializeRegistryAccess(windowsRegistryAccess);
        }

        public void InitializeRegistryAccess(WindowsRegistryAccess windowsRegistryAccess)
        {
            WindowsRegistryAccess = windowsRegistryAccess;
            _registryKey = _registryKeyInitializerFactory.InitializeRegistryKey(windowsRegistryAccess);
        }

        public void Write<T>(T value) where T : class
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            // ha hibat dob, nincs prop, nem ezt hasznaljuk
            try
            {
                if (IsPrimitive(value.GetType()))
                {
                    dictionary.Add(typeof(T).Name + _rangeIndex, value);
                    _rangeIndex++;
                }
                else
                {
                    dictionary = value.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(value, null));
                }
            }
            catch(Exception e) when (e is TargetParameterCountException)
            {
                Console.WriteLine(e.ToString());
            }

            foreach (KeyValuePair<string, object> entry in dictionary)
            {
                WriteSingleElement(entry.Key, entry.Value);
            }
        }

        private void WriteSingleElement(string name, object value)
        {
            _registryKey.SetValue(name, value, RegistryValueKind.String);
        }

        // primitiv tipusokat tarolo listara
        public void WriteAll<T>(IList<T> values) where T : class
        {
            _rangeIndex = 0;
            bool isPrimitive = IsPrimitive(typeof(T));
            string subFolder = typeof(T).Name + "List";

            if (isPrimitive)
            {
                ChangeToSubFolder(subFolder);
                _rangeIndex++;
            }

            for (int i = 0; i < values.Count; i++)
            {
                if (!isPrimitive)
                {
                    string subFolder2 = subFolder + @"\" + typeof(T).Name + i;
                    ChangeToSubFolder(subFolder2);
                }
                Write(values[i]);
            }

            InitializeRegistryAccess(WindowsRegistryAccess);
        }

        public T Read<T>() where T : class
        {
            Dictionary<string, object> dictionary = ReadToDictionary();

            var serializableT = CreateSerializableObject<T>();
            PropertyInfo[] properties = serializableT.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                try
                {
                    if (!dictionary.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
                        continue;

                    KeyValuePair<string, object> item = dictionary
                        .First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));

                    Type tPropertyType = serializableT.GetType().GetProperty(property.Name).PropertyType;
                    Type newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;

                    object newValue;
                    if (tPropertyType.IsEnum)
                    {
                        newValue = Enum.Parse(tPropertyType, item.Value.ToString());
                    }
                    else if (tPropertyType == typeof(float))
                    {
                        newValue = float.Parse(item.Value.ToString(), CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        newValue = Convert.ChangeType(item.Value.ToString(), newT);
                    }

                    serializableT.GetType().GetProperty(property.Name).SetValue(serializableT, newValue, null);
                }
                catch (Exception e) when (e is ArgumentException || e is TargetException
                    || e is TargetParameterCountException || e is MethodAccessException
                    || e is TargetInvocationException || e is NullReferenceException)
                {
                    Console.WriteLine(e.ToString());
                }

            }

            bool isSubKexExist = IsSubKeyExist(WindowsRegistryAccess.PathWithoutRoot);

            if (!isSubKexExist)
            {
                Write(serializableT);
            }

            return serializableT;
        }

        private Dictionary<string, object> ReadToDictionary()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            string[] keys = new string[1];
            try
            {
                keys = _registryKey.GetValueNames();
            }
            catch (Exception e) when (e is ObjectDisposedException || e is UnauthorizedAccessException
                || e is SecurityException || e is IOException)
            {
                Console.WriteLine(e.ToString());
            }

            foreach (string key in keys)
            {
                object value = new object();

                try
                {
                    value = _registryKey.GetValue(key);
                }
                catch (Exception e) when (e is ArgumentException || e is ObjectDisposedException
                    || e is UnauthorizedAccessException || e is SecurityException || e is IOException)
                {
                    Console.WriteLine(e.ToString());
                }

                dictionary.Add(key, value);
            }

            return dictionary;
        }

        private T CreateSerializableObject<T>() where T : class
        {
            T result = default(T);

            try
            {
                result = (T)Activator.CreateInstance(typeof(T));
            }
            catch (Exception e) when (e is ArgumentException || e is TargetException
                || e is TargetParameterCountException || e is MethodAccessException
                || e is TargetInvocationException || e is NullReferenceException
                || e is NotSupportedException || e is MemberAccessException
                || e is MissingMethodException || e is TypeLoadException ||
                e is InvalidComObjectException || e is COMException)
            {
                Console.WriteLine(e.ToString());
            }

            return result;
        }

        private bool IsSubKeyExist(string registryPath)
        {
            string[] keys = new string[1];
            try
            {
                keys = _registryKey.GetValueNames();
            }
            catch (Exception e) when (e is ObjectDisposedException || e is UnauthorizedAccessException
                || e is SecurityException || e is IOException)
            {
                Console.WriteLine(e.ToString());
            }

            List<string> list = keys.ToList();

            return list.Count > 0;
        }

        public void Delete()
        {
            try
            {
                _registryKey.DeleteSubKeyTree(WindowsRegistryAccess.PathWithoutRoot);
            }
            catch (Exception e) when (e is ArgumentException || e is ObjectDisposedException
                || e is UnauthorizedAccessException || e is SecurityException || e is IOException)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public int GetActualItemCount()
        {
            string[] names = GetValuesNames();
            return names == null ? 0 : names.Length;
        }

        private string[] GetValuesNames()
        {
            string[] names = null;
            try
            {
                names = _registryKey.GetValueNames();
            }
            catch (Exception e) when (e is SecurityException || e is ObjectDisposedException
                || e is IOException || e is UnauthorizedAccessException)
            {
                Console.WriteLine(e.ToString());
            }

            return names;
        }

        private bool IsPrimitive(Type type)
        {
            if (type == null) { return false; }

            return type.IsPrimitive || type == typeof(decimal) || type == typeof(string);
        }

        private void ChangeToSubFolder(string subFolder)
        {
            _registryKey.CreateSubKey(subFolder, true);
            _registryKey = _registryKey.OpenSubKey(subFolder, true);
        }
    }
}
