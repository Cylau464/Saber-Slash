using Engine;
using UnityEngine;

namespace Skins
{
    [CreateAssetMenu(fileName = "Skins Container", menuName = "Add/Skins/Skins Container")]
    public class SkinsContainer : ScriptableObject, IAsset
    {
        public SkinPreset[] SkinPresets;
    }
}