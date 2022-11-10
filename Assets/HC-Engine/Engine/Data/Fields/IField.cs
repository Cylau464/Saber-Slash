namespace Engine.Data
{
    public interface IField<T>
    {
        public T value { get; }

        public bool hasValue { get; }
        public string fileName { get; }
    }
}