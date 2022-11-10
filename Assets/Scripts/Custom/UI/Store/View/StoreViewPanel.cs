using Engine.DI;
using UnityEngine;
using System.Linq;
using Engine.Store;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Main.store
{
    public class StoreViewPanel : MonoBehaviour, IPanel
    {
        [Header("UI Objects")]
        [SerializeField] private int _storeID;
        [SerializeField] private RectTransform _contents;
        [SerializeField] private Button _headerButton;
        [SerializeField] private GameObject _productViewPrefab;

        [Header("Random")]
        [SerializeField] private int _randomSteps = 7;
        [SerializeField] private float _updateEvery = 0.5f;

        public IStore store { get; private set; }

        public int id { get; private set; }
        public bool isRandoming { get; private set; }
        public MarketViewPanel marketView { get; private set; }
        public ProductView productView { get; private set; }
        public ProductView[] productsView { get; private set; }

        public bool isVisible { get; private set; }

        public void Initialize(MarketViewPanel marketView, int id)
        {
            this.id = id;
            this.marketView = marketView;

            store = DIContainer.Collect<IStore>().OfType<Store>().NonNull().Where((store) => store.type == StoreType.Skin).Last() ?? throw new System.NullReferenceException();

            int totalProducts = store.GetTotalProducts();
            productsView = new ProductView[totalProducts];

            ProductView productView;
            for (int i = 0; i < totalProducts; i++)
            {
                productView = Instantiate(_productViewPrefab, _contents).GetComponent<ProductView>();
                productsView[i] = productView;
                productView.Initialize(this, store.GetProduct(i));
            }

            _headerButton.onClick.AddListener(() => marketView.ShowStore(this.id));
            _contents.ForceUpdateRectTransforms();

            Hide();
        }

        public void Show()
        {
            isVisible = true;
            _contents.gameObject.SetActive(true);
        }

        public void Hide()
        {
            isVisible = false;
            _contents.gameObject.SetActive(false);
        }

        public void RandomBuyProduct()
        {
            if (isRandoming == true)
                return;

            StartCoroutine(WaitAndUpdateHover(GetProductViewForBuy(), _randomSteps));
            isRandoming = true;
        }

        protected IEnumerator WaitAndUpdateHover(List<ProductView> products, int step)
        {
            yield return new WaitForSeconds(_updateEvery);
            if (0 <= step)
            {
                if (0 < products.Count)
                {
                    step--;

                    productView?.SetHover(false);
                    productView = products[Random.Range(0, products.Count)];
                    productView?.SetHover(true);

                    StartCoroutine(WaitAndUpdateHover(products, step));
                }
            }
            else
            {
                productView?.SetHover(false);
                BuyProduct(productView);
                isRandoming = false;
            }
        }

        protected List<ProductView> GetProductViewForBuy()
        {
            List<ProductView> listView = new List<ProductView>();

            for (int i = 0; i < productsView.Length; i++)
            {
                if (productsView[i].statue == ProductStatue.ForBuy)
                    listView.Add(productsView[i]);
            }

            return listView;
        }

        protected void BuyProduct(ProductView productView)
        {
            if (productView == null)
                return;

            store.BuyProduct(productView.product.id);
            store.SelectProduct(productView.product.id);
        }

        public void SelectProduct(int idProduct)
        {
            store.SelectProduct(idProduct);
        }
    }
}
