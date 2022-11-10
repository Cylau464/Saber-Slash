using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace IronSourceAnalyticsSDK.Editor
{
    [CustomEditor(typeof(ISAnalyticsPreInitSettings))]
    public class IronSourceAnalyticsSettingsInspector : UnityEditor.Editor
    {
        ISAnalyticsPreInitSettings preInitSettings;
        SerializedObject serializedPreInitSettingsObject;

        private GUIContent appKeyIOSLabel = new GUIContent("iOS app key", "Your ironSource application key for Android.");
        private GUIContent appKeyAndroidLabel = new GUIContent("Android app key", "Your ironSource application key for iOS.");

        private const string setupTabName = "Setup";
        private const string iapTabName = "IAP Settings";
        private const string resourcesTabName = "Resources Settings";

        private const string setupTabText = "To initialize the App Analytics SDK, enter your app key as it shown on the ironSource dashboard.";
        private const string iapTabText = "Use the fields below to update your in-app purchase settings. You can update any of the settings that were configured before the SDK was initialized.";
        private const string resourcesTabText = "Use the fields below to update the settings for your in-app resources.";

        private readonly string[] tabs = { setupTabName, iapTabName, resourcesTabName };

        private int currentTab = 0;
        private string currentTabText = setupTabText;
        private const int setupTabIndex = 0;
        private const int iapTabIndex = 1;
        private const int resourcesTabIndex = 2;

        private const string kcURL = "https://developers.is.com/ironsource-mobile/android/app-analytics-sdk-unity/";
        private const string platformURL = "https://platform.ironsrc.com/";

        private const string iconsPath = "Assets/IronSourceAnalytics/Icons/";
        private const string ironSourceLogoFileName = "iS64.png";
        private const string ironSourcePlatformIconFileName = "platform_64.png";
        private const string ironSourceKCIconFileName = "info_64.png";
        private const string deleteIconFileName = "delete_32.png";
        private const string helpIconFileName = "Help_36.png";

        private const string purchasedPlacementTooltip = "Places in your app where users can purchase items";
        private const string purchasedItemTooltip = "Items users can purchase in your app";
        private const string purchasedItemCatgoryTooltip = "Groups of items users can purchase in your app";
        private const string appResourcePlacementsTooltip = "Places in your app where users can earn or spend currency";
        private const string appResourceUserActionsTooltip = "Actions users take to earn or spend currency";
        private const string appResourceCurrenciesTooltip = "Currencies available to users in your app";

        private const int maxItems = 100;

        private const int defaultSpacing = 8;
        private const int iconWidth = 18;
        private const int iconHeight = 18;
        private const string listStartSpacing = "   ";
        private const string addButtonText = "Add";
        private const int iSLogoHeight = 32;
        private const int iSLogoWidth = 32;
        private const int iSDocsLogoWidth = 26;
        private const int iSDocsLogoHeight = 26;
        private const int iSPlatformLogoWidth = 30;
        private const int iSPlatformLogoHeight = 28;

        private const int listBeginHorizontalSpacing = 20;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            GUILayout.BeginVertical();
            GUILayout.Space(defaultSpacing);

            DrawISAnalyticsHeader();

            GUILayout.Space(defaultSpacing);
            currentTab = SelectTab(currentTab);
            drawSelectedTab(currentTab);
            GUILayout.EndVertical();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(preInitSettings);
            }
        }

        private int SelectTab(int tabToSelect)
        {
            EditorGUI.BeginChangeCheck();
            int selectedTab = GUILayout.Toolbar(tabToSelect, tabs);
            if (EditorGUI.EndChangeCheck())
            {
                serializedPreInitSettingsObject.ApplyModifiedProperties();
                GUI.FocusControl(null);
            }

            return selectedTab;
        }

        private void drawCurrenciesList()
        {
            GUILayout.BeginHorizontal();
            preInitSettings.isCurrenciesFoldOut = EditorGUILayout.Foldout(preInitSettings.isCurrenciesFoldOut, new GUIContent(listStartSpacing + "Currencies (" + preInitSettings.appResourceCurrencies.Count + " / " + maxItems + " values)", string.Empty));
            GUILayout.Label(new GUIContent { image = preInitSettings.infoIcon, tooltip = appResourceCurrenciesTooltip }, new GUILayoutOption[] {
                GUILayout.Width(iconWidth),
                GUILayout.Height(iconHeight)
            });

            GUILayout.EndHorizontal();
            GUILayout.Space(defaultSpacing);

            if (preInitSettings.isCurrenciesFoldOut)
            {
                List<int> removeCurrencies = new List<int>();

                for (int i = 0; i < preInitSettings.appResourceCurrencies.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(listBeginHorizontalSpacing);

                    GUILayout.Label("Currency " + (i + 1), GUILayout.Width(110));

                    preInitSettings.appResourceCurrencies[i] = ISAnalyticsValidator.validAnalyticsString(EditorGUILayout.TextField(preInitSettings.appResourceCurrencies[i]), "App Currency");

                    GUILayout.Space(2);

                    if (GUILayout.Button(preInitSettings.deleteIcon, GUI.skin.label, new GUILayoutOption[] {
                                GUILayout.Width(iconWidth),
                                GUILayout.Height(iconHeight)
                            }))
                    {
                        removeCurrencies.Add(i);
                    }

                    EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.Link);
                    GUILayout.EndHorizontal();
                    GUILayout.Space(2);
                }

                foreach (int i in removeCurrencies)
                {
                    preInitSettings.appResourceCurrencies.RemoveAt(i);
                }

                GUILayout.BeginHorizontal();
                GUILayout.Space(listBeginHorizontalSpacing);

                if (GUILayout.Button(addButtonText, GUILayout.Width(50)))
                {
                    if (preInitSettings.appResourceCurrencies.Count < maxItems)
                    {
                        preInitSettings.appResourceCurrencies.Add(IronSourceAnalyticsConstants.defaultValue);
                    }
                }

                GUILayout.EndHorizontal();
            }
        }

        private void drawPlacementsList()
        {
            GUILayout.BeginHorizontal();
            preInitSettings.isPlacementsFoldOut = EditorGUILayout.Foldout(preInitSettings.isPlacementsFoldOut, new GUIContent(listStartSpacing + "Placements (" + preInitSettings.appResourcePlacements.Count + " / " + maxItems + " values)", string.Empty));
            GUILayout.Label(new GUIContent { image = preInitSettings.infoIcon, tooltip = appResourcePlacementsTooltip }, new GUILayoutOption[] {
                GUILayout.Width(iconWidth),
                GUILayout.Height(iconHeight)
            });

            GUILayout.EndHorizontal();
            GUILayout.Space(defaultSpacing);

            if (preInitSettings.isPlacementsFoldOut)
            {
                List<int> removePlacements = new List<int>();

                for (int i = 0; i < preInitSettings.appResourcePlacements.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(listBeginHorizontalSpacing);

                    GUILayout.Label("Placement " + (i + 1), GUILayout.Width(110));

                    preInitSettings.appResourcePlacements[i] = ISAnalyticsValidator.validAnalyticsString(EditorGUILayout.TextField(preInitSettings.appResourcePlacements[i]), "App Placement");

                    GUILayout.Space(2);

                    if (GUILayout.Button(preInitSettings.deleteIcon, GUI.skin.label, new GUILayoutOption[] {
                                GUILayout.Width(iconWidth),
                                GUILayout.Height(iconHeight)
                            }))
                    {
                        removePlacements.Add(i);
                    }

                    EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.Link);
                    GUILayout.EndHorizontal();
                    GUILayout.Space(2);
                }

                foreach (int i in removePlacements)
                {
                    preInitSettings.appResourcePlacements.RemoveAt(i);
                }

                GUILayout.BeginHorizontal();
                GUILayout.Space(listBeginHorizontalSpacing);

                if (GUILayout.Button(addButtonText, GUILayout.Width(50)))
                {
                    if (preInitSettings.appResourcePlacements.Count < maxItems)
                    {
                        preInitSettings.appResourcePlacements.Add(IronSourceAnalyticsConstants.defaultValue);
                    }
                }

                GUILayout.EndHorizontal();
            }
        }

        private void drawUserActionsList()
        {
            GUILayout.BeginHorizontal();
            preInitSettings.isUserActionsFoldOut = EditorGUILayout.Foldout(preInitSettings.isUserActionsFoldOut, new GUIContent(listStartSpacing + "User actions (" + preInitSettings.appResourceUserActions.Count + " / " + maxItems + " values)", string.Empty));
            GUILayout.Label(new GUIContent { image = preInitSettings.infoIcon, tooltip = appResourceUserActionsTooltip }, new GUILayoutOption[] {
                GUILayout.Width(iconWidth),
                GUILayout.Height(iconHeight)
            });

            GUILayout.EndHorizontal();
            GUILayout.Space(defaultSpacing);

            if (preInitSettings.isUserActionsFoldOut)
            {
                List<int> removeUserActions = new List<int>();

                for (int i = 0; i < preInitSettings.appResourceUserActions.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(listBeginHorizontalSpacing);

                    GUILayout.Label("Action " + (i + 1), GUILayout.Width(110));

                    preInitSettings.appResourceUserActions[i] = ISAnalyticsValidator.validAnalyticsString(EditorGUILayout.TextField(preInitSettings.appResourceUserActions[i]), "User Action");

                    GUILayout.Space(2);

                    if (GUILayout.Button(preInitSettings.deleteIcon, GUI.skin.label, new GUILayoutOption[] {
                                GUILayout.Width(iconWidth),
                                GUILayout.Height(iconHeight)
                            }))
                    {
                        removeUserActions.Add(i);
                    }

                    EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.Link);
                    GUILayout.EndHorizontal();
                    GUILayout.Space(2);
                }

                foreach (int i in removeUserActions)
                {
                    preInitSettings.appResourceUserActions.RemoveAt(i);
                }

                GUILayout.BeginHorizontal();
                GUILayout.Space(listBeginHorizontalSpacing);

                if (GUILayout.Button(addButtonText, GUILayout.Width(50)))
                {
                    if (preInitSettings.appResourceUserActions.Count < maxItems)
                    {
                        preInitSettings.appResourceUserActions.Add(IronSourceAnalyticsConstants.defaultValue);
                    }
                }

                GUILayout.EndHorizontal();
            }
        }

        private void drawPurchasedItemsList()
        {
            GUILayout.BeginHorizontal();
            preInitSettings.isPurchasedItemsFoldOut = EditorGUILayout.Foldout(preInitSettings.isPurchasedItemsFoldOut, new GUIContent(listStartSpacing + "Items for purchase (" + preInitSettings.purchasedItems.Count + " / " + maxItems + " values)", string.Empty));
            GUILayout.Label(new GUIContent { image = preInitSettings.infoIcon, tooltip = purchasedItemTooltip }, new GUILayoutOption[] {
                GUILayout.Width(iconWidth),
                GUILayout.Height(iconHeight)
            });

            GUILayout.EndHorizontal();
            GUILayout.Space(defaultSpacing);

            if (preInitSettings.isPurchasedItemsFoldOut)
            {
                List<int> removePurchasedItems = new List<int>();

                for (int i = 0; i < preInitSettings.purchasedItems.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(listBeginHorizontalSpacing);

                    GUILayout.Label("Item " + (i + 1), GUILayout.Width(110));
                    preInitSettings.purchasedItems[i] = ISAnalyticsValidator.validAnalyticsString(EditorGUILayout.TextField(preInitSettings.purchasedItems[i]), "Purchase Item");

                    GUILayout.Space(2);

                    if (GUILayout.Button(preInitSettings.deleteIcon, GUI.skin.label, new GUILayoutOption[] {
                                GUILayout.Width(iconWidth),
                                GUILayout.Height(iconHeight)
                            }))
                    {
                        removePurchasedItems.Add(i);
                    }

                    EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.Link);
                    GUILayout.EndHorizontal();
                    GUILayout.Space(2);
                }

                foreach (int i in removePurchasedItems)
                {
                    preInitSettings.purchasedItems.RemoveAt(i);
                }

                GUILayout.BeginHorizontal();
                GUILayout.Space(listBeginHorizontalSpacing);

                if (GUILayout.Button(addButtonText, GUILayout.Width(50)))
                {
                    if (preInitSettings.purchasedItems.Count < maxItems)
                    {
                        preInitSettings.purchasedItems.Add(IronSourceAnalyticsConstants.defaultValue);
                    }
                }

                GUILayout.EndHorizontal();
            }
        }

        private void drawItemCategoriesList()
        {
            GUILayout.BeginHorizontal();
            preInitSettings.isPurchasedItemCategoriesFoldOut = EditorGUILayout.Foldout(preInitSettings.isPurchasedItemCategoriesFoldOut, new GUIContent(listStartSpacing + "Categories (" + preInitSettings.purchasedItemCategories.Count + " / " + maxItems + " values)", string.Empty));
            GUILayout.Label(new GUIContent { image = preInitSettings.infoIcon, tooltip = purchasedItemCatgoryTooltip }, new GUILayoutOption[] {
                GUILayout.Width(iconWidth),
                GUILayout.Height(iconHeight)
            });

            GUILayout.EndHorizontal();
            GUILayout.Space(defaultSpacing);

            if (preInitSettings.isPurchasedItemCategoriesFoldOut)
            {
                List<int> removeItemCategories = new List<int>();

                for (int i = 0; i < preInitSettings.purchasedItemCategories.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(listBeginHorizontalSpacing);

                    GUILayout.Label("Category " + (i + 1), GUILayout.Width(110));

                    preInitSettings.purchasedItemCategories[i] = ISAnalyticsValidator.validAnalyticsString(EditorGUILayout.TextField(preInitSettings.purchasedItemCategories[i]), "Item Category");

                    GUILayout.Space(2);

                    if (GUILayout.Button(preInitSettings.deleteIcon, GUI.skin.label, new GUILayoutOption[] {
                                GUILayout.Width(iconWidth),
                                GUILayout.Height(iconHeight)
                            }))
                    {
                        removeItemCategories.Add(i);
                    }

                    EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.Link);
                    GUILayout.EndHorizontal();
                    GUILayout.Space(2);
                }

                foreach (int i in removeItemCategories)
                {
                    preInitSettings.purchasedItemCategories.RemoveAt(i);
                }

                GUILayout.BeginHorizontal();
                GUILayout.Space(listBeginHorizontalSpacing);

                if (GUILayout.Button(addButtonText, GUILayout.Width(50)))
                {
                    if (preInitSettings.purchasedItemCategories.Count < maxItems)
                    {
                        preInitSettings.purchasedItemCategories.Add(IronSourceAnalyticsConstants.defaultValue);
                    }
                }

                GUILayout.EndHorizontal();
            }
        }

        private void drawItemPlacementsList()
        {
            GUILayout.BeginHorizontal();
            preInitSettings.isPurchasedPlacementsFoldOut = EditorGUILayout.Foldout(preInitSettings.isPurchasedPlacementsFoldOut, new GUIContent(listStartSpacing + "Placements (" + preInitSettings.purchasedPlacements.Count + " / " + maxItems + " values)", string.Empty));
            GUILayout.Label(new GUIContent
            {
                image = preInitSettings.infoIcon,
                tooltip = purchasedPlacementTooltip
            }, new GUILayoutOption[] {
                GUILayout.Width(iconWidth),
                GUILayout.Height(iconHeight),
            });

            GUILayout.EndHorizontal();
            GUILayout.Space(defaultSpacing);

            if (preInitSettings.isPurchasedPlacementsFoldOut)
            {
                List<int> removePlacements = new List<int>();

                for (int i = 0; i < preInitSettings.purchasedPlacements.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(listBeginHorizontalSpacing);

                    GUILayout.Label("Placement " + (i + 1), GUILayout.Width(110));

                    preInitSettings.purchasedPlacements[i] = ISAnalyticsValidator.validAnalyticsString(EditorGUILayout.TextField(preInitSettings.purchasedPlacements[i]), "Purchase Placement");
                    GUILayout.Space(2);

                    if (GUILayout.Button(preInitSettings.deleteIcon, GUI.skin.label, new GUILayoutOption[] {
                                GUILayout.Width(iconWidth),
                                GUILayout.Height(iconHeight)
                            }))
                    {
                        removePlacements.Add(i);
                    }

                    EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.Link);
                    GUILayout.EndHorizontal();
                    GUILayout.Space(2);
                }

                foreach (int i in removePlacements)
                {
                    preInitSettings.purchasedPlacements.RemoveAt(i);
                }

                GUILayout.BeginHorizontal();
                GUILayout.Space(listBeginHorizontalSpacing);

                if (GUILayout.Button(addButtonText, GUILayout.Width(50)))
                {
                    if (preInitSettings.purchasedPlacements.Count < maxItems)
                    {
                        preInitSettings.purchasedPlacements.Add(IronSourceAnalyticsConstants.defaultValue);
                    }
                }
                GUILayout.EndHorizontal();
            }
        }


        private void drawSelectedTab(int selectedTab)
        {
            EditorGUI.BeginChangeCheck();
            switch (selectedTab)
            {
                case setupTabIndex:
                    drawSetupTab();
                    break;

                case iapTabIndex:
                    drawIAPTab();
                    break;

                case resourcesTabIndex:
                    drawResourcesTab();
                    break;

            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void drawTabText()
        {
            GUILayout.Space(defaultSpacing);
            GUILayout.BeginHorizontal();
            GUIStyle textStyle = EditorStyles.label;
            textStyle.wordWrap = true;
            GUILayout.Label(currentTabText, textStyle);
            GUILayout.Space(defaultSpacing);
            GUILayout.EndHorizontal();
            GUILayout.Space(defaultSpacing);
            drawSplitLine();
            GUILayout.Space(defaultSpacing);
        }

        private void drawIAPTab()
        {
            currentTabText = iapTabText;
            drawTabText();
            drawPurchasedItemsList();
            GUILayout.Space(defaultSpacing);
            drawSplitLine();
            GUILayout.Space(defaultSpacing);
            drawItemCategoriesList();
            GUILayout.Space(defaultSpacing);
            drawSplitLine();
            GUILayout.Space(defaultSpacing);
            drawItemPlacementsList();
        }

        private void drawResourcesTab()
        {
            currentTabText = resourcesTabText;
            drawTabText();
            drawCurrenciesList();
            GUILayout.Space(defaultSpacing);
            drawSplitLine();
            GUILayout.Space(defaultSpacing);
            drawPlacementsList();
            drawSplitLine();
            GUILayout.Space(defaultSpacing);
            drawUserActionsList();
            GUILayout.Space(defaultSpacing);
        }

        private void drawSetupTab()
        {
            currentTabText = setupTabText;
            drawTabText();
            GUILayout.BeginHorizontal();
            GUILayout.Label(appKeyIOSLabel, GUILayout.Width(220));
            var previousIOSAppKey = preInitSettings.appKeyIOS;
            preInitSettings.appKeyIOS = EditorGUILayout.TextField(string.Empty, preInitSettings.appKeyIOS);
            GUILayout.EndHorizontal();
            GUILayout.Space(defaultSpacing * 2);
            GUILayout.BeginHorizontal();
            GUILayout.Label(appKeyAndroidLabel, GUILayout.Width(220));
            var previousAndroidAppKey = preInitSettings.appKeyAndroid;
            preInitSettings.appKeyAndroid = EditorGUILayout.TextField(string.Empty, preInitSettings.appKeyAndroid);
            GUILayout.EndHorizontal();
        }

        public static void drawSplitLine()
        {
            GUIStyle splitter = new GUIStyle();
            splitter.normal.background = EditorGUIUtility.whiteTexture;
            splitter.stretchWidth = true;
            splitter.margin = new RectOffset(0, 0, 7, 7);

            Rect position = GUILayoutUtility.GetRect(GUIContent.none, splitter, GUILayout.Height(1));

            if (Event.current.type == EventType.Repaint)
            {
                Color restoreColor = GUI.color;
                GUI.color = new Color(0.5f, 0.5f, 0.5f);
                splitter.Draw(position, false, false, false, false);
                GUI.color = restoreColor;
            }
        }

        private void DrawISAnalyticsHeader()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(preInitSettings.ironSourceLogo, new GUILayoutOption[] {
                GUILayout.Width(iSLogoHeight),
                GUILayout.Height(iSLogoWidth)
            });
            GUILayout.BeginVertical();
            GUILayout.Space(defaultSpacing);
            GUILayout.BeginHorizontal();
            var title = "App Analytics SDK v" + IronSourceAnalytics.getPluginVersion();

            GUILayout.Label(title, EditorStyles.largeLabel);
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            if (GUILayout.Button(new GUIContent { image = preInitSettings.ironSourcePlatformLogo, tooltip = "Platform Login" }, GUI.skin.label, GUILayout.Width(iSPlatformLogoWidth), GUILayout.Height(iSPlatformLogoHeight)))
            {
                Application.OpenURL(platformURL);
            }

            EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.Link);

            GUILayout.Space(defaultSpacing);

            if (GUILayout.Button(new GUIContent { image = preInitSettings.ironSourceKCLogo, tooltip = "Documentation" }, GUI.skin.label, GUILayout.Width(iSDocsLogoWidth), GUILayout.Height(iSDocsLogoHeight)))
            {
                Application.OpenURL(kcURL);
            }

            EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.Link);

            GUILayout.Space(defaultSpacing);

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            drawSplitLine();

            GUILayout.EndVertical();


        }

        private void OnEnable()
        {
            preInitSettings = target as ISAnalyticsPreInitSettings;
            serializedPreInitSettingsObject = new SerializedObject(preInitSettings);

            if (preInitSettings.ironSourceLogo == null)
            {
                preInitSettings.ironSourceLogo = (Texture2D)AssetDatabase.LoadAssetAtPath(iconsPath + ironSourceLogoFileName, typeof(Texture2D));
            }

            if (preInitSettings.ironSourcePlatformLogo == null)
            {
                preInitSettings.ironSourcePlatformLogo = (Texture2D)AssetDatabase.LoadAssetAtPath(iconsPath + ironSourcePlatformIconFileName, typeof(Texture2D));
            }

            if (preInitSettings.ironSourceKCLogo == null)
            {
                preInitSettings.ironSourceKCLogo = (Texture2D)AssetDatabase.LoadAssetAtPath(iconsPath + ironSourceKCIconFileName, typeof(Texture2D));
            }

            if (preInitSettings.deleteIcon == null)
            {
                preInitSettings.deleteIcon = (Texture2D)AssetDatabase.LoadAssetAtPath(iconsPath + deleteIconFileName, typeof(Texture2D));
            }

            if (preInitSettings.infoIcon == null)
            {
                preInitSettings.infoIcon = (Texture2D)AssetDatabase.LoadAssetAtPath(iconsPath + helpIconFileName, typeof(Texture2D));
            }

        }
    }
}
