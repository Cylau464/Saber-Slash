namespace States.Characters
{
    public class CharacterNeutralState : CharacterState
    {
        public CharacterNeutralState(CharacterStateMachine machine, CharacterStateFactory factory) : base(machine, factory)
        {
            IsRootState = true;
        }

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