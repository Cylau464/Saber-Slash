using Cinemachine;
using Game.Systems;
using System.Collections;
using UnityEngine;

namespace Systems
{
    public class CameraZoomSystem : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private float _targetFOV = 60f;
        [SerializeField] private float _fadeTime = .25f;

        private float _defaultFOV;

        private void Start()
        {
            _defaultFOV = _virtualCamera.m_Lens.FieldOfView;
        }

        private void OnEnable()
        {
            SlowMotionSystem.OnActivated += Activate;
            SlowMotionSystem.OnDeactivated += Deactivate;
        }

        private void OnDisable()
        {
            SlowMotionSystem.OnActivated -= Activate;
            SlowMotionSystem.OnDeactivated -= Deactivate;
        }

        private void Activate()
        {
            StopAllCoroutines();
            StartCoroutine(Fade(_targetFOV));
        }

        private void Deactivate()
        {
            StopAllCoroutines();
            StartCoroutine(Fade(_defaultFOV));
        }

        private IEnumerator Fade(float targetFOV)
        {
            float t = 0f;
            float startFOV = _virtualCamera.m_Lens.FieldOfView;

            while (t < 1f)
            {
                t += Time.unscaledDeltaTime / _fadeTime;
                _virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);

                yield return null;
            }
        }
    }
}