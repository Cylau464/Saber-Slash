using Skins;
using UnityEngine;
using Zenject;

namespace DI
{
    public class SkinsInstaller : MonoInstaller
    {
        [SerializeField] private SkinsContainer _skinsData;
        [SerializeField] private SkinsHandler _skinsHandler;

        public override void InstallBindings()
        {
            Container.BindInstance(_skinsData).AsSingle();
            Container.BindInstance(_skinsHandler).AsSingle();
        }
    }
}
