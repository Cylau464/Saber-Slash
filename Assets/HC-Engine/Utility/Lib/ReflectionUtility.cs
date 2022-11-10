#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace editor
{
    public static class ReflectionUtility
    {
        public static IEnumerable<Type> GetTypesWithAttribute(Type attributeType, bool inherit = true)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.GetCustomAttributes(attributeType, true).Length > 0)
                    {
                        yield return type;
                    }
                }
            }
        }

        public static IEnumerable<Type> GetTypesWithAttribute<TAttribute>(bool inherit = true) where TAttribute : Attribute
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.GetCustomAttributes(typeof(TAttribute), inherit).Length > 0)
                    {
                        yield return type;
                    }
                }
            }
        }

        public static IEnumerable<Type> GetTypesWithAttribute<TAttribute>(Assembly assembly) where TAttribute : System.Attribute
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(TAttribute), true).Length > 0)
                {
                    yield return type;
                }
            }
        }

        public static IEnumerable<FieldInfo> GetAllFields(object target, System.Func<FieldInfo, bool> predicate, BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly)
        {
            if (target == null)
            {
                Debug.LogError("The target object is null. Check for missing scripts.");
                yield break;
            }

            List<Type> types = GetSelfAndBaseTypes(target);

            for (int i = types.Count - 1; i >= 0; i--)
            {
                IEnumerable<FieldInfo> fieldInfos = types[i].GetFields(bindingAttr).Where(predicate);

                foreach (var fieldInfo in fieldInfos)
                {
                    yield return fieldInfo;
                }
            }
        }

        private static List<Type> GetSelfAndBaseTypes(object target)
        {
            List<Type> types = new List<Type>()
            {
                target.GetType()
            };

            while (types.Last().BaseType != null)
            {
                types.Add(types.Last().BaseType);
            }

            return types;
        }
    }
}
#endif