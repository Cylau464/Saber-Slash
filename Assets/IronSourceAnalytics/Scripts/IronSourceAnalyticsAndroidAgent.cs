#if UNITY_ANDROID && !UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace IronSourceAnalyticsSDK
{
    class IronSourceAnalyticsAndroidAgent : IIronSourceAnalyticsAgent
    {
        private static AndroidJavaObject _androidBridge;
        private readonly static string AndroidBridge = "com.ironsource.unity.analyticsandroidbridge.ISAnalyticsAndroidBridge";

        public AndroidJavaObject getBridge()
        {
            if (_androidBridge == null)
                using (var pluginClass = new AndroidJavaClass(AndroidBridge))
                    _androidBridge = pluginClass.CallStatic<AndroidJavaObject>("getInstance");

            return _androidBridge;
        }

        public void init(string appKey)
        {
            getBridge().Call("setPluginType", "Unity", IronSourceAnalytics.pluginVersion(), IronSourceAnalytics.unityVersion());
            getBridge().Call("initWithAppKey", appKey);
        }

        public void setAppUserId(string userId)
        {
            getBridge().Call("setAppUserId", userId);

        }

        public void setAppResources(ISAnalyticsResourceType resourceType, params string[] values)
        {
            getBridge().Call("setAppResources", resourceType.ToString(), values);
        }

        public void setUserInfo(HashSet<ISAnalyticsMetaData> metaDataList)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (ISAnalyticsMetaData metaData in metaDataList)
            {
                dic.Add(metaData.metaDataPair.Key, metaData.metaDataPair.Value);
            }

            string json = Json.Serialize(dic);

            getBridge().Call("setUserInfo", json);
        }

        public void setUserPrivacy(ISAnalyticsPrivacyRestriction toUpdate, bool restriced, ISAnalyticsReason why)
        {
            getBridge().Call("setUserPrivacy", toUpdate.ToString(), restriced, why.ToString());
        }

        public void setIAPSettings(ISAnalyticsPurchasingType purchasingType, params string[] values)
        {
            getBridge().Call("setIAPSettings", purchasingType.ToString(), values);
        }

        public void updateUserResources(string appResource)
        {
            getBridge().Call("updateUserResources", appResource);
        }

        public void updateProgress(string userProgress)
        {
            getBridge().Call("updateProgress", userProgress);
        }

        public void updateUserPurchase(string appPurchase)
        {
            getBridge().Call("updateUserPurchase", appPurchase);
        }

        public void updateImpressionData(ISAnalyticsMediationName from, string json)
        {
            getBridge().Call("updateImpressionData", from.ToString(), json);
        }

        public void updateCustomActivity(string userActivity)
        {
            getBridge().Call("updateCustomActivity", userActivity);
        }

        public string getSDKVersion()
        {
            return getBridge().Call<string>("getSdkVersion");
        }
    }
}
#endif
