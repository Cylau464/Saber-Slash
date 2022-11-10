using UnityEngine;
using UnityEditor;

namespace IronSourceAnalyticsSDK.Editor
{
    public static class IronSourceAnalyticsMenu
    {
        [MenuItem("Window/IronSourceAnalytics/Documentation", false, 0)]
        public static void Documentation()
        {
            Application.OpenURL("https://developers.is.com/ironsource-mobile/android/app-analytics-sdk-unity/");
        }

        [MenuItem("Window/IronSourceAnalytics/Platform Login", false, 1)]
        public static void Platform()
        {
            Application.OpenURL("https://platform.ironsrc.com/");
        }

        [MenuItem("Window/IronSourceAnalytics/Edit Settings", false, 3)]
        public static void SelectISAnalyticsSetting()
        {
            Selection.activeObject = IronSourceAnalytics.preInitSettings;
        }

        [MenuItem("Window/IronSourceAnalytics/Create ironSource App Analytics GameObject", false, 2)]
        public static void AddIronSourceAppAnalyticsObject()
        {
            var iSAnalyticsObject = Object.FindObjectOfType(typeof(IronSourceAnalytics));
            if (iSAnalyticsObject != null)
            {
                Object.DestroyImmediate(((IronSourceAnalytics)iSAnalyticsObject).gameObject);
            }

            GameObject gameObject =
                PrefabUtility.InstantiatePrefab(
                    AssetDatabase.LoadAssetAtPath(IronSourceAnalytics.whereIs("IronSourceAnalytics.prefab"),
                        typeof(GameObject))) as GameObject;
            gameObject.name = "IronSourceAnalytics";
            Selection.activeObject = gameObject;
            Undo.RegisterCreatedObjectUndo(gameObject, "Created IronSourceAnalytics GameObject");

            if (iSAnalyticsObject == null)
            {
                Debug.Log("Created IronSourceAnalytics GameObjectObject");
            }
            else
            {
                Debug.Log("Updated existing IronSourceAnalytics Object");
            }
        }
    }
}
