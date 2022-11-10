#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace editor
{
    public static class EditorManager
    {
        /// <summary>
        /// Find all object of TObject activate and non activate in array.
        /// </summary>
        /// <typeparam name="TObject"> Type of object that you searching for.</typeparam>
        /// <returns> Array of TObjects </returns>
        public static TObject FindScenesComponent<TObject>()
        {
            List<TObject> objectsInScene = new List<TObject>();

            foreach (Object go in Resources.FindObjectsOfTypeAll(typeof(Object)) as Object[])
            {
                GameObject cGO = go as GameObject;
                if (cGO != null && !EditorUtility.IsPersistent(cGO.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                {
                    TObject result = cGO.GetComponent<TObject>();
                    if (result != null)
                        return result;
                }
            }

            return default;
        }

        /// <summary>
        /// Find all object of TObject activate and non activate in array.
        /// </summary>
        /// <typeparam name="TObject"> Type of object that you searching for.</typeparam>
        /// <returns> Array of TObjects </returns>
        public static Object FindScenesComponent(System.Type type)
        {
            foreach (Object go in Resources.FindObjectsOfTypeAll(typeof(Object)) as Object[])
            {
                GameObject cGO = go as GameObject;
                if (cGO != null && !EditorUtility.IsPersistent(cGO.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                {
                    Object result = cGO.GetComponent(type);
                    if (result != null) return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Find all object of Object activate and non activate in all activate scenes and put it in array.
        /// </summary>
        /// <returns> Array of Objects </returns>
        public static Object[] FindAllSceneObjects()
        {
            List<Object> objectsInScene = new List<Object>();

            foreach (Object go in Resources.FindObjectsOfTypeAll(typeof(Object)) as Object[])
            {
                GameObject cGO = go as GameObject;
                if (cGO != null && !EditorUtility.IsPersistent(cGO.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                {
                    objectsInScene.Add(go);
                }
            }

            return objectsInScene.ToArray();
        }

        /// <summary>
        /// Find all object of TObject activate and non activate in array.
        /// </summary>
        /// <typeparam name="TObject"> Type of object that you searching for.</typeparam>
        /// <returns> Array of TObjects </returns>
        public static TObject[] FindScenesComponents<TObject>()
        {
            List<TObject> objectsInScene = new List<TObject>();

            foreach (Object go in Resources.FindObjectsOfTypeAll(typeof(Object)) as Object[])
            {
                GameObject cGO = go as GameObject;
                if (cGO != null && !EditorUtility.IsPersistent(cGO.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                {
                    TObject result = cGO.GetComponent<TObject>();
                    if (result != null)
                        objectsInScene.Add(result);
                }
            }

            return objectsInScene.ToArray();
        }

        /// <summary>
        /// Find all object of TObject activate and non activate in array.
        /// </summary>
        /// <typeparam name="TObject"> Type of object that you searching for.</typeparam>
        /// <returns> Array of TObjects </returns>
        public static Object[] FindScenesComponents(System.Type type)
        {
            List<Object> objectsInScene = new List<Object>();

            foreach (Object go in Resources.FindObjectsOfTypeAll(typeof(Object)) as Object[])
            {
                GameObject cGO = go as GameObject;
                if (cGO != null && !EditorUtility.IsPersistent(cGO.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                {
                    Object result = cGO.GetComponent(type);
                    if (result != null) objectsInScene.Add(result);
                }
            }

            return objectsInScene.ToArray();
        }

        /// <summary>
        /// Save all active scenes amd assets.
        /// </summary>
        public static void SaveGame()
        {
            ScenesEditor.SaveAllActiveScenes();
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// Save all active scenes amd assets.
        /// </summary>
        public static void SaveObject(this Object @object)
        {
            if (@object == null) throw new System.ArgumentNullException("The object has a null value!...");
            EditorUtility.SetDirty(@object);
            AssetDatabase.SaveAssets();
            EditorUtility.ClearDirty(@object);
        }

        public static void SaveObjectsOfType<T>() where T : ScriptableObject
        {
            T[] ts = AssetUtility.FindScribtableObjectsOfType<T>();

            foreach (T t in ts)
            {
                EditorUtility.SetDirty(t);
            }

            AssetDatabase.SaveAssets();

            foreach (T t in ts)
            {
                EditorUtility.ClearDirty(t);
            }
        }
        /// <summary>
        /// Save all objects after
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objects"></param>
        public static void SaveObjectsOfType(this Object[] objects)
        {
            foreach (Object t in objects)
            {
                EditorUtility.SetDirty(t);
            }

            AssetDatabase.SaveAssets();

            foreach (Object t in objects)
            {
                EditorUtility.ClearDirty(t);
            }
        }

        /// <summary>
        /// Find any assets in the project with type of TObject or ScriptableObject.
        /// </summary>
        /// <typeparam name="TObject"> Is the type of TObject or ScriptableObject </typeparam>
        /// <returns></returns>
        public static TObject[] FindAllAssetsOfType<TObject>()
        {
            List<TObject> objects = new List<TObject>();
            objects.AddRange(AssetUtility.FindScribtableObjectsOfType<ScriptableObject>().OfType<TObject>());
            objects.AddRange(FindScenesComponents<TObject>());

            return objects.ToArray();
        }
    }
}
#endif