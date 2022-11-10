using Engine.Events;

namespace Engine.Senser
{
    public interface ISenser
    {
        Event<ISenserUpdated> OnProductUpdated { get; }

        bool isEnable { get; }

        void SwitchEnable();

        void SetEnable(bool enable);
    }
}
