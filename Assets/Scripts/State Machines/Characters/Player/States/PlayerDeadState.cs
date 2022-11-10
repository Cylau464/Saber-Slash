using Cinemachine;

namespace States.Characters.Player
{
    public class PlayerDeadState : CharacterDeadState
    {
        private readonly RageMode _rageMode;
        private readonly GameManager _gameManager;

        public PlayerDeadState(PlayerStateMachine machine, PlayerStateFactory factory, GameManager gameManager) : base(machine, factory)
        {
            _gameManager = gameManager;
            _gameManager.MakeFailed();
        }

        public override void Enter()
        {
            base.Enter();
            _gameManager.OnContinued += Revive;

        }

        public override void Exit()
        {
            base.Exit();
            _gameManager.OnContinued -= Revive;
        }

        private void Revive()
        {
            SwitchState(Factory.Neutral());
        }
    }
}