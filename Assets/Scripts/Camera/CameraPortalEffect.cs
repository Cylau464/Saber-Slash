using CameraExtensions;
using Cinemachine;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace Effects
{
    public class CameraPortalEffect : MonoBehaviour
    {
        [SerializeField] private float _maxFOV = 135f;
        [SerializeField] private float _minFOV = 35f;

        [SerializeField] private float _FOVIncreaseTime = .5f;
        [SerializeField] private float _FOVDecreaseTime = .25f;

        [SerializeField] private AnimationCurve _increaseCurve;
        [SerializeField] private AnimationCurve _decreaseCurve;

        [SerializeField] private ParticleSystem _increaseParticle;
        [SerializeField] private ParticleSystem _decreaseParticle;

        [Header("Gate")]
        [SerializeField] private Renderer _gateRenderer;
        [SerializeField] private int _materialIndex = 1;
        [SerializeField, ColorUsage(true, true)] private Color _targetGateEmissionColor;
        [SerializeField] private AnimationCurve _gateColorLerpCurve;
        
        [Header("Portal")]
        [SerializeField] private Renderer _portalRenderer;
        [SerializeField] private float _targetTwirlSpeed;

        [Header("Camera Shake")]
        [SerializeField] private CameraShakeSettings _cameraShakeSettings;

        [Header("Post Processing")]
        [SerializeField, Range(-1, 1f)] private float _increaseLensDistortionIntensity;
        [SerializeField, Range(-1, 1f)] private float _decreaseLensDistortionIntensity;
        [SerializeField, CurveRange(EColor.Red)] private AnimationCurve _increaseLensDistortionXMultiplierCurve;
        [SerializeField, CurveRange(EColor.Green)] private AnimationCurve _increaseLensDistortionYMultiplierCurve;
        [SerializeField, CurveRange(EColor.Red)] private AnimationCurve _decreaseLensDistortionXMultiplierCurve;
        [SerializeField, CurveRange(EColor.Green)] private AnimationCurve _decreaseLensDistortionYMultiplierCurve;
        private LensDistortion _lensDistortion;

        [Inject] private Volume _volume;
        [Inject] private CinemachineVirtualCamera _virtualCamera;
        [Inject] private GameManager _gameManager;

        private MaterialPropertyBlock _gatePropertyBlock;
        private MaterialPropertyBlock _portalPropertyBlock;

        private long[] _vibrationPattern = 
        {
            0,
            50, 100
        };

        private const string EMISSION_COLOR_PROPERTY = "_EmissionColor";
        private const string TWIRL_SPEED_PROPERTY = "_TwirlStrength";

        private void OnEnable()
        {
            _gameManager.OnCompleted += Activate;
        }

        private void OnDisable()
        {
            _gameManager.OnCompleted -= Activate;
        }

        private void Start()
        {
            _volume.profile.TryGet(out _lensDistortion);
        }

        private void Activate()
        {
            StartCoroutine(ChangeFov());
        }

        private IEnumerator ChangeFov()
        {
            float t = 0f;
            float startFOV = _virtualCamera.m_Lens.FieldOfView;
            _increaseParticle.Play();

            CameraShake.Shake(_virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>(), _cameraShakeSettings);
            Vibration.Vibrate(_vibrationPattern, 0);

            _gatePropertyBlock = new MaterialPropertyBlock();
            _portalPropertyBlock = new MaterialPropertyBlock();
            _gateRenderer.GetPropertyBlock(_gatePropertyBlock, _materialIndex);
            _portalRenderer.GetPropertyBlock(_portalPropertyBlock);

            Color startColor = _gateRenderer.sharedMaterial.GetColor(EMISSION_COLOR_PROPERTY);
            float startSpeed = _portalRenderer.sharedMaterial.GetFloat(TWIRL_SPEED_PROPERTY);
            float startLensDistortionIntensity = _lensDistortion.intensity.value;

            while (t < 1f)
            {
                t += Time.deltaTime / _FOVIncreaseTime;
                _virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(startFOV, _maxFOV, _increaseCurve.Evaluate(t));
                
                _gatePropertyBlock.SetColor(EMISSION_COLOR_PROPERTY, Color.Lerp(startColor, _targetGateEmissionColor, _gateColorLerpCurve.Evaluate(t)));
                _gateRenderer.SetPropertyBlock(_gatePropertyBlock, _materialIndex);

                _portalPropertyBlock.SetFloat(TWIRL_SPEED_PROPERTY, Mathf.Lerp(startSpeed, _targetTwirlSpeed, t));
                _portalRenderer.SetPropertyBlock(_portalPropertyBlock);

                _lensDistortion.intensity.value = Mathf.Lerp(startLensDistortionIntensity, _increaseLensDistortionIntensity, t);
                _lensDistortion.xMultiplier.value = _increaseLensDistortionXMultiplierCurve.Evaluate(t);
                _lensDistortion.yMultiplier.value = _increaseLensDistortionYMultiplierCurve.Evaluate(t);

                yield return null;
            }

            t = 0f;
            startFOV = _virtualCamera.m_Lens.FieldOfView;
            _decreaseParticle.Play();
            startLensDistortionIntensity = _lensDistortion.intensity.value;
            Vibration.VibrateMedium();

            while (t < 1f)
            {
                t += Time.deltaTime / _FOVDecreaseTime;
                _virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(startFOV, _minFOV, _decreaseCurve.Evaluate(t));

                _portalPropertyBlock.SetFloat(TWIRL_SPEED_PROPERTY, Mathf.Lerp(_targetTwirlSpeed, -startSpeed, t));
                _portalRenderer.SetPropertyBlock(_portalPropertyBlock);

                _lensDistortion.intensity.value = Mathf.Lerp(startLensDistortionIntensity, _decreaseLensDistortionIntensity, t);
                _lensDistortion.xMultiplier.value = _decreaseLensDistortionXMultiplierCurve.Evaluate(t);
                _lensDistortion.yMultiplier.value = _decreaseLensDistortionYMultiplierCurve.Evaluate(t);

                yield return null;
            }
        }
    }
}
