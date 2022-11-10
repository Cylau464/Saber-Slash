using System;
using System.IO;
using IronSourceAnalyticsSDK;
using UnityEditor;
using UnityEngine;

namespace IronSourceAnalyticsSDK
{
    public partial class IronSourceAnalytics : MonoBehaviour
    {
        const string iSAnalyticsSettingsAssetPath = "Assets/Resources/IronSourceAnalytics/Settings.asset";
        private const string iSAnalyticsSettingsFolderPath = "IronSourceAnalytics/Settings";
        private const string iSAnalyticsResourceFolderPath = "/Resources/IronSourceAnalytics";
        private const string resourcesFolder = "/Resources";
        private static ISAnalyticsPreInitSettings _preInitSettings;

        public static ISAnalyticsPreInitSettings preInitSettings
        {
            get
            {
                if(_preInitSettings == null)
                {
                    InitPreInitSettings();
                }

                return _preInitSettings;
            }

            private set{ _preInitSettings = value; }
        }

        private static void InitPreInitSettings()
        {
            try
            {
                _preInitSettings = (ISAnalyticsPreInitSettings)Resources.Load(iSAnalyticsSettingsFolderPath, typeof(ISAnalyticsPreInitSettings));

#if UNITY_EDITOR
                if (_preInitSettings == null)
                {
                    //Create resource folder first
                    if (!Directory.Exists(Application.dataPath + resourcesFolder))
                    {
                        Directory.CreateDirectory(Application.dataPath + resourcesFolder);
                    }

                    //Create ironsourceanalytics folder inside resource
                    if (!Directory.Exists(Application.dataPath + iSAnalyticsResourceFolderPath))
                    {
                        Directory.CreateDirectory(Application.dataPath + iSAnalyticsResourceFolderPath);
                        Debug.LogWarning("IronSourceAnalytics: " + iSAnalyticsResourceFolderPath + " folder was created");
                    }

                    if (File.Exists(iSAnalyticsSettingsAssetPath))
                    {
                        AssetDatabase.DeleteAsset(iSAnalyticsSettingsAssetPath);
                        AssetDatabase.Refresh();
                    }

                    var asset = ScriptableObject.CreateInstance<ISAnalyticsPreInitSettings>();
                    AssetDatabase.CreateAsset(asset, iSAnalyticsSettingsAssetPath);
                    AssetDatabase.Refresh();

                    AssetDatabase.SaveAssets();
                    Debug.LogWarning("IronSourceAnalytics: Created settings file- no settings file exist");
                    Selection.activeObject = asset;

                    //save reference
                    _preInitSettings = asset;
                }
#endif
            }
            catch (System.Exception e)
            {
                Debug.Log("Error getting Settings: " + e.Message);
            }
        }
    }
}
