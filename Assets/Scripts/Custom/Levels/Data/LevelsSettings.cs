using Engine;
using Engine.Data;
using Engine.Random;
using UnityEngine;
using Engine.Attribute;

namespace Main.Level
{
    [TemplateSettings("Assets/ScriptableObjects/", "Levels Data")]
    [CreateAssetMenu(fileName = "New Levels Data", menuName = "Add/More/Levels Data", order = 7)]
    public class LevelsSettings : ScriptableObject, IAsset
    {
        [Header("Data")]
        public FieldKey<int> idLevel;
        public FieldKey<int> playerLevel;
        public FieldKey<int> randomLevels;

        [Header("Settings")]
        public bool isTestingMode = false;
        public int indexTestLevel = 0;
        public int startRandomLevel = 0;
    }
}