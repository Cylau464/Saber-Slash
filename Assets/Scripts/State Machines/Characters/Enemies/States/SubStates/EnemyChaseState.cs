using Game.Systems;
using UnityEngine;

namespace States.Characters.Enemy
{
    public class EnemyChaseState : EnemyState
    {
        private float _targetVerticalOffset = 2f;

        private readonly HealthSystem _target;

        public EnemyChaseState(EnemyStateMachine machine, EnemyStateFactory factory, HealthSystem target) : base(machine, factory)
        {
            _target = target;
        }

        public override void CheckSwitchStates()
        {
            float distance = Vector3.Distance(_target.transform.position, Machine.transform.position);

            if (distance <= Machine.AttackRange)
            {
                if (distance > Machine.MinAttackRange)
                    SwitchState(Factory.Attack(_target));
                else
                    SwitchState(Factory.Idle());
            }
        }

        public override void Enter()
        {
            Machine.AnimationController.Chase();
            CheckSwitchStates();
        }

        public override void Exit()
        {

        }

        public override void InitializeSubState()
        {

        }

        public override void Update()
        {
            Vector3 targetPosition = _target.transform.position + Vector3.up * _targetVerticalOffset;
            Vector3 direction = (Machine.transform.position - targetPosition).normalized;
            
            if (Machine.CanVerticalRotate == false)
                direction.y = 0f;

            if (Machine.Movable == true)
                Machine.transform.position += direction * Machine.MoveSpeed * Time.deltaTime;
            
            Quaternion rotation = Quaternion.LookRotation(direction);
            Machine.transform.rotation = Quaternion.RotateTowards(Machine.transform.rotation, rotation, Time.deltaTime * Machine.RotationSpeed);

            CheckSwitchStates();
        }
    }
}