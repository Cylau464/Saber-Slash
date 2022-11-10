using Engine.Events;

namespace Engine.Store
{
    public interface IStore
    {
        public Event<IProductUpdated> OnProductUpdated { get; }

        bool DeselectProduct();
        bool AllowSelect(int idProduct);
        bool SelectProduct(int idProduct);

        bool AllowBuyProduct(int idProduct);
        bool BuyProduct(int idProduct);

        bool AllowRewardProduct(int idProduct);
        bool RewardProduct(int idProduct);

        int GetTotalProducts();
        int GetIDSelectedProduct();
        IProduct GetSelectedProduct();
        IProduct GetProduct(int idProduct);

        ProductStatue GetProductState(int idProduct);
    }
}
