using System;
using UnityEngine;

namespace apps.KPIs
{
    public static class RetentionInfo
    {
        private const string _keyName = "Login";


        private static DateTime _firstLogin;
        private static int _retentionDay;

        public static int RetentionDay
        {
            get
            {
                return _retentionDay;
            }
        } 

        public static void Initialize()
        {
            Login();

            DefineRetention();
        }

        private static void Login()
        {
            if (!PlayerPrefs.HasKey(_keyName))
            {
                _firstLogin = DateTime.UtcNow;
                PlayerPrefs.SetString(_keyName, _firstLogin.ToString());
                return;
            }

            if (!DateTime.TryParse(PlayerPrefs.GetString(_keyName), out _firstLogin))
            {
                Debug.LogError("The login is not available to Parse Datetime!...");
                _firstLogin = DateTime.UtcNow;
                PlayerPrefs.SetString(_keyName, _firstLogin.ToString());
            }
        }

        private static void DefineRetention()
        {
            TimeSpan span = DateTime.UtcNow.Subtract(_firstLogin);
            _retentionDay = Mathf.FloorToInt((int)span.TotalDays);
        }

        public static void SendLoginEvent()
        {
            EventsLogger.CustomEvent($"KPI:Retention{RetentionDay}", false);
        }
    }
}