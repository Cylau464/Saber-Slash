using Animation;
using UnityEngine;
using Zenject;

namespace DI
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemyAnimationController _animationController;

        public override void InstallBindings()
        {
            Container.BindInstance(_animationController).AsSingle();
        }
    }
}