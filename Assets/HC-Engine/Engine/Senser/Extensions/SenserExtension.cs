namespace Engine.Senser
{
    public static class SenserExtension
    {
        public static void ResetSenserData(this SenserInfo senserInfo, string fileName)
        {
            senserInfo.isEnable = new Data.FieldKey<int>("isEnable", fileName, 1);
        }
    }
}