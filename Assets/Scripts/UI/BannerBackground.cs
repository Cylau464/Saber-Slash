using apps;
using UnityEngine;
using UnityEngine.UI;

namespace Main.UI
{
    [DefaultExecutionOrder(-100)]
    public class BannerBackground : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private void OnEnable()
        {
            ADSManager.OnBannerSwitched += Switch;
        }

        private void OnDisable()
        {
            ADSManager.OnBannerSwitched -= Switch;
        }

        private void Switch(bool enable)
        {
            _image.enabled = enable;
        }
    }
}