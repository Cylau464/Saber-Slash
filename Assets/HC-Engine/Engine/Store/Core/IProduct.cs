namespace Engine.Store
{
    public interface IProduct
    {
        void Initialize(IStore store, int id);

        bool AllowReward();
        bool Reward();

        bool Selected();
        bool AllowSelect();
        bool Deselect();

        bool UpdateState();
    }
}
