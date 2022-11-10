using Engine.DI;
using Engine.Senser;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.UI
{
    public class SettingsView : MonoBehaviour, IPanel
    {
        [SerializeField] private GameObject _window;

        [Header("Animators")]
        [SerializeField] private SettingsElement _audioElement;
        [SerializeField] private SettingsElement _vibrationElement;

        [Header("Buttons")]
        [SerializeField] private Button _showButton;
        [SerializeField] private Button[] _hideButtons;
        [SerializeField] private Button _audioSwitchBtn;
        [SerializeField] private Button _vibrateSwitchBtn;

        [Space]
        [SerializeField] private Button _privacyPolicyBtn;
        [SerializeField] private TMP_Text _versionText;

        private Senser _audioInfo;
        private Senser _vibrationInfo;

        public bool isShowed { get; private set; }
        public bool isVisible { get; private set; }

        protected void Start()
        {
            _audioInfo = DIContainer.Collect<ISenser>().OfType<Senser>().Where((senser) => senser.type == SenserType.Audio).Last();
            _vibrationInfo = DIContainer.Collect<ISenser>().OfType<Senser>().Where((senser) => senser.type == SenserType.Vibration).Last();

            _audioSwitchBtn.onClick.AddListener(SwitchAudio);
            _vibrateSwitchBtn.onClick.AddListener(SwitchVibrate);
            _showButton.onClick.AddListener(Show);

            foreach (Button button in _hideButtons)
                button.onClick.AddListener(Hide);

            _privacyPolicyBtn.onClick.AddListener(OpenPrivacyPolicy);
            _versionText.text = "VERSION ID (" + Application.version + ")";
        }

        public void SwitchPanel()
        {
            if (!isShowed)
                Show();
            else
                Hide();
        }

        public void Show()
        {
            _window.SetActive(true);
            isVisible = true;
            isShowed = true;

            OnSwitchedAudio(_audioInfo.isEnable);
            OnSwitchedVibrate(_vibrationInfo.isEnable);

        }

        public void Hide()
        {
            _window.SetActive(false);

            isVisible = false;
            isShowed = false;
        }

        public void SwitchAudio()
        {
            _audioInfo.SwitchEnable();
            OnSwitchedAudio(_audioInfo.isEnable);
        }

        public void SwitchVibrate()
        {
            _vibrationInfo.SwitchEnable();
            OnSwitchedVibrate(_vibrationInfo.isEnable);
        }

        public void OnSwitchedAudio(bool enable)
        {
            _audioElement.Switch(enable);
        }

        public void OnSwitchedVibrate(bool enable)
        {
            _vibrationElement.Switch(enable);
        }

        private void OpenPrivacyPolicy()
        {
            Application.OpenURL("https://verariumgames.com/privacy");
        }
    }
}