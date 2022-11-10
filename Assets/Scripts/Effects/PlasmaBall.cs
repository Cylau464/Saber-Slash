using System.Collections;
using UnityEngine;

namespace Effects
{
    public class PlasmaBall : MonoBehaviour
    {
        [SerializeField] private float _fadeInTime = .25f;
        [SerializeField] private float _scaleMiltiplier = .5f;
        [SerializeField] private AnimationCurve _fadeInYScale;
        [SerializeField] private AnimationCurve _fadeInXZScale;

        [HideInInspector] public bool Initialized;

        private void Start()
        {
            if(Initialized == false)
                StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            Initialized = true;
            float t = 0f;
            transform.localScale = Vector3.zero;
            Vector3 scale;

            while (t < 1f)
            {
                t += Time.deltaTime / _fadeInTime;
                scale = new Vector3(
                    _fadeInXZScale.Evaluate(t),
                    _fadeInYScale.Evaluate(t),
                    _fadeInXZScale.Evaluate(t)
                    );
                transform.localScale = scale * _scaleMiltiplier;

                yield return null;
            }
        }
    }
}