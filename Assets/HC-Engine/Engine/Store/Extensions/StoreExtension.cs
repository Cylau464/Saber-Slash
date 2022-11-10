using Engine.Data;

namespace Engine.Store
{
    public static class StoreExtension
    {
        public static void ResetStoreData(this StoreInfo storeInfo, string fileName)
        {
            if (storeInfo.products != null && storeInfo.products.Length != 0)
            {
                storeInfo.idSelectedProduct = new FieldKey<int>(StoreInfo.idSelectedProductKey, fileName, 0);
                storeInfo.isBoughtProducts = new FieldArray<int>(StoreInfo.isBoughtProductsKey, fileName, storeInfo.products.Length);
                storeInfo.isBoughtProducts[0] = 1;
            }
        }

        public static void UnlockStoreData(this StoreInfo storeInfo, string fileName)
        {
            if (storeInfo.products != null && storeInfo.products.Length != 0)
            {
                storeInfo.idSelectedProduct = new FieldKey<int>(StoreInfo.idSelectedProductKey, fileName, 0);
                storeInfo.isBoughtProducts = new FieldArray<int>(StoreInfo.isBoughtProductsKey, fileName, storeInfo.products.Length);

                for (int i = 0; i < storeInfo.products.Length; i++)
                {
                    storeInfo.isBoughtProducts[i] = 1;
                }
            }
        }
    }
}
