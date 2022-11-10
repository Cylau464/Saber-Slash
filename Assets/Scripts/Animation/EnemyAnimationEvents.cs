using UnityEngine;
using Helpers;
using UnityEngine.Events;

namespace Animation
{
    public class EnemyAnimationEvents : AnimationEventsCaller
    {
        [SerializeField] private UnityEvent _chase;
        [SerializeField] private UnityEvent _attack;
        [SerializeField] private UnityEvent _startAttack;

        private void StartAttack()
        {
            _startAttack.Invoke();
        }

        private void Attack()
        {
            _attack.Invoke();
        }

        private void Chase()
        {
            _chase.Invoke();
        }
    }
}