# WindowsRegistryManager
A generic Registry Reader and Writer library in .NET. The project depends on my StrategySupporter library.

Example
The following examples show how to use the basic methods of the library.

### Step 1
Define your class, which will be written to the Windows Registry, and tag it with [Serializable].
````
[Serializable]
public class MyClass
{
    public string Name { get; set; }
    public int Age { get; set; }
    public bool IsProgrammer { get; set; }

    public MyClass(string name, int age, bool isProgrammer)
    {
        Name = name;
        Age = age;
        IsProgrammer = isProgrammer;
    }
}
````

### Step 2
Instantiate the WindowsRegistryService.

````
// Define the registry root in which you want to read/write.
RootKey rootKey = RootKey.CurrentUser;

// Define the absolute Registry path, where you want to read/write.
string registryPathWithoutRoot = @"Exampe\SubFolder\SubSubFolder";

// Instantiate the WindowsRegistryService.
IWindowsRegistryService windowsRegistryService = new WindowsRegistryService(rootKey, registryPathWithoutRoot);
````

### Step 3 - Writing to Registry

````
// Define the object you want to write out to Registry.
MyClass object = new MyClass("Joe", 30, true);

// Writing one element:
string registryEntryName = "object1";
windowsRegistryService.Add(registryEntryName, object1);
````

### Step 4 - Reading from Registry
````
MyClass readOutObject = windowsRegistryService.Get(registryEntryName);
````
