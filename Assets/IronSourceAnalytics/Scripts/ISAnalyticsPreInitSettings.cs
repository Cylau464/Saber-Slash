using System;
using System.Collections.Generic;
using UnityEngine;

namespace IronSourceAnalyticsSDK
{
    public class ISAnalyticsPreInitSettings : ScriptableObject
    {

        [SerializeField]
        public string appKeyAndroid = "";

        [SerializeField]
        public string appKeyIOS = "";

        [SerializeField]
        public List<string> appResourceCurrencies = new List<string>();
        [SerializeField]
        public List<string> appResourcePlacements = new List<string>();
        [SerializeField]
        public List<string> appResourceUserActions = new List<string>();
        [SerializeField]
        public List<string> purchasedItems = new List<string>();
        [SerializeField]
        public List<string> purchasedPlacements = new List<string>();
        [SerializeField]
        public List<string> purchasedItemCategories = new List<string>();

        public Texture2D ironSourceLogo;
        public Texture2D ironSourceKCLogo;
        public Texture2D ironSourcePlatformLogo;
        public Texture2D deleteIcon;
        public Texture2D infoIcon;

        public bool isCurrenciesFoldOut = false;
        public bool isPlacementsFoldOut = false;
        public bool isUserActionsFoldOut = false;

        public bool isPurchasedItemsFoldOut = false;
        public bool isPurchasedPlacementsFoldOut = false;
        public bool isPurchasedItemCategoriesFoldOut = false;

    }

}
