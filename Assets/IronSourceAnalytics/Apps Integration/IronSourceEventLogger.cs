#if UNITY_ANDROID
using GameAnalyticsSDK;
using IronSourceAnalyticsSDK;
using System;
using System.Collections.Generic;
using UnityEngine;
#endif

namespace apps.ad
{
    public class IronSourceEventLogger : IEvent
    {
        public IronSourceEventLogger(ADSInfo info)
        {
#if UNITY_ANDROID
            string appUserId = SystemInfo.deviceUniqueIdentifier;
            ISAnalytics.setAppUserId(appUserId);

            DateTime firstLogin = GetFirstLogin();

            HashSet<ISAnalyticsMetaData> metaDataSet = new HashSet<ISAnalyticsMetaData>
            {
                new ISAnalyticsMetaData(ISAnalyticsMetaDataKey.ACHIEVEMENT, "success"),
                new ISAnalyticsMetaData(ISAnalyticsMetaDataKey.FIRST_LOGIN, firstLogin),
                new ISAnalyticsMetaData(ISAnalyticsMetaDataKey.CREATION_DATE, firstLogin),
                new ISAnalyticsMetaData(ISAnalyticsMetaDataKey.IAP_USER, true),
                new ISAnalyticsMetaData(ISAnalyticsMetaDataKey.IS_SUBSCRIBED, true)
            };
            ISAnalytics.setUserInfo(metaDataSet);

            ISAnalytics.setUserPrivacy(ISAnalyticsPrivacyRestriction.RESTRICTED_DATA, true, ISAnalyticsReason.COPPA);

            ISAnalytics.init(info.ironSourceInfo.androidKey);
#endif
        }

#if UNITY_ANDROID
        private DateTime GetFirstLogin()
        {
            if (PlayerPrefs.HasKey("FIRST_LOGIN"))
            {
                try
                {
                    return new DateTime(long.Parse(PlayerPrefs.GetString("FIRST_LOGIN").ToString()));
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error Exception, Message: {e.Message}");
                }
            }

            PlayerPrefs.SetString("FIRST_LOGIN", DateTime.Now.Ticks.ToString());
            return DateTime.Now;
        }
#endif
        public void CustomEvent(string eventName) { }

        public void SessionEvent(string sessionName, SessionStatue statue) { }

        public void IAPEvent(string productIAPID, float price){}

        public void ProgressStartedEvent(ProgressStartInfo progressInfo)
        {
#if UNITY_ANDROID
            ISAnalyticsUserProgress progress_start = new ISAnalyticsUserProgress($"level_{progressInfo.playerLevel}").state(ISAnalyticsProgressState.BEGIN).attempt(1);
            ISAnalytics.updateProgress(progress_start);
#endif
        }

        public void ProgressFailedEvent(ProgressFailedInfo progressInfo)
        {
#if UNITY_ANDROID
            ISAnalyticsUserProgress userProgress_fail = new ISAnalyticsUserProgress($"level_{progressInfo.playerLevel}").state(ISAnalyticsProgressState.FAILED).score(0).attempt(1);
            ISAnalytics.updateProgress(userProgress_fail);
#endif
        }

        public void ProgressCompletedEvent(ProgressCompletedInfo progressInfo)
        {
#if UNITY_ANDROID
            ISAnalyticsUserProgress userProgress_score = new ISAnalyticsUserProgress($"level_{progressInfo.playerLevel}").state(ISAnalyticsProgressState.COMPLETE).score(progressInfo.progress).attempt(1);
            ISAnalytics.updateProgress(userProgress_score);
#endif
        }

        public void ErrorEvent(ErrorSeverity severity, string message) { }

        public void IAPEvent(InAppInfo info)
        {
#if UNITY_ANDROID
            ISAnalyticsInAppPurchase purchase = new ISAnalyticsInAppPurchase(info.InAppID)
            .paid((float)info.Price)
            .currency(info.Currency);
            
            ISAnalytics.updateUserPurchase(purchase);
#endif
        }

        /// <summary>
        /// To send the revenue of the ads.
        /// </summary>
        /// <param name="mediation"> The mediation name ex: Ironsource, Applovin... </param>
        /// <param name="impressionData"> The data of the impression </param>
        public void ADRevenueEvent(object impressionData)
        {
#if UNITY_ANDROID
            try
            {
                IronSourceImpressionData data = (IronSourceImpressionData)impressionData;
                ISAnalytics.updateImpressionData(ISAnalyticsMediationName.IRONSOURCE, data.allData);
            }
            catch (Exception e)
            {
                GameAnalytics.NewErrorEvent(GAErrorSeverity.Error, e.Message);
            }
#endif
        }

        public void AdEvent(EventADSName eventADSName, AdType adType, string placement, EventADSResult result) { }
    }
}
