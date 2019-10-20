using System;
using System.Collections.Generic;
using WindowsRegistryManager.Services;
using WindowsRegistryManager.Utilities;

namespace WindowsRegistryManagerExecuter
{
    class Program
    {
        static void Main(string[] args)
        {
            IWindowsRegistryService windowsRegistryService = new WindowsRegistryService(RootKey.CurrentUser, @"Exampe\SubFolder\SubSubFolder");

            MyClass myClass1 = new MyClass(1023, true, "hi");
            MyClass myClass2 = new MyClass(12342340, false, "bye");
            MyClass myClass3 = new MyClass(1340, true, "sup");

            List<MyClass> list = new List<MyClass>();
            list.Add(myClass1);
            list.Add(myClass2);
            list.Add(myClass3);

            windowsRegistryService.Add("myClass2", myClass2);
            windowsRegistryService.Add("myClass3", myClass3);


            IList<MyClass> c = windowsRegistryService.GetAll<MyClass>();

            Console.ReadKey();
        }
    }
}
