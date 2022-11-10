using System;
using UnityEngine;
using Engine.DI;
using Engine.Data;
using Engine.Events;

namespace Engine.Store
{
    public enum StoreType { Skin }

    [Serializable]
    public class Store : IStore, IAwake, IDependency
    {
        public Event<IProductUpdated> OnProductUpdated { get; private set; } = new Event<IProductUpdated>();

        [SerializeField] StoreInfo m_Info;

        private FieldKey<int> m_IdSelectedProduct;
        private FieldArray<int> m_IsBoughtProducts;


        private StoreType m_Type;
        private Product[] m_Products;

        public StoreType type => m_Type;

        public void Inject()
        {
            DIContainer.Register<IStore>(this);
        }

        public void Awake()
        {
            SetInfo();

            InitializeProducts();
        }

        private void SetInfo()
        {
            m_IdSelectedProduct = m_Info.idSelectedProduct;
            m_IsBoughtProducts = m_Info.isBoughtProducts;

            m_Type = m_Info.storeType;
            m_Products = m_Info.products;
        }

        private void InitializeProducts()
        {
            if (m_Products == null)
                return;

            for (int i = 0; i < m_Products.Length; i++)
            {
                m_Products[i].Initialize(this, i);
            }
        }

        public void RefreshProducts()
        {
            for (int i = 0; i < m_Products.Length; i++)
            {
                m_Products[i].UpdateState();
            }
        }

        public bool DeselectProduct()
        {
            if (m_IdSelectedProduct.value < 0 || m_Products.Length <= m_IdSelectedProduct.value) return false;

            m_Products[m_IdSelectedProduct.value].Deselect();
            OnProductUpdated.Events.Invoke(item => item.OnProductUpdated(new StoreUpdateInfo(ProductStatue.Bought, type, m_Products[m_IdSelectedProduct.value])));
            m_IdSelectedProduct.value = -1;
            return true;
        }

        /// <summary>
        /// If user the use can select or choice this product.
        /// </summary>
        /// <param name="idProduct"> The id of the product. </param>
        /// <returns> True if product is enable for select.</returns>
        public virtual bool AllowSelect(int idProduct)
        {
            if (idProduct < 0 || m_Products.Length <= idProduct)
            {
                Debug.LogError("The id is out of array lenght: ID " + idProduct + ", Array Lenght: " + m_Products.Length);
                return false;
            }

            return m_IsBoughtProducts[idProduct] == 1 && m_IdSelectedProduct.value != idProduct && m_Products[idProduct].AllowSelect();
        }

        public bool SelectProduct(int idProduct)
        {
            if (!AllowSelect(idProduct))
                return false;
            else
            {
                // Deselect the old id.
                DeselectProduct();

                // update data product.
                m_IdSelectedProduct.value = idProduct;

                // Execut select on the product class.
                m_Products[idProduct].Selected();
                OnProductUpdated.Events.Invoke(item => item.OnProductUpdated(new StoreUpdateInfo(ProductStatue.Bought, type, m_Products[m_IdSelectedProduct.value])));

                return true;
            }
        }

        /// <summary>
        /// If user the use can Buy this product.
        /// </summary>
        /// <param name="idProduct"> The id of the product. </param>
        /// <returns> True if product is enable for buy.</returns>
        public virtual bool AllowBuyProduct(int idProduct)
        {
            if (idProduct < 0 || m_Products.Length <= idProduct)
            {
                throw new ArgumentOutOfRangeException("The id is out of array lenght: ID " + idProduct + ", Array Lenght: " + m_Products.Length);
            }

            return true; /// Here you need check if the player can buy the product or no.
        }
        
        /// <summary>
        /// If user the use can Buy this product.
        /// </summary>
        /// <param name="idProduct"> The id of the product. </param>
        /// <returns> True if product is enable for buy.</returns>
        public virtual bool AllowRewardProduct(int idProduct)
        {
            if (idProduct < 0 || m_Products.Length <= idProduct)
            {
                throw new ArgumentOutOfRangeException("The id is out of array lenght: ID " + idProduct + ", Array Lenght: " + m_Products.Length);
            }

            return !(m_IsBoughtProducts[idProduct] == 1) && m_Products[idProduct].AllowReward();
        }

        public bool BuyProduct(int idProduct)
        {
            if (!AllowBuyProduct(idProduct))
                return false;

            return RewardProduct(idProduct);
        }

        public bool RewardProduct(int idProduct)
        {
            if (!AllowRewardProduct(idProduct))
                return false;

            DeselectProduct();

            // Execute buy the product.
            m_IsBoughtProducts[idProduct] = 1;
            m_Products[idProduct].Reward();

            // Execute select the product.
            SelectProduct(idProduct);

            return true;
        }

        public IProduct GetProduct(int idProduct)
        {
            if (idProduct < 0 || m_Products.Length <= idProduct)
            {
                Debug.LogError("The id is out of array lenght: ID " + idProduct + ", Array Lenght: " + m_Products.Length);
                return null;
            }

            return m_Products[idProduct];
        }

        public int GetTotalProducts()
        {
            return m_Products.Length;
        }

        public IProduct GetSelectedProduct()
        {
            return GetProduct(m_IdSelectedProduct.value);
        }

        public int GetIDSelectedProduct()
        {
            return m_IdSelectedProduct.value;
        }
        
        public virtual ProductStatue GetProductState(int idProduct)
        {
            if ((uint)m_Products.Length <= (uint)idProduct) throw new ArgumentOutOfRangeException();

            if (GetIDSelectedProduct() == idProduct)
                return ProductStatue.Selected;

            if (m_IsBoughtProducts[idProduct] == 1)
                return ProductStatue.Bought;

            return ProductStatue.ForBuy;
        }
    }
}
