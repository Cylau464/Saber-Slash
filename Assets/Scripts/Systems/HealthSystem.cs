using System;
using System.Collections;
using UnityEngine;

namespace Game.Systems
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 100;
        public int MaxHealth => _maxHealth;
        protected int _health;
        protected bool _immortal;

        [Header("Auto Regeneration")]
        [SerializeField] private bool _autoRegen;
        [SerializeField, NaughtyAttributes.ShowIf(nameof(_autoRegen))] private float _autoRegenDelay = 1f;
        [SerializeField, NaughtyAttributes.ShowIf(nameof(_autoRegen))] private float _regenTickDuration = 1f;
        [SerializeField, NaughtyAttributes.ShowIf(nameof(_autoRegen))] private int _regenByTickValue = 25;

        public bool IsDead => _health <= 0;

        public Action<int> OnHealthChanged { get; set; }
        public Action OnDeath { get; set; }

        protected virtual void Start()
        {
            ResetHealth();
        }

        protected void ResetHealth()
        {
            _health = _maxHealth;
            OnHealthChanged?.Invoke(_health);
        }

        public virtual void TakeDamage(int damage)
        {
            if (damage <= 0 || _immortal == true) return;

            _health = Mathf.Max(0, _health - damage);
            OnHealthChanged?.Invoke(_health);

            if (_health <= 0)
            {
                OnDeath?.Invoke();
            }
            else if(_autoRegen == true)
            {
                StopAllCoroutines();
                StartCoroutine(Regeneration());
            }
        }

        private IEnumerator Regeneration()
        {
            yield return new WaitForSeconds(_autoRegenDelay);

            while (_health < _maxHealth)
            {
                _health += _regenByTickValue;
                OnHealthChanged?.Invoke(_health);

                yield return new WaitForSeconds(_regenTickDuration);
            }
        }
    }
}