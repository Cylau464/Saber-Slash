using UnityEngine;
using Engine.Data;

namespace Engine.Store
{
    [CreateAssetMenu(fileName = "New Store", menuName = "Add/Store/Add Store", order = 1)]
    public class StoreInfo : ScriptableObject, IAsset
    {
        public const string idSelectedProductKey = "Selected";
        public const string isBoughtProductsKey = "Bought";

        [Header("Data")]
        public FieldKey<int> idSelectedProduct;
        public FieldArray<int> isBoughtProducts;

        [Header("Settings")]
        public StoreType storeType;
        public Product[] products;
    }
}