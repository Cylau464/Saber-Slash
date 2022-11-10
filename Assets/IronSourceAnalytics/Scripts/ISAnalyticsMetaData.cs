using System.Collections.Generic;
using System;

namespace IronSourceAnalyticsSDK
{
    public class ISAnalyticsMetaData
    {
        internal KeyValuePair<string, object> metaDataPair = new KeyValuePair<string, object>();

        private static readonly HashSet<ISAnalyticsMetaDataKey> allowString = new HashSet<ISAnalyticsMetaDataKey> { ISAnalyticsMetaDataKey.ACHIEVEMENT };
        private static readonly HashSet<ISAnalyticsMetaDataKey> allowBool = new HashSet<ISAnalyticsMetaDataKey> { ISAnalyticsMetaDataKey.IS_SUBSCRIBED, ISAnalyticsMetaDataKey.IAP_USER };
        private static readonly HashSet<ISAnalyticsMetaDataKey> allowInt = new HashSet<ISAnalyticsMetaDataKey> { ISAnalyticsMetaDataKey.AGE };
        private static readonly HashSet<ISAnalyticsMetaDataKey> allowDate = new HashSet<ISAnalyticsMetaDataKey> { ISAnalyticsMetaDataKey.FIRST_LOGIN, ISAnalyticsMetaDataKey.CREATION_DATE };

        public enum GENDER
        {
            MALE,
            FEMALE,
            OTHER
        }

        public enum LOGIN_TYPE_VALUE
        {
            FACEBOOK,
            GOOGLE,
            EMAIL,
            APP,
            OTHER
        }

        public ISAnalyticsMetaData(LOGIN_TYPE_VALUE loginType)
        {
            metaDataPair = new KeyValuePair<string, object>(ISAnalyticsMetaDataKey.LOGIN_TYPE.getDescription(), loginType.ToString());
        }

        public ISAnalyticsMetaData(GENDER gender)
        {
            metaDataPair = new KeyValuePair<string, object>(ISAnalyticsMetaDataKey.GENDER.getDescription(), gender.ToString());
        }

        public ISAnalyticsMetaData(ISAnalyticsMetaDataKey key, bool value)
        {
            if (!allowBool.Contains(key))
            {
                return;
            }

            metaDataPair = new KeyValuePair<string, object>(key.getDescription(), value);
        }

        public ISAnalyticsMetaData(ISAnalyticsMetaDataKey key, int value)
        {
            if (!allowInt.Contains(key))
            {
                return;
            }

            metaDataPair = new KeyValuePair<string, object>(key.getDescription(), value);
        }

        public ISAnalyticsMetaData(ISAnalyticsMetaDataKey key, string value)
        {
            if (!allowString.Contains(key))
            {
                return;
            }

            metaDataPair = new KeyValuePair<string, object>(key.getDescription(), value);
        }

        public ISAnalyticsMetaData(ISAnalyticsMetaDataKey key, DateTime value)
        {
            if (!allowDate.Contains(key))
            {
                return;
            }


            metaDataPair = new KeyValuePair<string, object>(key.getDescription(), value.ToString("MM/dd/yyyy"));
        }

    }
}