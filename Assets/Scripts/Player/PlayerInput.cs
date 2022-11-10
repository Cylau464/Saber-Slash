using Engine.DI;
using Engine.Input;
using Main.UI;
using System;
using UnityEngine;

namespace Game.Player
{
    public class PlayerInput : MonoBehaviour, IBeginDrag, IDrag, IEndDrag
    {
        [SerializeField] private float _sensetivity = 2f;

        private Vector2 _movementDirection = Vector2.zero;
        public Vector2 movementDirection => _movementDirection;//_movementDirection;

        private IPlayUI _playUI;

        public Action onMovementJoystickReleased { get; set; }
        public Action<Vector2> OnDragging { get; set; }
        public Action OnStartDragging { get; set; }
        public Action OnEndDragging { get; set; }

        private void Awake()
        {
            _playUI = DIContainer.AsSingle<IPlayUI>();
        }

        private void OnEnable()
        {
            //_playUI.OnMovementReleased += OnMovementReleased;

            InputEvents.BeginDrag.Subscribe(this);
            InputEvents.Drag.Subscribe(this);
            InputEvents.EndDrag.Subscribe(this);
        }

        private void OnDisable()
        {
            //_playUI.OnMovementReleased -= OnMovementReleased;

            InputEvents.BeginDrag.Unsubscribe(this);
            InputEvents.Drag.Unsubscribe(this);
            InputEvents.EndDrag.Unsubscribe(this);
        }

        private void OnMovementReleased()
        {
            onMovementJoystickReleased?.Invoke();
        }

        public void OnBeginDrag(InputInfo data)
        {
            _movementDirection = Vector2.zero;
            OnStartDragging?.Invoke();
        }

        public void OnDrag(InputInfo data)
        {
            _movementDirection = data.lastDaltaDrag * _sensetivity;
            OnDragging?.Invoke(_movementDirection);
        }

        public void OnEndDrag(InputInfo data)
        {
            _movementDirection = Vector2.zero;
            _movementDirection.y = 0;

            OnEndDragging?.Invoke();
        }
    }
}