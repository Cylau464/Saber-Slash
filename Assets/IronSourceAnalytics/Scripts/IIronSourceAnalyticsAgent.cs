using System.Collections.Generic;

namespace IronSourceAnalyticsSDK
{
    interface IIronSourceAnalyticsAgent
    {
        void init(string appKey);
        void setAppUserId(string userId);
        void setAppResources(ISAnalyticsResourceType resourceType, params string[] values);
        void setUserInfo(HashSet<ISAnalyticsMetaData> metaDataList);
        void setUserPrivacy(ISAnalyticsPrivacyRestriction toUpdate, bool restriced, ISAnalyticsReason why);
        void setIAPSettings(ISAnalyticsPurchasingType purchasingType, params string[] values);
        void updateUserResources(string appResource);
        void updateProgress(string userProgress);
        void updateUserPurchase(string appPurchase);
        void updateImpressionData(ISAnalyticsMediationName from, string json);
        void updateCustomActivity(string userActivity);
        string getSDKVersion();
    }
}
