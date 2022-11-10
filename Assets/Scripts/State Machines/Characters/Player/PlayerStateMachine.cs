using Dreamteck.Splines;
using Game.Player;
using Game.Systems;
using UnityEngine;
using Zenject;

namespace States.Characters.Player
{
    public class PlayerStateMachine : CharacterStateMachine
    {
        [SerializeField] private SplineFollower _splineFollower;
        [SerializeField] private PlayerInput _playerInput;
        public PlayerInput PlayerInput => _playerInput;
        [SerializeField] private float _moveSpeed = 6f;
        [SerializeField] private float _slowMoMoveSpeed = 2f;
        
        private bool _slowSpeed;

        [HideInInspector, Inject] public Lightsaber LightSaber;

        protected new PlayerStateFactory States { get; private set; }

        private void OnEnable()
        {
            SlowMotionSystem.OnActivated += SlowMoActivated;
            SlowMotionSystem.OnDeactivated += SlowMoDeactivated;
        }

        private void OnDisable()
        {
            SlowMotionSystem.OnActivated -= SlowMoActivated;
            SlowMotionSystem.OnDeactivated -= SlowMoDeactivated;
        }

        private void SlowMoActivated()
        {
            _slowSpeed = true;
        }

        private void SlowMoDeactivated()
        {
            _slowSpeed = false;
        }

        protected override void InitializeState()
        {
            States = _factory.Create<PlayerStateFactory>(this);
            CurrentState = States.Neutral();
            CurrentState.Enter();
        }

        public void Move()
        {
            float speed = _slowSpeed == true ? _slowMoMoveSpeed : _moveSpeed;
            _splineFollower.Move(speed * Time.deltaTime);
        }
    }
}