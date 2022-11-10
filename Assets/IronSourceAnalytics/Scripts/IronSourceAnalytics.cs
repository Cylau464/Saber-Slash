

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace IronSourceAnalyticsSDK
{
    public partial class IronSourceAnalytics : MonoBehaviour
    {
        private const string UNITY_PLUGIN_VERSION = "0.2.0.0";
        private static IronSourceAnalytics _instance = null;
        private const string assetsKeyword = "Assets";

        /// <summary>
        /// Call this method to init ironSource Analytics without using Settings UI 
        /// </summary>
        public static void init(string appKey)
        {
            IronSourceAnalyticsAgent.Agent.init(appKey);
        }

        public static string pluginVersion()
        {
            return UNITY_PLUGIN_VERSION;
        }

        public static string unityVersion()
        {
            return Application.unityVersion;
        }

        public static void setAppUserId(string userId)
        {
            IronSourceAnalyticsAgent.Agent.setAppUserId(userId);
        }

        public static void setAppResources(ISAnalyticsResourceType resourceType, params string[] values)
        {
            IronSourceAnalyticsAgent.Agent.setAppResources(resourceType, values);
        }

        public static void setUserInfo(HashSet<ISAnalyticsMetaData> metaDataList)
        {
            IronSourceAnalyticsAgent.Agent.setUserInfo(metaDataList);
        }

        public static void setUserPrivacy(ISAnalyticsPrivacyRestriction toUpdate, bool restricted, ISAnalyticsReason why)
        {
            IronSourceAnalyticsAgent.Agent.setUserPrivacy(toUpdate, restricted, why);
        }

        public static void setIAPSettings(ISAnalyticsPurchasingType purchasingType, string[] values)
        {
            IronSourceAnalyticsAgent.Agent.setIAPSettings(purchasingType, values);
        }

        public static void updateUserResources(ISAnalyticsResourceUpdate appResource)
        {
            IronSourceAnalyticsAgent.Agent.updateUserResources(appResource);
        }

        public static void updateProgress(ISAnalyticsUserProgress userProgress)
        {
            IronSourceAnalyticsAgent.Agent.updateProgress(userProgress);
        }

        public static void updateUserPurchase(ISAnalyticsInAppPurchase appPurchase)
        {
            IronSourceAnalyticsAgent.Agent.updateUserPurchase(appPurchase);
        }

        public static void updateImpressionData(ISAnalyticsMediationName from, string json)
        {
            IronSourceAnalyticsAgent.Agent.updateImpressionData(from, json);
        }

        public static void updateCustomActivity(ISAnalyticsUserActivity userActivity)
        {
            IronSourceAnalyticsAgent.Agent.updateCustomActivity(userActivity);
        }

        public static string getSDKVersion()
        {
            return IronSourceAnalyticsAgent.Agent.getSDKVersion();
        }

        public static string getPluginVersion()
        {
            return UNITY_PLUGIN_VERSION;
        }


        /// <summary>
        /// Locate file under Assets folder(including subdirectories)
        /// </summary>
        public static string whereIs(string file)
        {
            string[] assets = { Path.DirectorySeparatorChar + assetsKeyword + Path.DirectorySeparatorChar };
            FileInfo[] myFile = new DirectoryInfo(assetsKeyword).GetFiles(file, SearchOption.AllDirectories);
            string[] temp = myFile[0].ToString().Split(assets, 2, StringSplitOptions.None);
            return assetsKeyword + Path.DirectorySeparatorChar + temp[1];
        }

        private void Awake()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }


        void OnDestroy()
        {
            if (!Application.isPlaying)
                return;

            if (_instance == this)
                _instance = null;
        }

        /// <summary>
        /// Call this method to init ironSource Analytics using the Settings UI
        /// </summary>
        public static void initWithUISettings()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            setIAPSettingsFromEditor();

            setAppResourcesSettingsFromEditor();

            initWithEditorAppkey();

        }

        private static void initWithEditorAppkey()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            if(preInitSettings.appKeyAndroid != null && preInitSettings.appKeyAndroid.Length > 0) {
                IronSourceAnalytics.init(preInitSettings.appKeyAndroid);
            }
#endif
        }

        /// <summary>
        /// Set IAP Settings params from the Settings UI
        /// </summary>
        private static void setIAPSettingsFromEditor()
        {
            if (preInitSettings.purchasedItemCategories != null && preInitSettings.purchasedItemCategories.Count > 0)
            {
                IronSourceAnalytics.setIAPSettings(ISAnalyticsPurchasingType.ITEM_CATEGORIES, preInitSettings.purchasedItemCategories.ToArray());
            }

            if (preInitSettings.purchasedItems != null && preInitSettings.purchasedItems.Count > 0)
            {
                IronSourceAnalytics.setIAPSettings(ISAnalyticsPurchasingType.PURCHASE_ITEMS, preInitSettings.purchasedItems.ToArray());
            }

            if (preInitSettings.purchasedPlacements != null && preInitSettings.purchasedPlacements.Count > 0)
            {
                IronSourceAnalytics.setIAPSettings(ISAnalyticsPurchasingType.PURCHASE_PLACEMENTS, preInitSettings.purchasedPlacements.ToArray());
            }
        }

        /// <summary>
        /// Set App Resources Settings params from the Settings UI
        /// </summary>
        private static void setAppResourcesSettingsFromEditor()
        {
            if (preInitSettings.appResourceCurrencies != null && preInitSettings.appResourceCurrencies.Count > 0)
            {
                IronSourceAnalytics.setAppResources(ISAnalyticsResourceType.CURRENCIES, preInitSettings.appResourceCurrencies.ToArray());
            }

            if (preInitSettings.appResourcePlacements != null && preInitSettings.appResourcePlacements.Count > 0)
            {
                IronSourceAnalytics.setAppResources(ISAnalyticsResourceType.PLACEMENTS, preInitSettings.appResourcePlacements.ToArray());
            }

            if (preInitSettings.appResourceUserActions != null && preInitSettings.appResourceUserActions.Count > 0)
            {
                IronSourceAnalytics.setAppResources(ISAnalyticsResourceType.USERACTIONS, preInitSettings.appResourceUserActions.ToArray());
            }
        }
    }
}