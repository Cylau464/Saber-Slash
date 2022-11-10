namespace Engine.Store
{
    public struct StoreUpdateInfo
    {
        public ProductStatue productStatue;
        public StoreType storeType;
        public IProduct product;

        public StoreUpdateInfo(ProductStatue productStatue, StoreType storeType, IProduct product)
        {
            this.productStatue = productStatue;
            this.storeType = storeType;
            this.product = product;
        }
    }
}