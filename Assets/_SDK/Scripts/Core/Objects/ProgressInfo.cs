using apps.KPIs;
using System.Collections.Generic;

namespace apps
{
    public class ProgressStartInfo
    {
        public int playerLevel;
        public string levelName;
        public int levelCount;
        public string difficulty;
        public int levelLoop;
        public bool isRandom;
        public string levelType;
        public string gameMode;

        public ProgressStartInfo(int playerLevel, string levelName, int levelCount, string difficulty, int levelLoop, bool isRandom, string levelType, string gameMode)
        {
            this.playerLevel = playerLevel;
            this.levelName = levelName;
            this.levelCount = levelCount;
            this.difficulty = difficulty;
            this.levelLoop = levelLoop;
            this.isRandom = isRandom;
            this.levelType = levelType;
            this.gameMode = gameMode;
        }

        public virtual Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> keys = new Dictionary<string, object>();

            Dictionary<string, object> momenet = new Dictionary<string, object>();
            momenet.Add(playerLevel.ToString(), PlayTimeInfo.TimeRange);

            keys.Add("level_number", momenet);
            keys.Add("level_name", levelName);
            keys.Add("level_count", levelCount);
            keys.Add("level_diff", difficulty);
            keys.Add("level_loop", levelLoop);
            keys.Add("level_random", (isRandom) ? 1 : 0);
            keys.Add("level_type", levelType);
            keys.Add("game_mode", gameMode);

            return keys;
        }
    }

    public class ProgressFailedInfo : ProgressStartInfo
    {
        public string time;
        public string reason;
        public int progress;
        public int continueValue;

        public ProgressFailedInfo(int playerLevel, string levelName, int levelCount, string difficulty, int levelLoop, bool isRandom, string levelType, string gameMode,
            string time, string reason, int progress, int continueValue)
            : base(playerLevel, levelName, levelCount, difficulty, levelLoop, isRandom, levelType, gameMode)
        {
            this.time = time;
            this.reason = reason;
            this.progress = progress;
            this.continueValue = continueValue;
        }

        public override Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> keys = new Dictionary<string, object>();

            Dictionary<string, object> momenet = new Dictionary<string, object>();
            momenet.Add(playerLevel.ToString(), PlayTimeInfo.TimeRange);

            keys.Add("level_number", momenet);
            keys.Add("level_name", levelName);
            keys.Add("level_count", levelCount);
            keys.Add("level_diff", difficulty);
            keys.Add("level_loop", levelLoop);
            keys.Add("level_random", (isRandom) ? 1 : 0);
            keys.Add("level_type", levelType);
            keys.Add("game_mode", gameMode);
            keys.Add("time", time);
            keys.Add("reason", reason);
            keys.Add("result", "lose");
            keys.Add("progress", progress);
            keys.Add("continue", continueValue);

            return keys;
        }
    }

    public class ProgressCompletedInfo : ProgressStartInfo
    {
        public string time;
        public int progress;
        public int continueValue;

        public ProgressCompletedInfo(int playerLevel, string levelName, int levelCount, string difficulty, int levelLoop, bool isRandom, string levelType, string gameMode,
            string time, int progress, int continueValue)
            : base(playerLevel, levelName, levelCount, difficulty, levelLoop, isRandom, levelType, gameMode)
        {
            this.time = time;
            this.progress = progress;
            this.continueValue = continueValue;
        }

        public override Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> keys = new Dictionary<string, object>();

            Dictionary<string, object> momenet = new Dictionary<string, object>();
            momenet.Add(playerLevel.ToString(), PlayTimeInfo.TimeRange);

            keys.Add("level_number", momenet);
            keys.Add("level_name", levelName);
            keys.Add("level_count", levelCount);
            keys.Add("level_diff", difficulty);
            keys.Add("level_loop", levelLoop);
            keys.Add("level_random", (isRandom) ? 1 : 0);
            keys.Add("level_type", levelType);
            keys.Add("game_mode", gameMode);
            keys.Add("time", time);
            keys.Add("result", "win");
            keys.Add("progress", progress);
            keys.Add("continue", continueValue);

            return keys;
        }
    }
}