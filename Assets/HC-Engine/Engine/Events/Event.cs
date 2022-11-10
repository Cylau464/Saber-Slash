namespace Engine.Events
{
    public class Event<TEvent> where TEvent : class
    {
        internal InterfaceEvent<TEvent> Events = new InterfaceEvent<TEvent>();

        public void Subscribe(TEvent eventValue) => Events.Subscribe(eventValue);
        public void Unsubscribe(TEvent eventValue) => Events.Unsubscribe(eventValue);

        internal void UnsubscribeAll() => Events.UnsubscribeAll();

        internal void CleanNulls() => Events.CleanNulls();
    }
}