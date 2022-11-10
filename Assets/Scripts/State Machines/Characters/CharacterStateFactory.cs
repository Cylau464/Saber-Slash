namespace States.Characters
{
    public abstract class CharacterStateFactory : BaseStateFactory
    {
        protected new CharacterStateMachine Machine => base.Machine as CharacterStateMachine;

        public CharacterStateFactory(CharacterStateMachine machine, State.ZenFactory stateFactory) : base(machine, stateFactory) { }

        public virtual State Neutral()
        {
            return StateFactory.Create<CharacterNeutralState>(Machine, this);
        }

        public virtual State Dead()
        {
            return StateFactory.Create<CharacterDeadState>(Machine, this);
        }

        public virtual State Hurt()
        {
            return StateFactory.Create<CharacterHurtState>(Machine, this);
        }

        public virtual State Move()
        {
            return StateFactory.Create<CharacterMoveState>(Machine, this);
        }

        public virtual State Idle()
        {
            return StateFactory.Create<CharacterIdleState>(Machine, this);
        }
    }
}