using Engine.Normal;
using Engine.Money;
using TMPro;
using UnityEngine;

namespace Main.UI.Coin
{
    public class TotalCoinsText : MonoBehaviour, IMoneyUpdated
    {
        [Header("Settings")]
        [SerializeField] protected TextMeshProUGUI _textTotalCoins;

        protected IMoneyManager m_CoinsInfo;

        protected void Awake()
        {
            m_CoinsInfo = Engine.DI.DIContainer.AsSingle<IMoneyManager>();
        }

        protected virtual void OnEnable()
        {
            _textTotalCoins.text = Normalization.NormalizeScore(m_CoinsInfo.totalCoins);
            m_CoinsInfo.OnMoneyUpdated.Subscribe(this);
        }

        protected virtual void OnDisable()
        {
            m_CoinsInfo.OnMoneyUpdated.Unsubscribe(this);
        }

        public virtual void OnMoneyUpdated(ParametersUpdate parameters)
        {
            _textTotalCoins.text = parameters.total.ToString();
        }
    }
}