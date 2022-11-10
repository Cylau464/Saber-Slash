using Main.Level;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class LevelRewardCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _counitngDuration = 1f;
        [SerializeField] private float _delay = .5f;
        [SerializeField] private AnimationCurve _textScaleCurve;

        [Inject] private ILevelsManager _levelsManager;

        private void Start()
        {
            _text.text = "+0";
            StartCoroutine(Counting());
        }

        private IEnumerator Counting()
        {
            yield return new WaitForSeconds(_delay);

            float t = 0f;
            int targetValue = _levelsManager.level.RewardHandler.Reward;

            while (t < 1f)
            {
                t += Time.deltaTime / _counitngDuration;
                _text.text = "+" + Mathf.RoundToInt(Mathf.Lerp(0f, targetValue, t));
                _text.transform.localScale = Vector3.one * _textScaleCurve.Evaluate(t);

                yield return null;
            }
        }
    }
}