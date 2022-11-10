using Engine.Data;
using UnityEngine;

namespace Engine.Money
{
    [CreateAssetMenu(fileName = "New Money Info", menuName = "Add/Money Info", order = 3)]
    public class MoneyInfo : ScriptableObject, IAsset
    {
        [Tooltip("Currect data saving values.")]
        public FieldKey<int> totalCoins;

        [Tooltip("Initialize total coins on start the game first time.")]
        public int initCoins = 0;
    }
}