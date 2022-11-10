using Engine;
using Engine.DI;
using System;
using UnityEngine;

namespace Game.Systems
{
    public class PlayerHealthSystem : HealthSystem
    {
        [SerializeField] private float _immortalDuration = 2f;

        private IMakeContinued _makeContinued;

        public Action OnRevive { get; set; }
        public Action OnImmortalEnd { get; set; }

        protected override void Start()
        {
            base.Start();
            _makeContinued = DIContainer.AsSingle<IMakeContinued>();
            _makeContinued.OnContinued += Revive;
        }

        private void OnDestroy()
        {
            _makeContinued.OnContinued -= Revive;
        }

        private void Revive()
        {
            ResetHealth();
            OnRevive?.Invoke();
            _immortal = true;
            Invoke(nameof(ImmortalEnd), _immortalDuration);
        }

        private void ImmortalEnd()
        {
            _immortal = false;
            OnImmortalEnd?.Invoke();
        }
    }
}