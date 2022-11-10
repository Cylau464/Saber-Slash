using UnityEngine;
using Cinemachine;
using Zenject;
using Engine.Camera;
using States.Characters.Player;
using CameraExtensions;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine _player;
    [SerializeField] private CameraShakeSettings _deadCameraShakeSettings;

    private CinemachineBasicMultiChannelPerlin _noise;

    [Inject] private CinemachineVirtualCamera _virtualCamera;
    [Inject] private IVirtualCamerasManager _virtualCamerasManager;

    private void OnEnable()
    {
        _player.OnDead += Shake;
    }

    private void OnDisable()
    {
        _player.OnDead -= Shake;
    }

    private void Start()
    {
        CameraView cameraView = _virtualCamerasManager.GetCurrentCameraView();
        SetCameraTarget(transform, cameraView);
        
        _noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void SetCameraTarget(Transform transform, CameraView cameraView)
    {
        cameraView.virtualCamera.Follow = transform;
        cameraView.virtualCamera.LookAt = transform;
    }

    private void Shake()
    {
        CameraShake.Shake(_noise, _deadCameraShakeSettings);
        Vibration.VibrateLong();
    }
}