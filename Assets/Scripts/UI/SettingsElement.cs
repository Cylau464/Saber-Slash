using TMPro;
using UnityEngine;

namespace Main.UI
{
    public class SettingsElement : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private TMP_Text _text;

        private int _enableParamID;

        private bool _enable;

        private void Awake()
        {
            _enableParamID = Animator.StringToHash("enable");
        }

        public void Switch(bool enable)
        {
            _enable = enable;
            _animator.SetBool(_enableParamID, enable);
        }

        private void ChangeText()
        {
            _text.text = _enable == true ? "on" : "off";
        }
    }
}