using Game.Systems;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Extensions;

namespace UI
{
    public class ImmortalIndicator : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _fadeTime = .2f;

        private PlayerHealthSystem _playerHealth;

        private void OnEnable()
        {
            _playerHealth.OnRevive += Show;
            _playerHealth.OnImmortalEnd += Hide;
        }

        private void OnDisable()
        {
            _playerHealth.OnRevive -= Show;
            _playerHealth.OnImmortalEnd -= Hide;
        }

        private void Start()
        {
            _image.color = _image.color.ChangeAlpha(0f);
        }

        public void Init(PlayerHealthSystem playerHealth)
        {
            _playerHealth = playerHealth;
        }

        private void Show()
        {
            StopAllCoroutines();
            StartCoroutine(Fade(1f));
        }

        private void Hide()
        {
            StopAllCoroutines();
            StartCoroutine(Fade(0f));
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