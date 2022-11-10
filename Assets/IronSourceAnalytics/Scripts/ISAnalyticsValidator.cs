using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace IronSourceAnalyticsSDK
{
    public class ISAnalyticsValidator
    {
        private const int minAnalyticsStringLength = 1;
        private const int maxAnalyticsStringLength = 20;
        private const string analyticsStringPattern = "^[a-zA-Z0-9_\\s.]*$";

        public static string validAnalyticsString(string itemValue, string itemLogName)
        {
            if (itemValue.Length < minAnalyticsStringLength || itemValue.Length > maxAnalyticsStringLength)
            {
                Debug.LogError($"IronSourceAnalytics: {itemLogName} length should be between {minAnalyticsStringLength} to {maxAnalyticsStringLength} chars");
                return IronSourceAnalyticsConstants.defaultValue;
            }

            if (!validateMagicNumber(itemValue, analyticsStringPattern))
            {
                if (itemValue != null)
                {
                    Debug.LogError($"IronSourceAnalytics: {itemValue} is invalid, {itemLogName} should contain only a-Z, 0-9 spaces, underscores and dots chars");
                }
                return IronSourceAnalyticsConstants.defaultValue;
            }

            return itemValue;
        }

        private static bool validateMagicNumber(string value, string pattern)
        {
            if (value == null || pattern == null)
            {
                return false;
            }

            return Regex.IsMatch(value, pattern);
        }
    }
}
