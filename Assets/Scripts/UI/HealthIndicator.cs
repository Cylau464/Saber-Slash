using Extensions;
using Game.Systems;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class HealthIndicator : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _fadeTime = .2f;

        private PlayerHealthSystem _playerHealth;

        private void OnEnable()
        {
            _playerHealth.OnHealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _playerHealth.OnHealthChanged -= OnHealthChanged;
        }

        private void Start()
        {
            _image.color = _image.color.ChangeAlpha(0f);
        }

        public void Init(PlayerHealthSystem playerHealth)
        {
            _playerHealth = playerHealth;
        }

        private void OnHealthChanged(int health)
        {
            float targetAlpha = 1f - (float)health / _playerHealth.MaxHealth;
            StopAllCoroutines();
            StartCoroutine(Fade(targetAlpha));
        }

        private IEnumerator Fade(float targetAlpha)
        {
            float t = 0f;
            Color startColor = _image.color;
            Color targetColor = startColor.ChangeAlpha(targetAlpha);

            while (t < 1f)
            {
                t += Time.deltaTime / _fadeTime;
                _image.color = Color.Lerp(startColor, targetColor, t);

                yield return null;
            }
        }
    }
}