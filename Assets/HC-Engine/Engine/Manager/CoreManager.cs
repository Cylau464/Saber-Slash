
namespace Engine
{
    public abstract class CoreManager<T> : StatueManager<T> where T : UnityEngine.Object
    {
        protected void Awake()
        {
            InitializeSingleton();

            Timer.Initialize();

            OnInitialize();
        }

        protected abstract void OnInitialize();
    }
}