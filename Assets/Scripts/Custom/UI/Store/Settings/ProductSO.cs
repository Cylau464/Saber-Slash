using Engine.Store;
using System;
using UnityEngine;

namespace Main.store
{
    [CreateAssetMenu(fileName = "New Product", menuName = "Add/Store/Add Product", order = 10)]
    public class ProductSO : Product
    {
        #region delegates
        private event Action<ProductStatue> _handleStateChanged;
        public event Action<ProductStatue> handleStateChanged
        {
            add
            {
                _handleStateChanged += value;
            }
            remove
            {
                _handleStateChanged -= value;
            }
        }
        #endregion

        public int id { get; private set; }
        public int price { get; private set; }
        public IStore store { get; private set; }
        public ProductStatue state { get; private set; } = ProductStatue.Non;

        public override void Initialize(IStore store, int id)
        {
            this.id = id;
            this.store = store;
            UpdateState();
        }

        public override bool Reward()
        {
            return ChangeState(ProductStatue.Selected);
        }

        public override bool Deselect()
        {
            return ChangeState(ProductStatue.Bought);
        }

        public override bool Selected()
        {
            return ChangeState(ProductStatue.Selected);
        }

        public override bool AllowReward()
        {
            return true;
        }

        public override bool AllowSelect()
        {
            return true;
        }

        public override bool UpdateState()
        {
            return ChangeState(store.GetProductState(id));
        }

        protected bool ChangeState(ProductStatue newState)
        {
            if (state != newState)
            {
                state = newState;
                _handleStateChanged?.Invoke(state);
                return true;
            }
            return false;
        }
    }
}
