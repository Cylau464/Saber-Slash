namespace States.Characters.Enemy
{
    public class EnemyMoveState : CharacterMoveState
    {
        protected new EnemyStateMachine Machine => base.Machine as EnemyStateMachine;
        protected new EnemyStateFactory Factory => base.Factory as EnemyStateFactory;

        public EnemyMoveState(EnemyStateMachine machine, EnemyStateFactory factory) : base(machine, factory) { }

        public override void CheckSwitchStates()
        {

        }

        public override void Enter()
        {
            
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