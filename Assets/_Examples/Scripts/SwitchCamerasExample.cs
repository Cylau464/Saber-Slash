using Engine.Camera;
using UnityEngine;
using Engine;
using Main;

namespace Examples
{
    [System.Serializable]
    public class LevelsLogic : ILevelFailed, ILevelStarted
    {
        public IVirtualCamerasManager virtualCamerasManager;

        public string _startViewTag = "OnStart";
        public CameraView _loseView;

        public void LevelFailed()
        {
            virtualCamerasManager = Engine.DI.DIContainer.AsSingle<IVirtualCamerasManager>();

            virtualCamerasManager.AddCameraViewAndSwitch(_loseView);
        }

        public void LevelStarted()
        {
            virtualCamerasManager = Engine.DI.DIContainer.AsSingle<IVirtualCamerasManager>();

            virtualCamerasManager.SwitchTo(_startViewTag);
        }
    }

    public class SwitchCamerasExample : MonoBehaviour
    {
        [SerializeField] private LevelsLogic _controllerViewCamera;

        void OnEnable()
        {
            LevelStatueStarted.Subscribe(_controllerViewCamera);
            LevelStatueFailed.Subscribe(_controllerViewCamera);
        }

        void OnDestroy()
        {
            LevelStatueStarted.Unsubscribe(_controllerViewCamera);
            LevelStatueFailed.Unsubscribe(_controllerViewCamera);
        }
    }
}
