using Game.Systems;
using UnityEngine;

namespace States.Characters.Enemy
{
    public class EnemyNeutralState : CharacterNeutralState
    {
        protected new EnemyStateMachine Machine => base.Machine as EnemyStateMachine;
        protected new EnemyStateFactory Factory => base.Factory as EnemyStateFactory;
        //private ObjectSlicerBySword _slicer { set { Debug.Log(value); } }
        //[Inject] private CollisionHandler _collisionHandler { set { Debug.Log(value); } }

        public EnemyNeutralState(
            EnemyStateMachine machine,
            EnemyStateFactory factory) : base(machine, factory)
        {
            InitializeSubState();
        }

        public override void Enter()
        {
            //Machine.RayfireRigid.demolitionEvent.LocalEvent += Dead;
            Machine.Slicer.OnStartSlice += Dead;
            Machine.Health.OnDeath += Dead;
            Machine.CollisionHandler.OnColEnter += OnCollisionEnter;

            Machine.AgroTirgger.radius = Machine.AgroRange;
            Machine.AgroCollisionHandler.OnTrigEnter += OnTriggerEnter;
        }

        public override void Exit()
        {
            //Machine.RayfireRigid.demolitionEvent.LocalEvent -= Dead;
            Machine.Slicer.OnStartSlice -= Dead;
            Machine.Health.OnDeath -= Dead;
            Machine.CollisionHandler.OnColEnter -= OnCollisionEnter;
            
            Machine.AgroCollisionHandler.OnTrigEnter -= OnTriggerEnter;
        }

        public override void InitializeSubState()
        {
            SetSubState(Factory.Idle());
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
                    Debug.Log("COLLISION DAMAGE");
                    health.TakeDamage(Machine.CollisionDamage);
                    SwitchState(Factory.Dead(false));
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((1 << other.gameObject.layer & Machine.TargetLayer) != 0)
            {
                SwitchState(Factory.Battle(other.GetComponentInParent<HealthSystem>()));
            }
        }
    }
}