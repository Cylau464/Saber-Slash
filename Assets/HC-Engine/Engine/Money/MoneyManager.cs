using System;
using Engine.DI;
using Engine.Data;
using UnityEngine;
using Engine.Events;

namespace Engine.Money
{
    public enum OperationType { Add, Minus }

    [System.Serializable]
    public class MoneyManager : IAwake, IMoneyManager, IDependency
    {
        public Event<IMoneyUpdated> OnMoneyUpdated { get; private set; } = new Event<IMoneyUpdated>();

        [SerializeField] private MoneyInfo m_MoneyInfo;

        private FieldKey<int> _totalCoins;

        public int totalCoins => _totalCoins.value;

        public void Inject()
        {
            DIContainer.RegisterAsSingle<IMoneyManager>(this);
        }

        public void Awake()
        {
            _totalCoins = m_MoneyInfo.totalCoins;
        }

        public void AddMoney(int amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount can't be nigative...");

            /// Update data.
            _totalCoins.value += amount;

            /// Fill data delegate.
            ParametersUpdate dData = new ParametersUpdate();
            dData.total = _totalCoins.value;
            dData.amount = amount;
            dData.operation = OperationType.Add;

            // Execute delegate.
            OnMoneyUpdated.Events.Invoke((updated) => updated.OnMoneyUpdated(dData));
        }

        public void RemoveMoney(int amount)
        {
            if (amount <= 0 || _totalCoins.value < amount) throw new ArgumentException("Amount can't be less then totalCoins!...");

            /// Update data.
            _totalCoins.value = Mathf.Clamp(_totalCoins.value - amount, 0, _totalCoins.value);

            /// Fill data delegate.
            ParametersUpdate dData = new ParametersUpdate();
            dData.total = _totalCoins.value;
            dData.amount = amount;
            dData.operation = OperationType.Minus;

            // Execute delegate.
            OnMoneyUpdated.Events.Invoke((updated) => updated.OnMoneyUpdated(dData));
        }
    }
}
