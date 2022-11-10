using BzKovSoft.ObjectSlicerSamples;
using Game.Systems;
using Triggers;
using UnityEngine;

namespace States.Characters.Enemy
{
    public class EnemyHurtState : CharacterNeutralState
    {
        protected new EnemyStateMachine Machine => base.Machine as EnemyStateMachine;
        protected new EnemyStateFactory Factory => base.Factory as EnemyStateFactory;

        private readonly ObjectSlicerBySword _slicer;
        private readonly CollisionHandler _collisionHandler;

        public EnemyHurtState(EnemyStateMachine machine,
            EnemyStateFactory factory) : base(machine, factory)
        {
            IsRootState = true;
        }

        public override void Enter()
        {
            //Machine.RayfireRigid.demolitionEvent.LocalEvent += Dead;
            Machine.Slicer.OnStartSlice += Dead;
            Machine.Health.OnDeath += Dead;
            Machine.CollisionHandler.OnColEnter += OnCollisionEnter;
        }

        public override void Exit()
        {
            //Machine.RayfireRigid.demolitionEvent.LocalEvent -= Dead;
            Machine.Slicer.OnStartSlice -= Dead;
            Machine.Health.OnDeath -= Dead;
            Machine.CollisionHandler.OnColEnter -= OnCollisionEnter;
        }

        private void Dead()
        {
            SwitchState(Factory.Dead());
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((1 << collision.gameObject.layer & Machine.TargetLayer) != 0)
            {
                if (collision.gameObject.TryGetComponent(out HealthSystem health) == true)
                {
                    health.TakeDamage(Machine.CollisionDamage);
                    Dead();
                }
            }
        }
    }
}