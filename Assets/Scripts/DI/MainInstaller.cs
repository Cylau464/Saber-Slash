using Cinemachine;
using Engine;
using Engine.Camera;
using Main.Level;
using Main.UI;
using Skins;
using States;
using UI;
using UnityEngine;
using UnityEngine.Rendering;
using Weapons;
using Zenject;

namespace DI
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private LevelsManager _levelsManager;
        [SerializeField] private WeaponSwitcher _weaponSwitcher;
        [SerializeField] private MainCanvasManager _mainCanvas;
        [SerializeField] private HealthIndicator _healthIndicator;
        [SerializeField] private ImmortalIndicator _immortalIndicator;
        [SerializeField] private RageMode _rageMode;
        [SerializeField] private Transform _plane;
        [SerializeField] private Lightsaber _lightSaber;
        [SerializeField] private CinemachineVirtualCamera _playerVirtualCamera;
        [SerializeField] private LevelProgressSlider _levelProgressSlider;
        [SerializeField] private Light _directionalLight;
        [SerializeField] private Volume _volume;
        [SerializeField] private LevelsSettings _levelsSettings;

        public override void InstallBindings()
        {
            BindInstances();
            BindFactories();
            BindInterfaces();
        }

        private void BindFactories()
        {
            Container.Bind<BaseStateFactory.ZenFactory>().AsSingle();
            Container.Bind<State.ZenFactory>().AsSingle();
            Container.Bind<Level.Factory>().AsSingle();
            Container.Bind<Bullet.Factory>().AsSingle();
        }

        private void BindInstances()
        {
            Container.BindInstance(_gameManager).AsSingle();
            Container.BindInstance(_weaponSwitcher).AsSingle();
            Container.BindInstance(_mainCanvas).AsSingle();
            Container.BindInstance(_healthIndicator).AsSingle();
            Container.BindInstance(_immortalIndicator).AsSingle();
            Container.BindInstance(_rageMode).AsSingle();
            Container.BindInstance(_plane).AsSingle();
            Container.BindInstance(_lightSaber).AsSingle();
            Container.BindInstance(_playerVirtualCamera).AsSingle();
            Container.BindInstance(_levelProgressSlider).AsSingle();
            Container.BindInstance(_directionalLight).AsSingle();
            Container.BindInstance(_volume).AsSingle();
            Container.BindInstance(_levelsSettings).AsSingle();
        }

        private void BindInterfaces()
        {
            Container.Bind<IMakeStarted>().FromInstance(_gameManager);
            Container.Bind<IMakeFailed>().FromInstance(_gameManager);
            Container.Bind<IMakeCompleted>().FromInstance(_gameManager);
            Container.Bind<ILevelsManager>().FromInstance(_levelsManager);

            Container.Bind<IVirtualCamerasManager>().To<VirtualCamerasManager>().AsSingle();
        }

        //private void InstallState(DiContainer subContainer)
        //{
        //    subContainer.Bind<BaseStateFactory.ZenFactory>().AsSingle();
        //    subContainer.Bind<State.ZenFactory>().AsSingle();
        //    Debug.Log("INSTALL STATE " + subContainer);
        //}
    }
}