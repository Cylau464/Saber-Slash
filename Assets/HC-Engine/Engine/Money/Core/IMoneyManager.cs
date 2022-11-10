using Engine.Events;

namespace Engine.Money
{
    public interface IMoneyManager
    {
        int totalCoins { get; }

        Event<IMoneyUpdated> OnMoneyUpdated { get; }

        void AddMoney(int amount);

        void RemoveMoney(int amount);
    }
}
