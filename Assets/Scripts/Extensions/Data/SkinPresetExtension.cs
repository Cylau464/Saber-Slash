namespace Skins
{
    public static class SkinPresetExtension
    {
        public static void ResetSkinsData(this SkinPreset data, string fileName)
        {
            data.Data = new Engine.Data.FieldKey<Data>(data.InitData.Key, fileName, data.InitData.value);
        }

        public static void UnlockSkinsData(this SkinPreset data, string fileName)
        {
            Data unlockedData = new Data(true, 100);
            data.Data = new Engine.Data.FieldKey<Data>(data.Data.Key, fileName, unlockedData);
        }
    }
}
