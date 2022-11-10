using Engine;
using Engine.Money;
using Main;
using UnityEngine;

namespace Examples
{
    public class AddCoinsExample : MonoBehaviour, ILevelCompleted, ILevelFailed, IMoneyUpdated
    {
        public IMoneyManager moneyManager;
        public int coinsTests;

        void OnEnable()
        {
            moneyManager = Engine.DI.DIContainer.AsSingle<IMoneyManager>();

            LevelStatueCompleted.Subscribe(this);
            LevelStatueFailed.Subscribe(this);

            moneyManager.OnMoneyUpdated.Subscribe(this);
        }

        void OnDisable()
        {
            LevelStatueCompleted.Unsubscribe(this);
            LevelStatueFailed.Unsubscribe(this);

            moneyManager.OnMoneyUpdated.Unsubscribe(this);
        }

        [NaughtyAttributes.Button("On Level Failed")]
        public void LevelFailed()
        {
            moneyManager.RemoveMoney(500);
            coinsTests -= 500;

            Debug.Log("TotalCoins: " + moneyManager.totalCoins);
        }

        [NaughtyAttributes.Button("On Level Completed")]
        public void LevelCompleted()
        {
            moneyManager.AddMoney(1000);
            coinsTests += 1000;

            Debug.Log("TotalCoins: " + moneyManager.totalCoins);
        }

        public void OnMoneyUpdated(ParametersUpdate parameters)
        {
            Debug.Log("Coins is updated...");
        }
    }
}