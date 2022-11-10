namespace IronSourceAnalyticsSDK
{
    public sealed class ISAnalyticsMetaDataKey
    {
        internal static readonly ISAnalyticsMetaDataKey GENDER = new ISAnalyticsMetaDataKey("GENDER");
        internal static readonly ISAnalyticsMetaDataKey LOGIN_TYPE = new ISAnalyticsMetaDataKey("LOGIN_TYPE");

        public static readonly ISAnalyticsMetaDataKey AGE = new ISAnalyticsMetaDataKey("AGE");
        public static readonly ISAnalyticsMetaDataKey FIRST_LOGIN = new ISAnalyticsMetaDataKey("FIRST_LOGIN");
        public static readonly ISAnalyticsMetaDataKey CREATION_DATE = new ISAnalyticsMetaDataKey("CREATION_DATE");
        public static readonly ISAnalyticsMetaDataKey IS_SUBSCRIBED = new ISAnalyticsMetaDataKey("IS_SUBSCRIBED");
        public static readonly ISAnalyticsMetaDataKey IAP_USER = new ISAnalyticsMetaDataKey("IAP_USER");
        public static readonly ISAnalyticsMetaDataKey ACHIEVEMENT = new ISAnalyticsMetaDataKey("ACHIEVEMENT");

        private string metaDataKey;

        private ISAnalyticsMetaDataKey(string key)
        {
            metaDataKey = key;
        }

        public string getDescription()
        {
            return metaDataKey;
        }
    }
}