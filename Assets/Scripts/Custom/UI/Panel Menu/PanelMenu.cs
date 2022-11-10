using UnityEngine;

namespace Main.UI
{
    public class PanelMenu : Panel
    {
        [SerializeField] protected MainCanvasManager m_MainCanvasManager;
        [SerializeField] protected LevelsProgress m_LevelsProgress;

        private Level.ILevelsData m_LevelsData;

        public void StartGame()
        {
            m_MainCanvasManager.StartGame();
        }

        public override void Show()
        {
            isVisible = true;

            if (m_LevelsData == null) m_LevelsData = DefineLevelsData();

            m_LevelsProgress.Initialize(m_LevelsData);
        }

        private Level.ILevelsData DefineLevelsData()
        {
            return Engine.DI.DIContainer.AsSingle<Level.ILevelsData>() ?? throw new System.NullReferenceException();
        }
    }
}
