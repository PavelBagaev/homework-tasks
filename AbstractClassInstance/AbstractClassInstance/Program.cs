using System;
using System.Reflection;

namespace AbstractClassInstance
{
    abstract class AbsractClass
    {
        public AbsractClass()
        {
            Console.WriteLine("Instance of the abstract class: " + this.GetType().Name + " has been created. Yay!");
        }
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            var obj = (AbsractClass)typeof(RuntimeTypeHandle).GetMethod("Allocate", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[] { typeof(AbsractClass) });
            typeof(AbsractClass).GetConstructor(Type.EmptyTypes).Invoke(obj, new object[0]);
            Console.ReadLine();
        }
    }
}
