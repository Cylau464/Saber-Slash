using System.Collections.Generic;

namespace IronSourceAnalyticsSDK
{
    class IronSourceAnalyticsAgent
    {
        private const string CURRENCY_KEY = "currency";
        private const string PLACEMENT_KEY = "placement";
        private const string ACTION_KEY = "action";
        private const string USER_ACTION_KEY = "userAction";
        private const string AMOUNT_KEY = "amount";
        private const string BALANCE_KEY = "balance";
        private const string LEVEL1_KEY = "lvl1";
        private const string LEVEL2_KEY = "lvl2";
        private const string LEVEL3_KEY = "lvl3";
        private const string ATTEMPT_KEY = "attempt";
        private const string SCORE_KEY = "score";
        private const string STATE_KEY = "state";
        private const string CATEGORY_KEY = "category";
        private const string ITEM_KEY = "item";
        private const string PAID_KEY = "paid";

        private const string ACTIVITY_NAME_KEY = "name";
        private const string ACTIVITY_PROPERTY_KEY = "property";


        private IIronSourceAnalyticsAgent _platformAgent;
        private static IronSourceAnalyticsAgent _instance;

        private IronSourceAnalyticsAgent()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
        _platformAgent = new IronSourceAnalyticsAndroidAgent ();
#else
            _platformAgent = new IronSourceAnalyticsUnsupportedPlatformAgent();
#endif
        }

        public static IronSourceAnalyticsAgent Agent
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new IronSourceAnalyticsAgent();
                }
                return _instance;
            }
        }

        public void init(string appKey)
        {
            _platformAgent.init(appKey);
        }

        public void setAppUserId(string userId)
        {
            _platformAgent.setAppUserId(userId);
        }

        public void setAppResources(ISAnalyticsResourceType resourceType, params string[] values)
        {
            _platformAgent.setAppResources(resourceType, values);
        }

        public void setUserInfo(HashSet<ISAnalyticsMetaData> metaDataList)
        {
            if (metaDataList != null)
            {
                _platformAgent.setUserInfo(metaDataList);
            }
        }

        public void setUserPrivacy(ISAnalyticsPrivacyRestriction toUpdate, bool restricted, ISAnalyticsReason why)
        {
            _platformAgent.setUserPrivacy(toUpdate, restricted, why);
        }

        public void setIAPSettings(ISAnalyticsPurchasingType purchasingType, params string[] values)
        {
            if (values != null)
            {
                _platformAgent.setIAPSettings(purchasingType, values);
            }
        }

        public void updateUserResources(ISAnalyticsResourceUpdate appResource)
        {
            if (appResource != null)
            {
                Dictionary<string, object> appResourceDic = new Dictionary<string, object>();
                appResourceDic.Add(PLACEMENT_KEY, appResource.getPlacement());
                appResourceDic.Add(CURRENCY_KEY, appResource.getCurrency());
                appResourceDic.Add(ACTION_KEY, appResource.getAction().ToString());
                appResourceDic.Add(USER_ACTION_KEY, appResource.getUserAction());
                appResourceDic.Add(AMOUNT_KEY, appResource.getAmount());
                appResourceDic.Add(BALANCE_KEY, appResource.getBalance());

                var parsedAppResource = parseDicToJson(appResourceDic);
                _platformAgent.updateUserResources(parsedAppResource);
            }
        }

        public void updateProgress(ISAnalyticsUserProgress userProgress)
        {
            if (userProgress != null)
            {
                Dictionary<string, object> userProgressDic = new Dictionary<string, object>();
                userProgressDic.Add(LEVEL1_KEY, userProgress.getLevel1());
                userProgressDic.Add(LEVEL2_KEY, userProgress.getLevel2());
                userProgressDic.Add(LEVEL3_KEY, userProgress.getLevel3());
                userProgressDic.Add(ATTEMPT_KEY, userProgress.getAttempt());
                userProgressDic.Add(SCORE_KEY, userProgress.getScore());
                userProgressDic.Add(STATE_KEY, userProgress.getState().ToString());

                string parsedUserProgress = parseDicToJson(userProgressDic);
                _platformAgent.updateProgress(parsedUserProgress);
            }
        }

        public void updateUserPurchase(ISAnalyticsInAppPurchase appPurchase)
        {
            if (appPurchase != null)
            {
                Dictionary<string, object> appPurchaseDic = new Dictionary<string, object>();
                appPurchaseDic.Add(CURRENCY_KEY, appPurchase.getCurrency());
                appPurchaseDic.Add(CATEGORY_KEY, appPurchase.getCategory());
                appPurchaseDic.Add(ITEM_KEY, appPurchase.getItem());
                appPurchaseDic.Add(PAID_KEY, appPurchase.getPaidAmount());
                appPurchaseDic.Add(PLACEMENT_KEY, appPurchase.getPlacement());

                string parsedAppPurchase = parseDicToJson(appPurchaseDic);
                _platformAgent.updateUserPurchase(parsedAppPurchase);
            }
        }

        public void updateImpressionData(ISAnalyticsMediationName from, string json)
        {
            _platformAgent.updateImpressionData(from, json);
        }

        public void updateCustomActivity(ISAnalyticsUserActivity userActivity)
        {
            if (userActivity != null)
            {
                Dictionary<string, object> userActivityDic = new Dictionary<string, object>();
                userActivityDic.Add(ACTIVITY_NAME_KEY, userActivity.getName());
                userActivityDic.Add(ACTIVITY_PROPERTY_KEY, userActivity.getProperty());

                string parsedUserActivity = parseDicToJson(userActivityDic);
                _platformAgent.updateCustomActivity(parsedUserActivity);
            }
        }

        public string getSDKVersion()
        {
            return _platformAgent.getSDKVersion();
        }

        private string parseDicToJson(Dictionary<string, object> dic)
        {
            string json = Json.Serialize(dic);
            return json;
        }
    }
}