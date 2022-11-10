using Weapons;

namespace States.Characters.Player
{
    public class PlayerMoveState : CharacterMoveState
    {
        protected new PlayerStateMachine Machine => base.Machine as PlayerStateMachine;
        protected new PlayerStateFactory Factory => base.Factory as PlayerStateFactory;

        private readonly GameManager _gameManager;

        public PlayerMoveState(PlayerStateMachine machine, PlayerStateFactory factory, GameManager gameManager) : base(machine, factory)
        {
            _gameManager = gameManager;
        }

        public override void CheckSwitchStates()
        {

        }

        public override void Enter()
        {
            _gameManager.OnCompleted += Stop;
        }

        public override void Exit()
        {
            _gameManager.OnCompleted -= Stop;
        }

        public override void InitializeSubState()
        {

        }

        public override void Update()
        {
            Machine.Move();
        }

        private void Stop()
        {
            SwitchState(Factory.Idle());
        }
    }
}