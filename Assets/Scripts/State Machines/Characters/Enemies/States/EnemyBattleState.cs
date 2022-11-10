using Game.Systems;
using UnityEngine;

namespace States.Characters.Enemy
{
    public class EnemyBattleState : EnemyState
    {
        //private readonly ObjectSlicerBySword _slicer;
        //private readonly CollisionHandler _collisionHandler;
        private readonly HealthSystem _target;

        public EnemyBattleState(EnemyStateMachine machine,
            EnemyStateFactory factory, HealthSystem target) : base(machine, factory)
        {
            _target = target;
            IsRootState = true;
            InitializeSubState();
        }

        public override void CheckSwitchStates()
        {

        }

        public override void Enter()
        {
            Machine.Slicer.OnStartSlice += Dead;
            Machine.Health.OnDeath += Dead;
            //Machine.RayfireRigid.demolitionEvent.LocalEvent += Dead;
            Machine.CollisionHandler.OnColEnter += OnCollisionEnter;
        }

        public override void Exit()
        {
            Machine.Slicer.OnStartSlice -= Dead;
            Machine.Health.OnDeath -= Dead;
            //Machine.RayfireRigid.demolitionEvent.LocalEvent -= Dead;
            Machine.CollisionHandler.OnColEnter -= OnCollisionEnter;
        }

        public override void InitializeSubState()
        {
            SetSubState(Factory.Chase(_target));
        }

        public override void Update()
        {
            
        }

        private void Dead()
        {
            SwitchState(Factory.Dead(true));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((1 << collision.gameObject.layer & Machine.TargetLayer) != 0)
            {
                if (collision.gameObject.TryGetComponent(out HealthSystem health) == true)
                {
                    health.TakeDamage(Machine.CollisionDamage);
                    SwitchState(Factory.Dead(false));
                }
            }
        }
    }
}