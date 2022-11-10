using States.Characters.Enemy;
using UnityEngine;

namespace Triggers
{
    public class EnemyDeadTrigger : MonoBehaviour
    {
        [SerializeField] protected EnemyStateMachine _stateMachine;

        protected virtual void OnEnable()
        {
            _stateMachine.OnDead += OnDead;
        }

        private void OnDisable()
        {
            _stateMachine.OnDead -= OnDead;
        }

        protected virtual void OnDead()
        {

        }
    }
}