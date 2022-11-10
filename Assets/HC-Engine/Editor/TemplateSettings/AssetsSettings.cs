using Engine.Attribute;
using UnityEngine;

namespace editor
{
    [TemplateSettings(k_FilePath, k_FileName)]
    public class AssetsSettings : ScriptableObject
    {
        internal const string k_FilePath = "Assets/Settings/Editor/";
        internal const string k_FileName = "AssetsSettings";

        [Header("Build Settings")]
        public bool resetData = false;
        public bool validate = false;
        public bool saveAssets = false;
    }
}
