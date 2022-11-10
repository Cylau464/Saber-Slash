namespace IronSourceAnalyticsSDK
{
    public class ISAnalyticsResourceUpdate
    {
        private string resourceCurrency;
        private string resourcePlacement;
        private string resourceUserAction;
        private ISAnalyticsResourceAction resourceAction;
        private int? resourceAmount;
        private int? resourceBalance;

        public ISAnalyticsResourceUpdate(string currency)
        {
            resourceCurrency = currency;
        }

        public ISAnalyticsResourceUpdate placement(string name)
        {
            resourcePlacement = name;
            return this;
        }

        public ISAnalyticsResourceUpdate userAction(string name)
        {
            resourceUserAction = name;
            return this;
        }

        public ISAnalyticsResourceUpdate balance(int amount)
        {
            resourceBalance = amount;
            return this;
        }

        public ISAnalyticsResourceUpdate gained(int amount)
        {
            resourceAction = ISAnalyticsResourceAction.GAINED;
            resourceAmount = amount;
            return this;
        }

        public ISAnalyticsResourceUpdate consumed(int amount)
        {
            resourceAction = ISAnalyticsResourceAction.CONSUMED;
            resourceAmount = amount;
            return this;
        }

        internal int? getBalance()
        {
            return resourceBalance;
        }

        internal string getUserAction()
        {
            return resourceUserAction;
        }

        internal int? getAmount()
        {
            return resourceAmount;
        }

        internal string getCurrency()
        {
            return resourceCurrency;
        }

        internal string getPlacement()
        {
            return resourcePlacement;
        }

        internal ISAnalyticsResourceAction getAction()
        {
            return resourceAction;
        }
    }
}