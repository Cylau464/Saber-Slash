using UnityEngine;
using Weapons;

namespace States.Characters.Player
{
    public class PlayerNeutralState : CharacterNeutralState
    {
        protected new PlayerStateMachine Machine => base.Machine as PlayerStateMachine;
        protected new PlayerStateFactory Factory => base.Factory as PlayerStateFactory;

        private readonly RageMode _rageMode;

        private Weapon _weapon;

        public PlayerNeutralState(PlayerStateMachine machine, PlayerStateFactory factory, RageMode rageMode, WeaponSwitcher weaponSwithcer) : base(machine, factory)
        {
            _rageMode = rageMode;
            weaponSwithcer.SwitchTo(WeaponType.Sword);
            _weapon = weaponSwithcer.CurrentWeapon;
            InitializeSubState();
        }

        public override void CheckSwitchStates()
        {

        }

        public override void Enter()
        {
            _rageMode.OnActivated += RageMode;
            Machine.PlayerInput.OnDragging += MoveWeapon;
            Machine.Health.OnDeath += Dead;
        }

        public override void Exit()
        {
            _rageMode.OnActivated -= RageMode;
            Machine.PlayerInput.OnDragging -= MoveWeapon;
            Machine.Health.OnDeath -= Dead;
        }

        public override void Update()
        {
            
        }

        public override void InitializeSubState()
        {
            if (GameManager.isPlaying == false)
                SetSubState(Factory.Idle());
            else
                SetSubState(Factory.Move());
        }

        private void RageMode()
        {
            SwitchState(Factory.Rage());
        }

        private void MoveWeapon(Vector2 delta)
        {
            Machine.LightSaber.Swing(delta);
            _weapon.Move(delta);
        }

        private void Dead()
        {
            SwitchState(Factory.Dead());
        }
    }
}