using Engine.Money;
using UnityEngine;

namespace Main.UI.Coin
{
    public class TotalCoinsCountText : TotalCoinsText
    {
        [Header("Counter")]
        [Range(0.1f, 10f)] [SerializeField] private float _timeCounting = 1;
        [Range(0.03f, 0.4f)] [SerializeField] private float _smouth = 0.1f;

        protected MoneyCounter m_Counter;

        protected override void OnEnable()
        {
            m_Counter = new MoneyCounter(_textTotalCoins, m_CoinsInfo.totalCoins, _timeCounting, _smouth);
            base.OnEnable();
        }

        public override void OnMoneyUpdated(ParametersUpdate parameters)
        {
            if (parameters.operation == OperationType.Add)
                m_Counter.UpdateCount(parameters.total, parameters.amount);
            else
            if (parameters.operation == OperationType.Minus)
                m_Counter.UpdateCount(parameters.total, -parameters.amount);
        }
    }
}