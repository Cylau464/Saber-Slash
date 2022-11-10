using Engine;
using Engine.Data;
using Engine.Random;
using UnityEngine;

namespace Main.Level
{
    [System.Serializable]
    public class LevelsData : ILevelsData, IAwake, Engine.DI.IDependency
    {
        [SerializeField] private LevelsSettings m_LevelsSettings;

        private FieldKey<int> m_IdLevel;
        private FieldKey<int> m_PlayerLevel;
        private FieldKey<int> m_RandomLevels;

        private bool m_IsTestingMode = false;
        private int m_IndexTestLevel = 0;
        private int m_StartRandomLevel = 0;

        public int playerLevel => m_PlayerLevel.value;
        public bool isTestingMode => m_IsTestingMode;
        public int indexTestLevel => m_IndexTestLevel;
        public int idLevel
        {
            get
            {
                if (m_IsTestingMode) return m_IndexTestLevel;

                return m_IdLevel.value;
            }
        }

        public bool isRandom => m_RandomLevels.value == 1;


        public void Inject()
        {
            Engine.DI.DIContainer.RegisterAsSingle<ILevelsData>(this);
        }

        public void Awake()
        {
            m_IdLevel = m_LevelsSettings.idLevel;
            m_PlayerLevel = m_LevelsSettings.playerLevel;
            m_RandomLevels = m_LevelsSettings.randomLevels;

            m_IsTestingMode = m_LevelsSettings.isTestingMode;
            m_IndexTestLevel = m_LevelsSettings.indexTestLevel;
            m_StartRandomLevel = m_LevelsSettings.startRandomLevel;
        }

        /// <summary>
        /// This function execute when the anylevel completed .
        /// </summary>
        public void LevelCompleted()
        {
            int totalLevels = Engine.DI.DIContainer.AsSingle<ILevelsGroup>().totalLevels;
            if (m_RandomLevels.value == 1)
            {
                m_IdLevel.value = GetRandomLevelID(m_IdLevel.value, totalLevels);
            }
            else
            {
                if (totalLevels - 1 <= m_IdLevel.value)
                {
                    m_RandomLevels.value = 1;
                    m_IdLevel.value = GetRandomLevelID(m_IdLevel.value, totalLevels);
                }
                else
                {
                    m_IdLevel.value++;
                }
            }
            m_PlayerLevel.value++;
        }

        /// <summary>
        /// Get random level with not repeated the last id level.
        /// </summary>
        private int GetRandomLevelID(int lastIdLevel, int totalLevels)
        {
            if (1 < totalLevels)
            {
                RandomFieldInfo[] fieldInfos;

                if (lastIdLevel <= m_StartRandomLevel)
                {
                    fieldInfos = new RandomFieldInfo[1];
                    fieldInfos[0] = new RandomFieldInfo(m_StartRandomLevel + 1, totalLevels);
                }
                else
                if (totalLevels - 1 <= lastIdLevel)
                {
                    fieldInfos = new RandomFieldInfo[1];
                    fieldInfos[0] = new RandomFieldInfo(m_StartRandomLevel, totalLevels - 1);
                }
                else
                {
                    fieldInfos = new RandomFieldInfo[2];
                    fieldInfos[0] = new RandomFieldInfo(m_StartRandomLevel, lastIdLevel - 1);
                    fieldInfos[1] = new RandomFieldInfo(lastIdLevel + 1, totalLevels);
                }

                return IntelligentRandom.GetRandomWithField(fieldInfos);
            }
            else
                return 0;
        }
    }
}