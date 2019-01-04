using System;
using System.Collections.Generic;
using System.Reflection;

namespace PrintType
{
    class MainApp
    {
        static void PrintInterfaces(Type[] interfaces)
        {
            Console.WriteLine("-------Interfaces-------");
            foreach(Type i in interfaces)
            Console.WriteLine("Name:{0}",i.Name);
            Console.WriteLine();
        }
        static void PrintFields(FieldInfo[] fields)
        {
            Console.WriteLine("-------Fields-------");
            /*
            FieldInfo[] fields = type.GetFields
            (
            BindingFlags.NonPublic |
            BindingFlags.Public |
            BindingFlags.Static |
            BindingFlags.Instance
            );*/
            foreach(FieldInfo field in fields)
            {
                String accessLevel = "protected";
                if(field.IsPublic)
                accessLevel = "public";
                else if(field.IsPrivate)
                accessLevel = "private";
                Console.WriteLine("Access:{0}, Type:{1}, Name{2}", accessLevel, field.FieldType.Name, field.Name);
            }
            Console.WriteLine();
        }
        static void PrintMethods(MethodInfo[] methods)
        {
            Console.WriteLine("-------Methods-------");
            foreach(MethodInfo method in methods)
            PrintMethod(method);
            Console.WriteLine();
        }
        static void PrintMethod(MethodInfo method)
        {
            String accessLevel = "protected";
            if(method.IsPublic)
            accessLevel = "public";
            else if(method.IsPrivate)
            accessLevel = "private";
            Console.Write("Access:{0}, Type:{1}, Name:{2}, Parameter:", accessLevel, method.ReturnType.Name, method.Name);
            ParameterInfo[] args = method.GetParameters();
            for(int i=0; i<args.Length; i++)
            {
                Console.Write("{0}", args[i].ParameterType.Name);
                if(i<args.Length-1)
                Console.Write(",");
            }
            Console.WriteLine();
        }
        static void PrintProperties(PropertyInfo[] properties)
        {
            Console.WriteLine("-------Properties-------");
            foreach(PropertyInfo property in properties)
            Console.WriteLine("Type:{0}, Name{1}", property.PropertyType.Name, property.Name);
            Console.WriteLine();
        }
        static void PrintEvents(EventInfo[] Events)
        {
            Console.WriteLine("-------Events-------");
            foreach(EventInfo Event in Events)
            Console.WriteLine("Name:"+Event.Name);
            Console.WriteLine();
        }
        public static void Main()
        {
            object obj = new Object();
            // Type type = (Type)obj;
            // Type type = typeof(obj);
            Type type = obj.GetType();

            PrintInterfaces(type.GetInterfaces());
            PrintFields(type.GetFields());
            PrintProperties(type.GetProperties());
            PrintMethods(type.GetMethods());
            PrintEvents(type.GetEvents());
        }
    }
}
