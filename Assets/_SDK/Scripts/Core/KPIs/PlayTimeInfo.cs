using UnityEngine;

namespace apps.KPIs
{
    public static class PlayTimeInfo
    {
        private const string _dataKey = "Playtime";
        private const int _range = 60;

        private static float _playTime;
        private static float _playTimeData
        {
            get
            {
                if (PlayerPrefs.HasKey(_dataKey))
                    return PlayerPrefs.GetFloat(_dataKey);

                return 0;
            }
            set
            {
                PlayerPrefs.SetFloat(_dataKey, value);
            }
        }

        private static float _lastRefreshingTime;

        private static float _lastPlayTimeEventSent;


        public static float PlayTime => _playTime;
        public static string TimeRange
        {
            get
            {
                int index = Mathf.FloorToInt(_playTime / _range);
                return $"{index}min";
            }
        }

        public static void Initialize()
        {
            _playTime = _playTimeData;
        }

        public static void FramePassed(float deltaTime)
        {
            _playTime += deltaTime;

            if (_lastRefreshingTime + 10 <= _playTime)
            {
                _playTimeData = _lastRefreshingTime = _playTime;
            }
        }

        public static void SendNewAchievementEvent()
        {
            int time = Mathf.FloorToInt(_playTime / _range);
            if (time != _lastPlayTimeEventSent)
            {
                _lastPlayTimeEventSent = time;
                EventsLogger.CustomEvent($"KPI:PlayTime{TimeRange}", false);
            }
        }
    }
}