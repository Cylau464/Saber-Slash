using UnityEngine;

namespace States.Characters.Enemy
{
    public class EnemyIdleState : CharacterIdleState
    {
        protected new EnemyStateMachine Machine => base.Machine as EnemyStateMachine;
        protected new EnemyStateFactory Factory => base.Factory as EnemyStateFactory;

        private float _currentDelta;

        public EnemyIdleState(EnemyStateMachine machine, EnemyStateFactory factory) : base(machine, factory) { }

        public override void CheckSwitchStates()
        {
            
        }

        public override void Enter()
        {
            _currentDelta = Random.value;

            if (Machine.Flying == true)
                Machine.AnimationController.Fly();
            else
                Machine.AnimationController.Idle();
        }

        public override void Exit()
        {
            
        }

        public override void InitializeSubState()
        {

        }

        public override void Update()
        {
            if (Machine.Flying == true)
            {
                _currentDelta += Time.deltaTime * Machine.FlyingPeriod;
                Machine.transform.position = Machine.StartPosition + Mathf.Sin(_currentDelta) * Machine.FlyingAmplitude * Vector3.up;
            }
        }

        private void Move()
        {
            SwitchState(Factory.Move());
        }
    }
}