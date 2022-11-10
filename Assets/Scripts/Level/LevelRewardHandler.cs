using Engine.Money;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Main.Level
{
    public class LevelRewardHandler : MonoBehaviour
    {
        [Inject] private GameManager _gameManager;

        private IMoneyManager _moneyManager;

        public int Reward { get; private set; } = 0;

        private void OnEnable()
        {
            _gameManager.OnCompleted += SaveReward;
        }

        private void OnDisable()
        {
            _gameManager.OnCompleted -= SaveReward;
        }

        private void Start()
        {
            _moneyManager = Engine.DI.DIContainer.AsSingle<IMoneyManager>();
        }

        public void AddCoinsToReward(int coins)
        {
            Reward += coins;
        }

        private void SaveReward()
        {
            if(Reward > 0)
                _moneyManager.AddMoney(Reward);
        }
    }
}