using System;
using System.Linq;
using System.Reflection;

namespace Katas
{
    public static class Reflection
    {
        public static string[] GetMethodNames(object TestObject)
        {
            if (TestObject == null)
                return Array.Empty<string>();
            return TestObject.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Select(m => m.Name).ToArray();
        }

        public static string ConcatStringMembers(object obj)
        {
            if (obj == null)
                return string.Empty;

            var members = obj.GetType()
                .GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static |
                            BindingFlags.Public | BindingFlags.NonPublic);

            var methods = members.Where(m => m.MemberType == MemberTypes.Method)
                .Cast<MethodInfo>()
                .Where(m => m.ReturnType == typeof(string) && m.GetParameters().Length == 0)
                .Select(m => m.Invoke(obj, null))
                .Cast<string>();

            var fields = members.Where(m => m.MemberType == MemberTypes.Field)
                .Cast<FieldInfo>()
                .Where(f => f.FieldType == typeof(string))
                .Select(f => f.GetValue(obj))
                .Cast<string>();

            return methods.Concat(fields)
                .OrderByDescending(s => s.Length)
                .ThenBy(s => s)
                .Aggregate(string.Empty, (n1, n2) => n1 + n2);
        }
    }
}