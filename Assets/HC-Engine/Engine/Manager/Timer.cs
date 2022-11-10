using UnityEngine;

namespace Engine
{
    public static class Timer
    {
        public static float defaultScale { get; private set; }
        public static float StartLevelTime { get; private set; }
        public static float LevelTime => Time.time - StartLevelTime;

        internal static void Initialize()
        {
            StartLevelTime = Time.time;
            defaultScale = Time.timeScale = 1;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        public static void PauseGame()
        {
            SetTimeScale(0);
        }

        public static void PauseSlow()
        {
            SetTimeScale(0.0001f);
        }

        public static void SetTimeScale(float scale)
        {
            Time.timeScale = scale;
            Time.fixedDeltaTime = 0.02F * scale;
        }

        public static void ContinueGame()
        {
            Time.timeScale = defaultScale;
        }
    }
}