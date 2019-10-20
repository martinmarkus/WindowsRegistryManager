using System;

namespace WindowsRegistryManagerExecuter
{
    [Serializable]
    class MyClass
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
}
