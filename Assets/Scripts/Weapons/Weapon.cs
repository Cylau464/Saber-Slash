using System;
using System.Collections;
using UnityEngine;

namespace Weapons
{
    public enum WeaponType { Sword, Gun }

    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Transform _virtualCamera;
        [SerializeField] protected WeaponType _type;
        public WeaponType Type => _type;
        [SerializeField] protected float _equipTime = .25f;
        [SerializeField] protected float _moveSpeed = 10f;
        [SerializeField] protected Vector3 _unequipLocalPosition;
        [SerializeField] protected Vector3 _unequipLocalRotation;

        public bool Moving { get; private set; }

        protected Vector3 _startPosition;
        protected Quaternion _startRotation;
        protected bool _equiped;
        protected Camera _camera;

        protected Action _moveCallback;

        private void Awake()
        {
            _camera = Camera.main;
            Spawn();
        }

        protected abstract void Spawn();

        public abstract void Move(Vector2 delta);

        protected IEnumerator StartMove(Vector3 targetPosition, Quaternion targetRotation, float time, bool equiping = false)
        {
            Moving = true;
            float t = 0f;
            Vector3 startPostion = transform.localPosition;
            Quaternion startRotation = transform.localRotation;

            while (t < 1f)
            {
                t += Time.unscaledDeltaTime / time;
                transform.localPosition = Vector3.Lerp(startPostion, targetPosition, t);
                transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, t);

                yield return null;
            }

            if (_moveCallback != null)
            {
                _moveCallback();
                _moveCallback = null;
            }

            if (equiping == true)
                _equiped = true;

            Moving = false;
        }

        public virtual void Equip()
        {
            if (_equiped == true) return;

            StopAllCoroutines();
            StartCoroutine(StartMove(_startPosition, _startRotation, _equipTime, true));
        }

        public virtual void Unequip(Action callback)
        {
            if (_equiped == false) return;

            _equiped = false;
            _moveCallback = callback;
            StopAllCoroutines();
            StartCoroutine(StartMove(_unequipLocalPosition, Quaternion.Euler(_unequipLocalRotation), _equipTime));
        }
    }
}