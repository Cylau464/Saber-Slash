namespace IronSourceAnalyticsSDK
{
    public class ISAnalyticsInAppPurchase
    {
        private string purchasedItem;
        private string purchasedCategory;
        private float? purchasedPaidAmount;
        private string purchasedCurrency;
        private string placement;

        public ISAnalyticsInAppPurchase(string item)
        {
            purchasedItem = item;
        }

        public ISAnalyticsInAppPurchase fromCategory(string name)
        {
            purchasedCategory = name;
            return this;
        }

        public ISAnalyticsInAppPurchase paid(float amount)
        {
            purchasedPaidAmount = amount;
            return this;
        }

        public ISAnalyticsInAppPurchase currency(string name)
        {
            purchasedCurrency = name;
            return this;
        }

        public ISAnalyticsInAppPurchase purchasedPlacement(string name)
        {
            placement = name;
            return this;
        }

        internal string getItem()
        {
            return purchasedItem;
        }

        internal string getCategory()
        {
            return purchasedCategory;
        }

        internal float? getPaidAmount()
        {
            return purchasedPaidAmount;
        }

        internal string getCurrency()
        {
            return purchasedCurrency;
        }

        internal string getPlacement()
        {
            return placement;
        }
    }
}