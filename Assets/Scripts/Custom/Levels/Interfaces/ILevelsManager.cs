using System;

namespace Main.Level
{
    public interface ILevelsManager
    {
        public bool isLevelLoaded { get; }
        public int totalLevels { get; }
        public Level level { get; }
        public LevelInfoSO levelInfo { get; }

        public void SubscribeLevelLoaded(Action<ILevel> action);
    }
}
