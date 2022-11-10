namespace States.Characters.Player
{
    public class PlayerIdleState : CharacterIdleState
    {
        protected new PlayerStateMachine Machine => base.Machine as PlayerStateMachine;
        protected new PlayerStateFactory Factory => base.Factory as PlayerStateFactory;

        private GameManager _gameManager;

        public PlayerIdleState(PlayerStateMachine machine, PlayerStateFactory factory, GameManager gameManager) : base(machine, factory)
        {
            _gameManager = gameManager;
        }

        public override void CheckSwitchStates()
        {

        }

        public override void Enter()
        {
            //if (GameManager.isStarted == true)
            //    Move();
            //else
                _gameManager.OnStarted += Move;
        }

        public override void Exit()
        {
            _gameManager.OnStarted -= Move;
        }

        public override void InitializeSubState()
        {

        }

        public override void Update()
        {

        }

        private void Move()
        {
            SwitchState(Factory.Move());
        }
    }
}