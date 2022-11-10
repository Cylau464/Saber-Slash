using Cinemachine;
using UnityEngine;

namespace Engine.Camera
{
    [System.Serializable]
    public class CameraView
    {
        [SerializeField] protected string m_Tag;
        [SerializeField] protected CinemachineVirtualCamera m_VirtualCamera;

        public CinemachineVirtualCamera virtualCamera => m_VirtualCamera;
        public string tag => m_Tag;
        public bool isEnable => m_VirtualCamera != null;

        public CameraView(string tag, CinemachineVirtualCamera virtualCamera)
        {
            if (m_VirtualCamera == null) throw new System.NullReferenceException("CinemachineVirtualCamera parameter equal null!.");

            m_Tag = tag;
            m_VirtualCamera = virtualCamera;
            Off();
        }

        public void Off()
        {
            m_VirtualCamera.gameObject.SetActive(false);
        }

        public void On()
        {
            m_VirtualCamera.gameObject.SetActive(true);
        }

        public void SetFollow(Transform follow)
        {
            m_VirtualCamera.Follow = follow;
        }

        public void SetLookAt(Transform lookAt)
        {
            m_VirtualCamera.LookAt = lookAt;
        }
    }
}