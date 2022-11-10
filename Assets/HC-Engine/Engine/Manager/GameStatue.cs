using Engine.Events;
using System;

namespace Engine
{
    public abstract class GameStatue<T> : IGameStatue where T : class
    {
        private static InterfaceEvent<T> _container = new InterfaceEvent<T>();

        public virtual void End() { }

        public virtual void Start() { }

        protected static void Invoke(Action<T> action)
        {
            _container.Invoke(action);
        }

        public static void Subscribe(T interfaceSubscribe)
        {
            _container.Subscribe(interfaceSubscribe);
        }

        public static void Unsubscribe(T interfaceSubscribe)
        {
            _container.Unsubscribe(interfaceSubscribe);
        }
    }
}
