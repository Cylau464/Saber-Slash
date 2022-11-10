using System.Collections.Generic;

namespace Engine.UI
{
    public interface IGameCanvas
    {
        IPanel lastPanel { get; }

        void ShowPanel(IPanel panel);

        void HidePanel(IPanel panel);

        void HideAllPanels();
    }

    public abstract class GameCanvas : UnityEngine.MonoBehaviour, IGameCanvas
    {
        protected List<IPanel> m_ShowingPanels = new List<IPanel>();

        public IPanel lastPanel { get; private set; }

        public void ShowPanel(IPanel panel)
        {
            if (panel == null) throw new System.NullReferenceException();

            if (panel.isVisible) return;

            panel.Show();
            m_ShowingPanels.Add(panel);
        }

        public void HidePanel(IPanel panel)
        {
            if (panel == null) throw new System.NullReferenceException();

            if (!panel.isVisible) return;

            panel.Hide();
            m_ShowingPanels.Remove(panel);
        }

        public void SwitchPanel(IPanel switchToPanel)
        {
            if (switchToPanel == null) throw new System.NullReferenceException();

            if (lastPanel != null) HidePanel(lastPanel);
            lastPanel = switchToPanel;
            ShowPanel(lastPanel);
        }

        public void HideAllPanels()
        {
            m_ShowingPanels.ForEach(item => { if (item != null && !item.Equals(null)) item.Hide(); });
            m_ShowingPanels.Clear();
        }
    }
}