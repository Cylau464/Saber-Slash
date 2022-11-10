using System;
using apps.exception;

namespace apps.ad
{
    [Serializable]
    public class IronSourceInfo
    {
        public string androidKey = "Entry Android Key";
        public string iosKey = "Entry IOS Key";

        public IronSourceBannerPosition bannerPosition = IronSourceBannerPosition.BOTTOM;
    }

    public class IronSourceADS : IADS
    {
        private readonly IronSourceInfo m_Info;
        private string m_LastPlacement;


        private bool _useBanner;
        public bool useBanner
        {
            get { return _useBanner; }
            set { _useBanner = value; }
        }

        private bool _useInterstitial;
        public bool useInterstitial
        {
            get { return _useInterstitial; }
            set
            {
                if (value == true)
                    LoadInterstitial();

                _useInterstitial = value;
            }
        }

        private bool _useRewardsVideo;
        public bool useRewardedVideo
        {
            get { return _useRewardsVideo; }
            set { _useRewardsVideo = value; }
        }

        protected Action _onCompletedRewardedVideo = null;
        protected Action _onClosedRewardedVideo = null;
        protected Action _onClosedInterstitial = null;

        public IronSourceADS(IronSourceInfo info, bool useBanner, bool useInterstitial, bool useRewardedVideo)
        {
            m_Info = info ?? throw new NullReferenceException("IronSourceInfo has a null value!...");

#if UNITY_EDITOR
            string key = "Unsupported platfrom";
#elif UNITY_ANDROID
            string key = m_Info.androidKey;
#elif UNITY_IOS
            string key = m_Info.iosKey;
#endif
            if (key == null || key.CompareTo("") == 0)
                throw new ArgumentEmptryOrNullException();


            this.useBanner = useBanner;
            this.useInterstitial = useInterstitial;
            this.useRewardedVideo = useRewardedVideo;

            Intialize(key);
        }

        private void Intialize(string key)
        {
            IronSource.Agent.validateIntegration();
            IronSource.Agent.init(key);

            SubscribeEvents();

            LoadBanner();

            LoadInterstitial();
        }

        private void SubscribeEvents()
        {
            IronSourceEvents.onImpressionDataReadyEvent += ImpressionDataAdReadyEventIronSourceAnalytics;

            IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
            IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
            IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
            IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

            IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
            IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
            IronSourceEvents.onRewardedVideoAdRewardedEvent += OnRewardedVideoAdRewardedEvent;
            IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;
            IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        }

#region call back
        private void ImpressionDataAdReadyEventIronSourceAnalytics(IronSourceImpressionData impressionData)
        {
            EventsLogger.ADRevenueEvent(impressionData);
        }

        private void InterstitialAdOpenedEvent()
        {
            EventsLogger.AdEvent(EventADSName.video_ads_started, AdType.interstitial, m_LastPlacement, EventADSResult.start);
        }

        private void InterstitialAdShowFailedEvent(IronSourceError error)
        {
            LoadInterstitial();
            EventsLogger.AdEvent(EventADSName.video_ads_started, AdType.interstitial, m_LastPlacement, EventADSResult.fail);
        }

        private void InterstitialAdClickedEvent()
        {
            EventsLogger.AdEvent(EventADSName.video_ads_available, AdType.interstitial, m_LastPlacement, EventADSResult.clicked);
        }

        private void InterstitialAdClosedEvent()
        {
            _onClosedInterstitial?.Invoke();
            LoadInterstitial();
            EventsLogger.AdEvent(EventADSName.video_ads_watch, AdType.interstitial, m_LastPlacement, EventADSResult.watched);
        }

        private void RewardedVideoAdOpenedEvent()
        {
            EventsLogger.AdEvent(EventADSName.video_ads_started, AdType.rewarded, m_LastPlacement, EventADSResult.start);
        }

        private void RewardedVideoAdShowFailedEvent(IronSourceError obj)
        {
            LoadRewardedVideo();
            EventsLogger.AdEvent(EventADSName.video_ads_started, AdType.rewarded, m_LastPlacement, EventADSResult.fail);
        }

        private void RewardedVideoAdClosedEvent()
        {
            LoadRewardedVideo();
        }

        private void OnRewardedVideoAdRewardedEvent(IronSourcePlacement placement)
        {
            _onCompletedRewardedVideo?.Invoke();
            EventsLogger.AdEvent(EventADSName.video_ads_watch, AdType.rewarded, m_LastPlacement, EventADSResult.watched);
        }
        private void RewardedVideoAdClickedEvent(IronSourcePlacement obj)
        {
            EventsLogger.AdEvent(EventADSName.video_ads_watch, AdType.rewarded, m_LastPlacement, EventADSResult.clicked);
        }
#endregion

        public void LoadBanner()
        {
            if (!useBanner)
                return;

            IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, m_Info.bannerPosition);
        }

        public void DisplayBanner()
        {
            if (!useBanner) return;

            IronSource.Agent.displayBanner();
        }

        public void HideBanner()
        {
            IronSource.Agent.hideBanner();
        }

        public void DestroyBanner()
        {
            IronSource.Agent.destroyBanner();
        }

        public void LoadInterstitial()
        {
            if (!useInterstitial) return;

            IronSource.Agent.loadInterstitial();
        }

        public bool IsInterstitialAvailable()
        {
            if (useInterstitial && !IronSource.Agent.isInterstitialReady()) LoadInterstitial();

            return useInterstitial && IronSource.Agent.isInterstitialReady();
        }

        public bool ShowInterstitial(string placementName = null, Action onClose = null)
        {
            if (!IsInterstitialAvailable())
                return false;

            _onClosedInterstitial = onClose;
            m_LastPlacement = placementName;
            IronSource.Agent.showInterstitial();
            return true;
        }

        public void LoadRewardedVideo() { }

        public bool IsRewardedideoAvailable()
        {
            return useRewardedVideo && IronSource.Agent.isRewardedVideoAvailable();
        }

        public bool ShowRewardedVideo(string placementName = null, Action onCompleted = null, Action onClose = null)
        {
            if (!IsRewardedideoAvailable())
                return false;

            m_LastPlacement = placementName;
            _onCompletedRewardedVideo = onCompleted;
            _onClosedRewardedVideo = onClose;
            IronSource.Agent.showRewardedVideo();
            return true;
        }
    }
}