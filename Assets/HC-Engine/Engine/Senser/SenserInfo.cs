using Engine.Data;
using UnityEngine;

namespace Engine.Senser
{
    [CreateAssetMenu(fileName = "New Senser", menuName = "Add/More/New Senser", order = 11)]
    public class SenserInfo : ScriptableObject, IAsset
    {
        public SenserType type;
        public FieldKey<int> isEnable;
    }
}
