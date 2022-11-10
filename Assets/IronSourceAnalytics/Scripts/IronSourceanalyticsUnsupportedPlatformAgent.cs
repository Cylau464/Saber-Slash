using System.Collections.Generic;
using UnityEngine;

namespace IronSourceAnalyticsSDK
{
    class IronSourceAnalyticsUnsupportedPlatformAgent : IIronSourceAnalyticsAgent
    {
        public string getSDKVersion()
        {
            return "";
        }

        public void init(string appKey)
        {
            Debug.Log("Unsupported Platform");
        }

        public void setAppResources(ISAnalyticsResourceType resourceType, params string[] values)
        {
            Debug.Log("Unsupported Platform");
        }

        public void setAppUserId(string userId)
        {
            Debug.Log("Unsupported Platform");
        }

        public void setIAPSettings(ISAnalyticsPurchasingType purchasingType, params string[] values)
        {
            Debug.Log("Unsupported Platform");
        }

        public void setUserInfo(HashSet<ISAnalyticsMetaData> metaDataList)
        {
            Debug.Log("Unsupported Platform");
        }

        public void setUserPrivacy(ISAnalyticsPrivacyRestriction toUpdate, bool restriced, ISAnalyticsReason why)
        {
            Debug.Log("Unsupported Platform");
        }

        public void updateUserResources(string appResource)
        {
            Debug.Log("Unsupported Platform");
        }

        public void updateProgress(string userProgress)
        {
            Debug.Log("Unsupported Platform");
        }

        public void updateUserPurchase(string appPurchase)
        {
            Debug.Log("Unsupported Platform");
        }

        public void updateImpressionData(ISAnalyticsMediationName from, string json)
        {
            Debug.Log("Unsupported Platform");
        }

        public void updateCustomActivity(string userActivity)
        {
            Debug.Log("Unsupported Platform");
        }
    }
}