using Engine.DI;
using UnityEngine;

namespace Main.Level
{
    [System.Serializable]
    public class LevelGroupsManager : IDependency
    {
        [SerializeField] private LevelsContainer m_LevelsContainer;

        public void Inject()
        {
            DIContainer.RegisterAsSingle<ILevelsGroup>(m_LevelsContainer.levelsGroup[0]);
        }
    }
}