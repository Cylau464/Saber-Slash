#if UNITY_IOS
using apps.analytics;
using System.Linq;
using System;
using static Balaso.AppTrackingTransparency;

namespace apps.ios
{
    public sealed class IOSInitialize
    {
        private static bool _isInitialized;
        public static bool IsInitialized => _isInitialized;

        private static event Action _onCompleted;
        public static event Action OnCompleted
        {
            add
            {
                if (!_onCompleted.GetInvocationList().Contains(value))
                    _onCompleted += value;
            }
            remove
            {
                if (_onCompleted.GetInvocationList().Contains(value))
                    _onCompleted -= value;
            }
        }

        public IOSInitialize(string terms, string privacy)
        {
            IGDPRWindow window = new GDPRWindow(terms, privacy);
            InitGDPR.Initialize(window, OnGDPRPassed);
        }

        private void OnGDPRPassed(GDPRStatus status)
        {
            if (status == GDPRStatus.Applies)
            {
                InitAppTrackingTransparency.Intiliaze(OnATTPassed);
            }
        }

        private void OnATTPassed(AuthorizationStatus status)
        {
            if (status == AuthorizationStatus.AUTHORIZED)
            {
                FacebookApp.isTrackingEnable = true;
                TenjinManager.MakeConnet();
            }
            UnityEngine.Debug.Log($"OnATTPassed: {status}");
            _isInitialized = true;
            _onCompleted?.Invoke();
        }
    }
}
#endif