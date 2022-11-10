using System;
using UnityEngine;

namespace apps
{
    public enum TrackingState { Undefined, Unknown, Restricted, Denied, Authorized }
    public class TenjinManager : IApplicationPause, IEvent
    {
        private static BaseTenjin Instance;
        private string _key;

        public TenjinManager(string key)
        {
            _key = key;
            TenjinConnect();
        }

        public void TenjinConnect()
        {
            Instance = Tenjin.getInstance(_key);

#if UNITY_ANDROID || UNITY_EDITOR
            Instance.SetAppStoreType(AppStoreType.googleplay);
            MakeConnet();
#endif
        }

        public static void MakeConnet()
        {
            Instance.Connect();
        }

        void IApplicationPause.OnApplicationPause(bool pauseStatus)
        {
            if (!pauseStatus)
            {
                TenjinConnect();
            }
        }

        public void CustomEvent(string eventName)
        {
            //Instance.SendEvent(eventName.Replace(":", ""));
        }

        public void SessionEvent(string sessionName, SessionStatue statue)
        {
            //Instance.SendEvent(sessionName, statue.ToString());
        }

        public void ProgressStartedEvent(ProgressStartInfo progressInfo)
        {
            //Instance.SendEvent($"LevelStarte{progressInfo.playerLevel}");
        }

        public void ProgressFailedEvent(ProgressFailedInfo progressInfo)
        {
            //Instance.SendEvent($"LevelFailed{progressInfo.playerLevel}");
        }

        public void ProgressCompletedEvent(ProgressCompletedInfo progressInfo)
        {
            //Instance.SendEvent($"LevelConmpleted{progressInfo.playerLevel}");
        }

        public void ErrorEvent(ErrorSeverity severity, string message)
        {
            //Instance.SendEvent($"Error{severity}, {message}");
        }

        public void IAPEvent(InAppInfo info)
        {
#if UNITY_ANDROID
            Instance.Transaction(info.InAppID, info.Currency, 1, info.Price, null, info.Receipt, info.SignatureAndTransactionID);
#elif UNITY_IOS
            Instance.Transaction(info.InAppID, info.Currency, 1, info.Price, info.SignatureAndTransactionID, info.Receipt, null);
#endif
        }

        public void AdEvent(EventADSName eventADSName, AdType adType, string placement, EventADSResult result)
        {
            //if (result == EventADSResult.start)
            //{
            //    Instance.SendEvent($"{eventADSName}{adType}{placement}");
            //}
        }


        /// <summary>
        /// To send the revenue of the ads.
        /// </summary>
        /// <param name="mediation"> The mediation name ex: Ironsource, Applovin... </param>
        /// <param name="impressionData"> The data of the impression </param>
        public void ADRevenueEvent(object impressionData) { }
    }
}