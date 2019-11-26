using System;
using System.Collections.Generic;
using WindowsRegistryManager.DataObjects.WindowsRegistryAccess;

namespace WindowsRegistryManager.Services.WindowsRegistryOperators.RegistryReaders
{
    internal interface IWindowsRegistryOperator : IWindowsRegistryAccessHolder
    {
        T Read<T>() where T : class;
        void Write<T>(T value) where T : class;
        void WriteAll<T>(IList<T> values) where T : class;
        void Delete();

        int GetActualItemCount();

        WindowsRegistryAccess WindowsRegistryAccess { get; }
    }
}
