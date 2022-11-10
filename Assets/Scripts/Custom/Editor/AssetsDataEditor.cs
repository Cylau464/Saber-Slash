using System;
using editor;
using Engine.Attribute;
using Engine.Senser;
using Engine.Store;
using Engine.Money;
using UnityEngine;
using Main.Level;
using Engine;
using Skins;

namespace Main.Editor
{
    [TemplateSettings(k_FilePath, k_FileName)]
    public class AssetsDataEditor : ScriptableObject, IResetData, IUnlock
    {
        internal const string k_FilePath = "Assets/Settings/Editor/";
        internal const string k_FileName = "AssetsData";

        [SerializeField] protected StoreInfo[] m_StoreInfos;
        [SerializeField] protected SenserInfo[] m_SenserInfos;
        [SerializeField] protected MoneyInfo[] m_MoneyInfos;
        [SerializeField] protected LevelsSettings[] m_LevelsData;
        [SerializeField] protected SkinPreset[] m_SkinsData;

        public virtual void ResetData(int idData)
        {
            m_StoreInfos = SearchAndInvoke<StoreInfo>((info, i) => info.ResetStoreData($"Store{i}"));
            m_SenserInfos = SearchAndInvoke<SenserInfo>((info, i) => info.ResetSenserData($"Senser{i}"));
            m_MoneyInfos = SearchAndInvoke<MoneyInfo>((info, i) => info.ResetMoneyData($"Money{i}"));
            m_LevelsData = SearchAndInvoke<LevelsSettings>((info, i) => info.ReseLevelsData($"LevelsData{i}"));
            m_SkinsData = SearchAndInvoke<SkinPreset>((info, i) => info.ResetSkinsData($"SkinsData{i}"));
        }

        public virtual void Unlock()
        {
            m_StoreInfos = SearchAndInvoke<StoreInfo>((info, i) => info.UnlockStoreData($"Store{i}"));
            m_MoneyInfos = SearchAndInvoke<MoneyInfo>((info, i) => info.UnlockMoneyData($"Money{i}"));
            m_SkinsData = SearchAndInvoke<SkinPreset>((info, i) => info.UnlockSkinsData($"SkinsData{i}"));
        }

        public static T[] SearchAndInvoke<T>(Action<T, int> actions) where T : ScriptableObject
        {
            T[] ts = AssetUtility.FindScribtableObjectsOfType<T>();

            for (int i = 0; i < ts.Length; i++)
            {
                if (ts[i] == null || ts[i].Equals(null)) continue;
                actions.Invoke(ts[i], i);
            }

            ts.SaveObjectsOfType();
            return ts;
        }
    }
}
