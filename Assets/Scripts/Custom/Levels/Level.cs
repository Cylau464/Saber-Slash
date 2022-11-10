using UnityEngine;
using Zenject;

namespace Main.Level
{
    public class Level : MonoBehaviour, ILevel
    {
        [SerializeField] private LevelInfoSO _levelInfo;
        public LevelInfoSO levelInfo => _levelInfo;

        [Inject] private LevelRewardHandler _rewardHandler;
        public LevelRewardHandler RewardHandler => _rewardHandler;

        public ILevelsManager levelsManager { get; private set; }

        public class Factory : PrefabFactory<Level> { }

        public virtual void Initialize(ILevelsManager levelsManager)
        {
            this.levelsManager = levelsManager;
        }
    }
}
