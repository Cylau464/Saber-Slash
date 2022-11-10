using States;
using Zenject;

namespace DI
{
    public class StateInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<State.ZenFactory>().AsSingle();
        }
    }
}