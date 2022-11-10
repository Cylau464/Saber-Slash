using Dreamteck.Splines;
using Game.Systems;
using Main.Level;
using UI;
using UnityEngine;
using Zenject;

namespace DI
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private PlayerHealthSystem _playerHealth;
        [SerializeField] private SplineFollower _splineFollower;
        [SerializeField] private Renderer _fogRenderer;
        [SerializeField] private LevelRewardHandler _levelRewardHandler;

        [Inject] private HealthIndicator _healthIndicator;
        [Inject] private ImmortalIndicator _immortalIndicator;

        public override void Start()
        {
            base.Start();
            _healthIndicator.Init(_playerHealth);
            _immortalIndicator.Init(_playerHealth);
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_playerHealth).AsSingle();
            Container.BindInstance(_splineFollower).AsSingle();
            Container.BindInstance(_fogRenderer).AsSingle();
            Container.BindInstance(_levelRewardHandler).AsSingle();
        }
    }
}