using apps.KPIs;
using GameAnalyticsSDK;
using System;
using System.Collections.Generic;

namespace apps.analytics
{
    public class GameAnalyticsEvents : IEvent
    {
        public GameAnalyticsEvents()
        {
            GameAnalytics.Initialize();
        }

        public void CustomEvent(string eventName)
        {
            GameAnalytics.NewDesignEvent($"{eventName}");
        }

        public void SessionEvent(string sessionName, SessionStatue statue)
        {
            GameAnalytics.NewDesignEvent($"{sessionName}:{statue}");
        }

        public void IAPEvent(string productIAPID, float price)
        {
            GameAnalytics.NewDesignEvent($"Revenue:IAP:{productIAPID}:{price}:{PlayTimeInfo.TimeRange}");
        }

        public void ProgressStartedEvent(ProgressStartInfo progressInfo)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"Level_{progressInfo.playerLevel}", $"{PlayTimeInfo.TimeRange}");
        }

        public void ProgressFailedEvent(ProgressFailedInfo progressInfo)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, $"Level_{progressInfo.playerLevel}", $"{PlayTimeInfo.TimeRange}");
            CustomEvent($"LevelDesign:ReasonFail:Level_{progressInfo.reason}");
        }

        public void ProgressCompletedEvent(ProgressCompletedInfo progressInfo)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Level_{progressInfo.playerLevel}", $"{PlayTimeInfo.TimeRange}");
        }

        public void ErrorEvent(ErrorSeverity severity, string message)
        {
            GameAnalytics.NewErrorEvent(ConvertToGAErrorSeverity(severity), message);
        }

        public void IAPEvent(InAppInfo info)
        {
            GameAnalytics.NewDesignEvent($"IAPEvent:{info.InAppID}:{PlayTimeInfo.TimeRange}");
        }

        public void AdEvent(EventADSName eventADSName, AdType adType, string placement, EventADSResult result)
        {
            switch (result)
            {
                case EventADSResult.start:
                    GameAnalytics.NewAdEvent(GAAdAction.Show, ConvertToGAAdType(adType), "standard", placement);
                    GameAnalytics.NewDesignEvent($"ADS:{placement}:Started:{PlayTimeInfo.TimeRange}");
                    break;
                case EventADSResult.fail:
                    GameAnalytics.NewAdEvent(GAAdAction.FailedShow, ConvertToGAAdType(adType), "standard", placement);
                    break;
                case EventADSResult.not_available:
                    GameAnalytics.NewAdEvent(GAAdAction.Request, ConvertToGAAdType(adType), "standard", placement);
                    break;
                case EventADSResult.clicked:
                    GameAnalytics.NewAdEvent(GAAdAction.Clicked, ConvertToGAAdType(adType), "standard", placement);
                    break;
                case EventADSResult.watched:
                    GameAnalytics.NewAdEvent(GAAdAction.RewardReceived, ConvertToGAAdType(adType), "standard", placement);
                    GameAnalytics.NewDesignEvent($"ADS:{placement}:Watched");
                    break;
            }
        }

        private GAAdType ConvertToGAAdType(AdType adType)
        {
            switch (adType)
            {
                case AdType.interstitial:
                    return GAAdType.Interstitial;
                case AdType.rewarded:
                    return GAAdType.RewardedVideo;
                default:
                    return GAAdType.Undefined;
            }
        }

        private GAErrorSeverity ConvertToGAErrorSeverity(ErrorSeverity severity)
        {
            switch (severity)
            {
                case ErrorSeverity.Error:
                    return GAErrorSeverity.Error;

                case ErrorSeverity.Warning:
                    return GAErrorSeverity.Warning;

                case ErrorSeverity.Info:
                    return GAErrorSeverity.Info;

                case ErrorSeverity.Critical:
                    return GAErrorSeverity.Critical;

                case ErrorSeverity.Debug:
                    return GAErrorSeverity.Debug;

                default:
                    return GAErrorSeverity.Undefined;
            }
        }

        /// <summary>
        /// To send the revenue of the ads.
        /// </summary>
        /// <param name="mediation"> The mediation name ex: Ironsource, Applovin... </param>
        /// <param name="impressionData"> The data of the impression </param>
        public void ADRevenueEvent(object impressionData)
        {
            try
            {
                IronSourceImpressionData data = (IronSourceImpressionData)impressionData;
                GameAnalytics.NewDesignEvent($"Revenue:Ad:{data.placement}:{data.revenue}:{PlayTimeInfo.TimeRange}");
            }
            catch (Exception e)
            {
                GameAnalytics.NewErrorEvent(GAErrorSeverity.Error, e.Message);
            }
        }
    }
}