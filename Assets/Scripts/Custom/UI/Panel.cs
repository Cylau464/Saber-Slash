using UnityEngine;

namespace Main.UI
{
    public abstract class Panel : MonoBehaviour, IPanel
    {
        public bool isVisible { get; protected set; }

        public virtual void Show()
        {
            isVisible = true;
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            isVisible = false;
            gameObject.SetActive(false);
        }
    }
}