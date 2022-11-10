using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using NaughtyAttributes;

namespace CameraExtensions
{
    public static class CameraShake
    {
        private static float _defaultAmplitude;
        private static float _defaultFrequency;
        private static NoiseSettings _defaultProfile;

        private static CancellationTokenSource _cancellationSource;

        private static bool _initialized;

        public static void Init(CinemachineBasicMultiChannelPerlin noise)
        {
            if (_initialized == true)
                return;

            _defaultAmplitude = noise.m_AmplitudeGain;
            _defaultFrequency = noise.m_FrequencyGain;
            _defaultProfile = noise.m_NoiseProfile;
            _initialized = true;
        }

        public static async void Shake(CinemachineBasicMultiChannelPerlin noise, CameraShakeSettings settings, bool stopCurrentShake = true)
        {
            if (_initialized == false)
                Init(noise);

            if (stopCurrentShake == false && _cancellationSource != null && _cancellationSource.IsCancellationRequested == false)
                return;

            if (_cancellationSource != null)
                _cancellationSource.Cancel();

            _cancellationSource = new CancellationTokenSource();

            noise.m_AmplitudeGain = settings.Amplitude;
            noise.m_FrequencyGain = settings.Frequency;

            if (settings.ChangeProfile == true)
                noise.m_NoiseProfile = settings.Profile;

            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(settings.Duration), cancellationToken: _cancellationSource.Token);
                StopShake(noise);
            }
            catch (OperationCanceledException ex)
            {
                UnityEngine.Debug.Log("Camera shake " + ex.Message);
            }
        }

        public static void StopShake(CinemachineBasicMultiChannelPerlin noise)
        {
            if (_initialized == false)
                return;

            if (_cancellationSource != null)
                _cancellationSource.Cancel();

            noise.m_AmplitudeGain = _defaultAmplitude;
            noise.m_FrequencyGain = _defaultFrequency;
            noise.m_NoiseProfile = _defaultProfile;
        }
    }

    [Serializable]
    public class CameraShakeSettings
    {
        public float Amplitude;
        public float Frequency;
        public float Duration;
        public bool ChangeProfile;
        [AllowNesting, ShowIf(nameof(ChangeProfile))] public NoiseSettings Profile;
    }
}
