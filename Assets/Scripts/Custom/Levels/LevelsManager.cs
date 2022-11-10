using apps;
using Engine.DI;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Main.Level
{
    public partial class LevelsManager : MonoBehaviour, ILevelsManager, IDependency
    {
        private event Action<ILevel> m_LevelLoadedEvets;

        [SerializeField] private Transform _levelsContents;
        
        private ILevelsData m_LevelData;
        private ILevelsGroup m_LevelContainer;

        private static int _currentInterstitialDelay = -1;
        private static int _interstitialDelay = 2;

        public int totalLevels
        {
            get 
            {
                if (m_LevelContainer == null) m_LevelContainer = DIContainer.AsSingle<ILevelsGroup>() ?? throw new NullReferenceException();
                return m_LevelContainer.totalLevels;
            }
        }

        public Level level { get; private set; }

        public LevelInfoSO levelInfo
        {
            get
            {
                if (isLevelLoaded) return level.levelInfo;

                return null;
            }
        }

        public bool isLevelLoaded { get; private set; } = false;

        [Inject] private Level.Factory _levelFactory;

        public void Inject()
        {
            DIContainer.RegisterAsSingle<ILevelsManager>(this);
        }

        private void OnEnable()
        {
            m_LevelContainer = DIContainer.AsSingle<ILevelsGroup>() ?? throw new NullReferenceException();
            m_LevelData = DIContainer.AsSingle<ILevelsData>() ?? throw new NullReferenceException();

            InitializeLevel();
        }

        private void InitializeLevel()
        {
            if (m_LevelData.isTestingMode)
            {
                Level level = FindObjectOfType<Level>();
                if (level != null)
                {
                    DefineCurrentLevel(level);
                    return;
                }

                MakeInstantiate();
                return;
            }

            MakeInstantiate();

            if (++_currentInterstitialDelay >= _interstitialDelay)
            {
                ADSManager.ShowInterstitial("level_start");
                _currentInterstitialDelay = 0;
            }
        }

        private void MakeInstantiate()
        {
            Level level = m_LevelContainer.GetLevelPrefab(m_LevelData.idLevel) ?? throw new NullReferenceException("Level has a null value!...");
            level = _levelFactory.Create(level);
            level.transform.SetParent(_levelsContents, false);
            DefineCurrentLevel(level);
        }

        private void DefineCurrentLevel(Level level)
        {
            this.level = level ?? throw new ArgumentNullException();

            this.level.Initialize(this);

            if (m_LevelLoadedEvets != null) m_LevelLoadedEvets.Invoke(level);

            isLevelLoaded = true;
        }

        public void SubscribeLevelLoaded(Action<ILevel> action)
        {
            if (action != null && !action.Equals(null)) throw new ArgumentNullException();

            if (isLevelLoaded)
            {
                action.Invoke(level);
                return;
            }

            if (!m_LevelLoadedEvets.GetInvocationList().Contains(action))
            {
                m_LevelLoadedEvets += action;
            }
        }
    }
}
