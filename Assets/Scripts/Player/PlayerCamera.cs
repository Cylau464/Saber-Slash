using Cinemachine;
using Engine.Camera;
using Engine.DI;
using Engine.Input;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        //private CinemachinePOV _cinemachinePOV;

        [Inject] private IVirtualCamerasManager _virtualCamerasManager;

        private void Start()
        {
            //_virtualCamerasManager = DIContainer.AsSingle<IVirtualCamerasManager>();

            //CameraView cameraView = _virtualCamerasManager.FindCameraView("Shooting");
            //cameraView.virtualCamera.transform.SetParent(transform, true);
            //_cinemachinePOV = cameraView.virtualCamera.GetCinemachineComponent<CinemachinePOV>();
            CameraView cameraView = _virtualCamerasManager.GetCurrentCameraView();
            SetCameraTarget(transform, cameraView);
        }

        public void SetShootingCamera()
        {
            //_cinemachinePOV.m_HorizontalAxis.Value = 0f;
            //_cinemachinePOV.m_VerticalAxis.Value = 0f;
            //_virtualCamerasManager.SwitchTo("Shooting");
        }

        public void SetMovingCamera()
        {
            //DOTween.To(() => _cinemachinePOV.m_HorizontalAxis.Value, (value) => { _cinemachinePOV.m_HorizontalAxis.Value = value; }, 90f, 0.5f).SetUpdate(UpdateType.Late);
            //DOTween.To(() => _cinemachinePOV.m_VerticalAxis.Value, (value) => { _cinemachinePOV.m_VerticalAxis.Value = value; }, 0f, 0.5f).SetUpdate(UpdateType.Late);
            /*_cinemachinePOV.m_HorizontalAxis.Value = 90f;
            _cinemachinePOV.m_VerticalAxis.Value = 0f;*/

            //_virtualCamerasManager.SwitchTo("Default");
        }

        private void SetCameraTarget(Transform transform, CameraView cameraView)
        {
            cameraView.virtualCamera.Follow = transform;
            cameraView.virtualCamera.LookAt = transform;
        }

        public void MoveShootingCamera(Vector3 direction)
        {
            //_cinemachinePOV.m_HorizontalAxis.Value += direction.x;
            //_cinemachinePOV.m_VerticalAxis.Value -= direction.y;
        }
    }
}