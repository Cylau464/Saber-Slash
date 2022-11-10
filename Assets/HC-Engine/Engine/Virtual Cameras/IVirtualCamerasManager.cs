using Cinemachine;
using UnityEngine;

namespace Engine.Camera
{
    public interface IVirtualCamerasManager
    {
        void AddCameraView(CameraView view);
        void AddCameraView(CameraView[] views);
        void AddCameraViewAndSwitch(CameraView view);

        CameraView FindCameraView(string tag);
        CameraView GetCurrentCameraView();

        CinemachineVirtualCamera GetVirtualCamera();


        bool SwitchTo(CameraView view);
        bool SwitchTo(string tag);

        bool SetFollow(Transform follow);
        bool SetLookAt(Transform lookAt);
        bool SetFollow(Transform follow, string tag);
        bool SetLookAt(Transform lookAt, string tag);
    }
}
