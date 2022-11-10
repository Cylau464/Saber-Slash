#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace editor
{
    public static class AssetUtility
    {
        private static string FilePath(string directoryPath, string assetsName) => directoryPath + assetsName;

        public static T[] FindScribtableObjectsOfType<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);

            if (guids == null || guids.Length == 0)
                return null;

            T[] a = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;
        }

        public static T FindScribtableObjectOfType<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);

            if (guids == null || guids.Length == 0)
                return null;

            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        public static Object[] FindScribtableObjectsOfType(System.Type type)
        {
            string[] guids = AssetDatabase.FindAssets("t:" + type.Name);

            if (guids == null || guids.Length == 0)
                return null;

            Object[] a = new Object[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath(path, type);
            }

            return a;
        }

        public static Object FindScribtableObjectOfType(System.Type type)
        {
            string[] guids = AssetDatabase.FindAssets("t:" + type.Name);

            if (guids == null || guids.Length == 0)
                return null;

            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath(path, type);
        }

        /// Search for the asset type of T if we find we will return it if no we will create a new one.
        /// 
        public static ScriptableObject GetOrCreateAsset(System.Type type, string directoryPath, string assetsName)
        {
            var settings = AssetDatabase.LoadAssetAtPath(FilePath(directoryPath, assetsName), type);
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance(type).SaveAsset(directoryPath, assetsName);
            }
            return settings as ScriptableObject;
        }

        /// Search for the asset type of T if we find we will return it if no we will create a new one.
        /// 
        public static T GetOrCreateAsset<T>(string directoryPath, string assetsName) where T : ScriptableObject
        {
            var settings = AssetDatabase.LoadAssetAtPath<T>(FilePath(directoryPath, assetsName));
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<T>().SaveAsset(directoryPath, assetsName);
            }
            return settings;
        }

        public static T SaveAsset<T>(this T t, string directoryPath, string assetsName) where T : ScriptableObject
        {
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

            AssetDatabase.CreateAsset(t, FilePath(directoryPath, assetsName));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return t;
        }

        public static void PingObject<T>(this T target) where T : Object
        {
            if (target == null) throw new System.ArgumentNullException("The Object has null variable.");

            Selection.activeObject = target;
            EditorGUIUtility.PingObject(target);
        }

        public static IEnumerable<Object> GetAssetsOfAttribute(System.Type attributeType, bool inherit = true)
        {
            List<Object> objects = new List<Object>();

            foreach (var type in ReflectionUtility.GetTypesWithAttribute(attributeType, inherit))
            {
                objects.AddRange(FindScribtableObjectsOfType(type));
            }

            foreach (var item in objects)
            {
                yield return item;
            }
        }

        public static IEnumerable<TObject> GetAssetsOfAttribute<TObject, TAttribute>(bool inherit = true) where TObject : ScriptableObject where TAttribute : System.Attribute
        {
            List<TObject> objects = new List<TObject>();

            foreach (var type in ReflectionUtility.GetTypesWithAttribute(typeof(TAttribute), inherit))
            {
                objects.AddRange(FindScribtableObjectsOfType(type) as TObject[]);
            }

            foreach(var item in objects)
            {
                yield return item;
            }
        }

        public static IEnumerable<TObject> GetAssetsOfAttribute<TObject>(System.Type attributeType, bool inherit = true) where TObject : ScriptableObject
        {
            List<TObject> objects = new List<TObject>();

            foreach (var type in ReflectionUtility.GetTypesWithAttribute(attributeType, inherit))
            {
                objects.AddRange(FindScribtableObjectsOfType(type) as TObject[]);
            }

            foreach (var item in objects)
            {
                yield return item;
            }
        }
    }
}
#endif