namespace Main.Level
{
    public static class LevelsDataExtension
    {
        public static void ReseLevelsData(this LevelsSettings levelsSettings, string fileName)
        {
            levelsSettings.idLevel = new Engine.Data.FieldKey<int>("id", fileName, 0);
            levelsSettings.playerLevel = new Engine.Data.FieldKey<int>("player", fileName, 1);
            levelsSettings.randomLevels = new Engine.Data.FieldKey<int>("random", fileName, 0);
        }
    }
}