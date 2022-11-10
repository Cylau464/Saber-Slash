using CameraExtensions;
using Cinemachine;

namespace States.Characters.Enemy
{
    public class EnemyDeadState : CharacterDeadState
    {
        protected new EnemyStateMachine Machine => base.Machine as EnemyStateMachine;
        protected new EnemyStateFactory Factory => base.Factory as EnemyStateFactory;

        public EnemyDeadState(EnemyStateMachine machine, EnemyStateFactory factory, RageMode rageMode, bool sliced) : base(machine, factory)
        {
            IsRootState = true;

            if (sliced == true)
                rageMode.AddPoints(Machine.RagePoints);
        }

        public override void CheckSwitchStates()
        {

        }

        public override void Enter()
        {
            base.Enter();
            //SlowMotionSystem.Deactivate(Machine);
        }

        public override void Exit()
        {

        }

        public override void InitializeSubState()
        {

        }

        public override void Update()
        {

        }
    }
}