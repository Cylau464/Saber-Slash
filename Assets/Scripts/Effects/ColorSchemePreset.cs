using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "ColorSchemePreset", menuName = "Add/Color Scheme Preset")]
    public class ColorSchemePreset : ScriptableObject
    {
        [ColorUsage(true, true)] public Color FogColor;
        public Color LightColor;
        public Material Skybox;
    }
}