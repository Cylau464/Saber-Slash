using UnityEngine;

namespace Main.Level
{
    [System.Serializable]
    public struct LevelsGroup : ILevelsGroup
    {
        [Header("Levels")]
        [SerializeField] private Level[] _levels;

        public int totalLevels => _levels.Length;

        public Level GetLevelPrefab(int idLevel)
        {
            if ((uint)_levels.Length <= (uint)idLevel) throw new System.ArgumentOutOfRangeException();
            return _levels[idLevel];
        }
    }

    [CreateAssetMenu(fileName = "New Levels Container", menuName = "Add/More/Levels Container", order = 10)]
    public class LevelsContainer : ScriptableObject
    {
        public LevelsGroup[] levelsGroup;
    }
}
