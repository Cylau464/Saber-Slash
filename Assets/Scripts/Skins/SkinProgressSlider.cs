using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Skins
{
    public class SkinProgressSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private Image _icon;
        [SerializeField] private float _fillProgressTime = 1f;
        [SerializeField] private float _delay = .5f;

        private SkinPreset _unlockingSkin;

        [Inject]
        private void Init(SkinsHandler skinsHandler)
        {
            _slider.maxValue = 100;
            _unlockingSkin = skinsHandler.UnlockingInProgress;
            _slider.value = _unlockingSkin.Data.value.UnlockProgress;
            _progressText.text = _slider.value + " %";
            _icon.sprite = _unlockingSkin.Icon;
        }

        private void Start()
        {
            StartCoroutine(FillProgress());
        }

        private IEnumerator FillProgress()
        {
            yield return new WaitForSeconds(_delay);

            float t = 0f;
            float startProgress = _slider.value;
            float targetProgress = _unlockingSkin.Data.value.UnlockProgress;
            float progress;

            while (t < 1f)
            {
                t += Time.deltaTime / _fillProgressTime;
                progress = Mathf.Lerp(startProgress, targetProgress, t);
                _progressText.text = Mathf.RoundToInt(progress) + " %";
                _slider.value = progress;

                yield return null;
            }
        }
    }
}