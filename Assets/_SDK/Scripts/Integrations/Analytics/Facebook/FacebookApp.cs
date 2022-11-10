using Facebook.Unity;

namespace apps.analytics
{
    public class FacebookApp
    {
        private static bool s_IsInited = false;

        private static int s_IsTrackingEnable = -1;
        public static bool isTrackingEnable
        {
            get => s_IsTrackingEnable == 1;
            set
            {
                if (s_IsInited)
                {
#if UNITY_IOS
                    s_IsTrackingEnable = (value) ? 1 : 0;
                    FB.Mobile.SetAdvertiserIDCollectionEnabled(value);
                    FB.Mobile.SetAdvertiserTrackingEnabled(value);
#else
                    FB.Mobile.SetAdvertiserIDCollectionEnabled(true);
#endif
                }
            }
        }

        public FacebookApp()
        {
            if (FB.IsInitialized == true)
            {
                CallEvents();
            }
            else
            {
                FB.Init(() =>
                {
                    CallEvents();
                });
            }
        }

        public void CallEvents()
        {
            FB.ActivateApp();
            FB.LogAppEvent(AppEventName.ActivatedApp);
#if UNITY_IOS
            if (s_IsTrackingEnable != -1)
            {
                FB.Mobile.SetAdvertiserIDCollectionEnabled(isTrackingEnable);
                FB.Mobile.SetAdvertiserTrackingEnabled(isTrackingEnable);
            }
#else
            FB.Mobile.SetAdvertiserIDCollectionEnabled(true);
#endif
            s_IsInited = true;
        }
    }
}