using System;

namespace WindowsRegistryManagerExecuter
{
    [Serializable]
    class MyClass
    {
        public MyClass(int v1, bool v2, string v3)
        {
            Asd1 = v1;
            Asd2 = v2;
            Asd3 = v3;
        }

        public int Asd1 { get; set; }
        public bool Asd2 { get; set; }
        public string Asd3 { get; set; }
    }
}
