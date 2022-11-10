using Game.Systems;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class EnergySphere : MonoBehaviour
    {
        [SerializeField] private PlayerHealthSystem _playerHealth;
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private SphereCollider _collider;
        [Space]
        [SerializeField] private float _targetScale = 5f;
        [SerializeField] private float _increaseTime = .5f;
        [SerializeField] private float _fadeInTime = .25f;
        [SerializeField] private float _fadeOutTime = .5f;
        [Space]
        [SerializeField] private float _explosionForce = 10f;
        [SerializeField] private float _explosionRadius = 10f;

        private MaterialPropertyBlock _propertyBlock;

        private const string DISSOLVE_ENABLE_PROPERTY = "_DissolveEnabled";
        private const string DISSOLVE_PROGRESS_PROPERTY = "_DissolveProgress";

        private void OnEnable()
        {
            _playerHealth.OnRevive += Activate;
        }

        private void OnDisable()
        {
            _playerHealth.OnRevive -= Activate;
        }

        private void Start()
        {
            transform.localScale = Vector3.one;
            _propertyBlock = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(_propertyBlock);
            _renderer.enabled = false;
            _collider.enabled = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Activate();
        }

        public void Activate()
        {
            _collider.enabled = true;
            _renderer.enabled = true;
            transform.localScale = Vector3.one;
            _propertyBlock.SetFloat(DISSOLVE_ENABLE_PROPERTY, 1f);
            _propertyBlock.SetFloat(DISSOLVE_PROGRESS_PROPERTY, 1f);
            _renderer.SetPropertyBlock(_propertyBlock);
            StopAllCoroutines();
            StartCoroutine(Fade(0f, _fadeInTime));
            StartCoroutine(Fade(1f, _fadeOutTime, _increaseTime - _fadeOutTime));
            StartCoroutine(Scale());
        }

        private IEnumerator Fade(float targetAlpha, float time, float delay = 0f)
        {
            yield return new WaitForSeconds(delay);

            float t = 0f;
            float startAlpha = _propertyBlock.GetFloat(DISSOLVE_PROGRESS_PROPERTY);

            while (t < 1f)
            {
                t += Time.deltaTime / time;
                _propertyBlock.SetFloat(DISSOLVE_PROGRESS_PROPERTY, Mathf.Lerp(startAlpha, targetAlpha, t));
                _renderer.SetPropertyBlock(_propertyBlock);
                yield return null;
            }
        }

        private IEnumerator Scale()
        {
            float t = 0f;
            float startScale = transform.localScale.x;

            while (t < 1f)
            {
                t += Time.deltaTime / _increaseTime;
                transform.localScale = Vector3.one * Mathf.Lerp(startScale, _targetScale, t);
                yield return null;
            }

            _collider.enabled = false;
            _renderer.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((1 << other.gameObject.layer & _targetMask) != 0)
            {
                IInstantlyKill ik = other.GetComponentInParent<IInstantlyKill>();
                ik?.Kill(transform.position, _explosionForce, _explosionRadius);
            }
        }
    }
}