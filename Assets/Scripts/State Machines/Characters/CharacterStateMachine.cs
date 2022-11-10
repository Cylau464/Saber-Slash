using Game.Systems;
using System;
using UnityEngine;

namespace States.Characters
{
    public abstract class CharacterStateMachine : BaseStateMachine
    {
        [SerializeField] protected HealthSystem _health;
        public HealthSystem Health => _health;

        public Action OnDead { get; set; }

        public virtual void Dead()
        {
            OnDead?.Invoke();
        }
    }
}