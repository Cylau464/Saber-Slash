using Engine.DI;
using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace Engine.Camera
{
    public class CamerasController : MonoBehaviour, IDependency
    {
        [SerializeField] private UnityEngine.Camera m_InitCamera;

        [SerializeField] private string m_DefaultSwitch = "Default";
        [SerializeField] private List<CameraView> m_Views;

        [Inject] private IVirtualCamerasManager m_VirtualCamerasManager;

        public void Inject()
        {
            DIContainer.Register(m_InitCamera);
        }

        protected void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            //m_VirtualCamerasManager = DIContainer.AsSingle<IVirtualCamerasManager>();

            m_VirtualCamerasManager.AddCameraView(m_Views.ToArray());
            m_VirtualCamerasManager.SwitchTo(m_DefaultSwitch);
        }
    }
}
