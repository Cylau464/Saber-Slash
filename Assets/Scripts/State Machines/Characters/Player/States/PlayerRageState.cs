using Weapons;

namespace States.Characters.Player
{
    public class PlayerRageState : PlayerState
    {
        private readonly RageMode _rageMode;

        public PlayerRageState(PlayerStateMachine machine, PlayerStateFactory factory, RageMode rageMode, WeaponSwitcher weaponSwithcer) : base(machine, factory)
        {
            IsRootState = true;
            _rageMode = rageMode;
            weaponSwithcer.SwitchTo(WeaponType.Gun);
            InitializeSubState();
        }

        public override void CheckSwitchStates()
        {
            
        }

        public override void Enter()
        {
            _rageMode.OnDeactivated += RageEnd;
        }

        public override void Exit()
        {
            _rageMode.OnDeactivated -= RageEnd;
        }

        public override void InitializeSubState()
        {
            SetSubState(Factory.Move());
        }

        public override void Update()
        {
            
        }

        private void RageEnd()
        {
            SwitchState(Factory.Neutral());
        }
    }
}