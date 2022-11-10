using Cinemachine;
using Engine.DI;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Camera
{
    public class VirtualCamerasManager : IVirtualCamerasManager, IDependency
    {
        private List<CameraView> m_CamerasViews = new List<CameraView>();
        private CameraView m_CurrentView;

        public void Inject()
        {
            DIContainer.RegisterAsSingle<IVirtualCamerasManager>(this);
        }

        public void AddCameraView(CameraView view)
        {
            m_CamerasViews.Add(view);
        }

        public void AddCameraView(CameraView[] views)
        {
            m_CamerasViews.AddRange(views);
        }

        public CameraView FindCameraView(string tag)
        {
            return m_CamerasViews.Find(x => x.tag.CompareTo(tag) == 0);
        }

        public void AddCameraViewAndSwitch(CameraView view)
        {
            m_CamerasViews.Add(view);
            SwitchTo(view);
        }

        public bool SwitchTo(CameraView view)
        {
            if (m_CurrentView != null && m_CurrentView.isEnable)
                m_CurrentView.Off();

            m_CurrentView = view;
            if (m_CurrentView == null || !m_CurrentView.isEnable)
                return false;
            else
            {
                m_CurrentView.On();
                return true;
            }
        }

        public bool SwitchTo(string tag)
        {
            if (m_CurrentView != null && m_CurrentView.isEnable)
                m_CurrentView.Off();

            m_CurrentView = FindCameraView(tag);
            if (m_CurrentView == null || !m_CurrentView.isEnable)
                return false;
            else
            {
                m_CurrentView.On();
                return true;
            }
        }

        public static bool SetFollow(Transform follow, CameraView view)
        {
            if (view != null && view.isEnable)
            {
                view.SetFollow(follow);
                return true;
            }
            return false;
        }

        public static bool SetLookAt(Transform lookAt, CameraView view)
        {
            if (view != null && view.isEnable)
            {
                view.SetLookAt(lookAt);
                return true;
            }
            return false;
        }

        public bool SetFollow(Transform follow)
        {
            return SetFollow(follow, m_CurrentView);
        }

        public bool SetLookAt(Transform lookAt)
        {
            return SetLookAt(lookAt, m_CurrentView);
        }

        public bool SetFollow(Transform follow, string tag)
        {
            return SetFollow(follow, FindCameraView(tag));
        }

        public bool SetLookAt(Transform lookAt, string tag)
        {
            return SetLookAt(lookAt, FindCameraView(tag));
        }

        public CinemachineVirtualCamera GetVirtualCamera()
        {
            if (m_CurrentView != null && m_CurrentView.isEnable)
                return m_CurrentView.virtualCamera;
            else
                return null;
        }

        public CameraView GetCurrentCameraView()
        {
            return m_CurrentView;
        }
    }
}
