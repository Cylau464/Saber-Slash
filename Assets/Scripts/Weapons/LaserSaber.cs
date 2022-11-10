using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Weapons
{
    public class LaserSaber : Weapon
    {
        [SerializeField] private Transform _blade;
        [SerializeField] private float _distanceFromCamera = 1.4f;
        [SerializeField] private float _moveRadiusOffset = 50f;
        [SerializeField] private float _rotationOffset = 1f;
        [SerializeField] private float _rotateSpeed = 10f;
        [SerializeField] private float _moveTime = .1f;
        [SerializeField] private float _deltaThreshold = 20f;
        [SerializeField] private float _bladeRotateTime = .05f;
        [SerializeField] protected Vector3 _bladeDirection;
        [SerializeField] private Lightsaber _lightsaber;

        private Vector3 _weaponCenterPosition;
        private List<Vector2> _lastDeltas = new List<Vector2>(5);
        private bool _resetDeltas;

        [Inject] private GameManager _gameManager;

        private void OnEnable()
        {
            _gameManager.OnStarted += ActivateSaber;
        }

        private void OnDisable()
        {
            _gameManager.OnStarted -= ActivateSaber;
        }

        private void ActivateSaber()
        {
            _lightsaber.LightsaberOn();
        }

        protected override void Spawn()
        {
            _weaponCenterPosition = new Vector3(Screen.width / 2, Screen.height / 3, _distanceFromCamera);
            Vector3 pos = _camera.WorldToScreenPoint(transform.TransformPoint(new Vector3(0f, 0f, _distanceFromCamera)));
            transform.localPosition = transform.InverseTransformPoint(_camera.ScreenToWorldPoint(pos));
            _startPosition = transform.localPosition;
            _startRotation = transform.localRotation;
        }

        private void LateUpdate()
        {
            if (_resetDeltas == true)
                _lastDeltas.Clear();
            else
                _resetDeltas = true;
        }

        public override void Move(Vector2 delta)
        {
            if (_equiped == false) return;

            //Vector2 averageDelta = Vector2.zero;
            //_resetDeltas = false;
            //foreach (Vector2 lastDelta in _lastDeltas)
            //    averageDelta += lastDelta;

            //if (_lastDeltas.Count < _lastDeltas.Capacity)
            //{
            //    _lastDeltas.Add(delta);
            //}
            //else
            //{
            //    _lastDeltas.Insert(0, delta);
            //    _lastDeltas.RemoveAt(_lastDeltas.Count - 1);
            //    _lastDeltas.Capacity = _lastDeltas.Count;
            //}

#if UNITY_EDITOR
            _deltaThreshold = 10f;
#endif

            delta.x = Mathf.Abs(delta.x) >= _deltaThreshold ? delta.x : 0f;
            delta.y = Mathf.Abs(delta.y) >= _deltaThreshold ? delta.y : 0f;
            //averageDelta += delta;
            //delta = averageDelta.normalized * delta.magnitude; //= averageDelta / (_lastDeltas.Count + 1); //Vector2.Lerp(delta, _lastDelta, .5f);
            Vector3 centerPoint = _camera.ScreenToWorldPoint(_weaponCenterPosition);
            Vector3 screenPos = _camera.WorldToScreenPoint(transform.position);
            Vector2 newPos = (Vector2)screenPos + delta; //Input.mousePosition;//
            newPos.x = Mathf.Clamp(newPos.x, 0f, Screen.width);
            newPos.y = Mathf.Clamp(newPos.y, 0f, Screen.height);
            Vector2 centerOffset = Vector2.ClampMagnitude(newPos - (Vector2)_weaponCenterPosition, Screen.width / 2f - _moveRadiusOffset);
            newPos = (Vector2)_weaponCenterPosition + centerOffset;

            Vector3 pointFarNear = _camera.ScreenToWorldPoint(new Vector3(newPos.x, newPos.y, _distanceFromCamera));
            Vector3 pointFar = _camera.ScreenToWorldPoint(new Vector3(newPos.x, newPos.y, _distanceFromCamera + _rotationOffset));

            Vector3 swordDirection = (pointFar - centerPoint).normalized;
            Vector3 targetPosition = transform.parent.InverseTransformPoint(pointFarNear);

            //Debug.Log(delta);
            Vector3 bladeDirection = Quaternion.LookRotation(swordDirection) * _bladeDirection;
            Vector3 normal = Vector3.Cross(delta.normalized, bladeDirection);
            Vector3 tangent = Vector3.Cross(Vector3.Cross(normal, bladeDirection), normal);
            Quaternion bladeRotation = Quaternion.Inverse(_blade.parent.rotation) * Quaternion.LookRotation(tangent, normal);
            bladeRotation = Quaternion.Euler(_blade.localEulerAngles.x, bladeRotation.eulerAngles.y, _blade.localEulerAngles.z);
            //_blade.localRotation = bladeRotation;

            Quaternion targetRotation = Quaternion.Inverse(transform.parent.rotation) * Quaternion.LookRotation(swordDirection);
            targetRotation = Quaternion.Euler(targetRotation.eulerAngles.x, targetRotation.eulerAngles.y, transform.localEulerAngles.z);

            StopAllCoroutines();
            StartCoroutine(StartMove(targetPosition, targetRotation, _moveTime));
            StartCoroutine(RotateBlade(bladeRotation));
        }

        private IEnumerator RotateBlade(Quaternion targerRotation)
        {
            float t = 0f;
            Quaternion startRotation = _blade.localRotation;

            while (t < 1f)
            {
                t += Time.unscaledDeltaTime / _bladeRotateTime;
                _blade.localRotation = Quaternion.Slerp(startRotation, targerRotation, t);

                yield return null;
            }
        }

        public override void Equip()
        {
            base.Equip();
            _virtualCamera.localRotation = Quaternion.identity;
        }
    }
}