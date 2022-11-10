using Game.Systems;
using UnityEngine;
using Weapons;

namespace States.Characters.Enemy
{
    public class EnemyAttackState : EnemyState
    {
        private float _currentAttackDelay;
        private bool _waitAttack;
        private float _targetVerticalOffset = 2f;

        private readonly HealthSystem _target;

        public EnemyAttackState(EnemyStateMachine machine, EnemyStateFactory factory, HealthSystem target) : base(machine, factory)
        {
            _target = target;
            CheckSwitchStates();
        }

        public override void CheckSwitchStates()
        {
            float distance = Vector3.Distance(_target.transform.position, Machine.transform.position);

            if (distance > Machine.AttackRange)
                SwitchState(Factory.Chase(_target));
            else if (distance <= Machine.MinAttackRange)
                SwitchState(Factory.Idle());
        }

        public override void Enter()
        {
            Attack();
        }

        public override void Exit()
        {
            Machine.AnimationController.OnAttacked -= GiveDamage;
            Machine.AnimationController.OnAttacked -= Shot;
            Machine.AnimationController.OnStartAttack -= StartAttack;
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

            Quaternion rotation = Quaternion.LookRotation(direction);
            Machine.transform.rotation = Quaternion.RotateTowards(Machine.transform.rotation, rotation, Time.deltaTime * Machine.RotationSpeed);

            if (_currentAttackDelay > 0f)
                _currentAttackDelay -= Time.deltaTime;
            else if(_waitAttack == false)
                Attack();

            CheckSwitchStates();
        }

        private void Attack()
        {
            Machine.AnimationController.Attack();
            _waitAttack = true;
            Machine.AnimationController.OnStartAttack += StartAttack;

            if (Machine.AttackType == AttackType.Range)
                Machine.AnimationController.OnAttacked += Shot;
            else
                Machine.AnimationController.OnAttacked += GiveDamage;

            _currentAttackDelay = Machine.AttackDelay;
        }

        private void StartAttack()
        {
            Machine.AnimationController.OnStartAttack -= StartAttack;

            if(Machine.AttackType == AttackType.Range)
                Machine.Weapon.Prepare();
        }

        private void GiveDamage()
        {
            Machine.AnimationController.OnAttacked -= GiveDamage;
            _target.TakeDamage(Machine.AttackDamage);
        }

        private void Shot()
        {
            Machine.Weapon.Shot(_target.transform, Machine.AttackDamage);
            Machine.AnimationController.OnAttacked -= Shot;
            _waitAttack = false;
        }
    }
}