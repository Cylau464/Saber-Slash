namespace Engine.Money
{
    public static class MoneyExtension
    {

        public static void ResetMoneyData(this MoneyInfo moneyInfo, string fileName)
        {
            moneyInfo.totalCoins = new Data.FieldKey<int>("totalMoney", fileName, moneyInfo.initCoins);
        }

        public static void UnlockMoneyData(this MoneyInfo moneyInfo, string fileName)
        {
            moneyInfo.totalCoins = new Data.FieldKey<int>("totalMoney", fileName, 1000000000);
        }
    }
}
