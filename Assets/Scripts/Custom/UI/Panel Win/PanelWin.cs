using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Main.UI
{
    public class PanelWin : Panel
    {
        [SerializeField] private GameObject _window;
        [SerializeField] private Image _blindgingImage;
        [SerializeField] private float _blindingDuration = .25f;
        [SerializeField] private float _unblindingDelay = .25f;
        [SerializeField] private float _unblindingDuration = .5f;

        public override void Show()
        {
            base.Show();
            StartCoroutine(Blinding());
        }

        public void NextLevel()
        {
            GameScenes.ReloadScene();
        }

        private IEnumerator Blinding()
        {
            _window.SetActive(false);
            float t = 0f;
            Color startColor = _blindgingImage.color;
            Color targetColor = startColor;
            startColor.a = 0f;
            targetColor.a = 1f;

            while (t < 1f)
            {
                t += Time.deltaTime / _blindingDuration;
                _blindgingImage.color = Color.Lerp(startColor, targetColor, t);

                yield return null;
            }

            yield return new WaitForSeconds(_unblindingDelay);
            _window.SetActive(true);

            t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime / _unblindingDuration;
                _blindgingImage.color = Color.Lerp(targetColor, startColor, t);

                yield return null;
            }
        }
    }
}
