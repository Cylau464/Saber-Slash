using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class RageProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        [Inject] private RageMode _rageMode;

        private void Start()
        {
            _slider.value = 0f;
        }

        private void OnEnable()
        {
            _rageMode.OnPointsChanged += OnPointsChanged;
        }

        private void OnDisable()
        {
            _rageMode.OnPointsChanged -= OnPointsChanged;
        }

        private void OnPointsChanged(float progress)
        {
            
            _slider.DOValue(progress, .1f);
        }
    }
}