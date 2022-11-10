namespace States.Characters.Player
{
    public class PlayerStateFactory : CharacterStateFactory
    {
        protected new PlayerStateMachine Machine => base.Machine as PlayerStateMachine;

        public PlayerStateFactory(PlayerStateMachine machine, State.ZenFactory stateFactory) : base(machine, stateFactory) { }

        public State Rage()
        {
            return StateFactory.Create<PlayerRageState>(Machine, this);
        }

        public State Shoot()
        {
            return StateFactory.Create<PlayerShootState>(Machine, this);
        }

        public State Slicing()
        {
            return StateFactory.Create<PlayerSlicingState>(Machine, this);
        }

        public override State Idle()
        {
            return StateFactory.Create<PlayerIdleState>(Machine, this);
        }

        public override State Move()
        {
            return StateFactory.Create<PlayerMoveState>(Machine, this);
        }

        public override State Neutral()
        {
            return StateFactory.Create<PlayerNeutralState>(Machine, this);
        }

        public override State Dead()
        {
            return StateFactory.Create<PlayerDeadState>(Machine, this);
        }
    }
}