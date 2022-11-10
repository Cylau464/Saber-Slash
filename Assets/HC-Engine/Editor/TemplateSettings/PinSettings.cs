using Engine.Attribute;
using UnityEngine;

namespace editor.pin
{
    [TemplateSettings(k_FilePath, k_FileName)]
    public partial class PinSettings : ScriptableObject
    {
        internal const string k_FilePath = "Assets/Settings/Editor/";
        internal const string k_FileName = "PinSettings";

        [SerializeField] internal Vector2 m_ButtonScale = new Vector2(80, 20);
        [SerializeField] internal PinInfo[] m_Pins;


        internal static PinSettings GetPinSettings()
        {
            return AssetUtility.GetOrCreateAsset<PinSettings>(k_FilePath, k_FileName + ".asset");
        }

        internal void OnPinInfoUpdated(PinInfo[] pinInfo)
        {
            m_Pins = pinInfo;
        }

        private void OnValidate()
        {
            PinListInfo.UpdatePinsInfo(m_Pins);
        }
    }
}