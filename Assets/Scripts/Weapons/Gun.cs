using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class Gun : Weapon
    {
        [Header("Damage")]
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _damage = 10;
        [SerializeField] private float _firingRate = 2f;
        [SerializeField] private float _reloadTime = 2f;
        private float _currentShotDelay;

        [Header("Aim")]
        [SerializeField] private Vector3 _minAimAngles;
        [SerializeField] private Vector3 _maxAimAngles;
        [SerializeField] private float _sensativity = .1f;

        private void Update()
        {
            if (_currentShotDelay > 0f)
                _currentShotDelay -= Time.deltaTime * _firingRate;

            if (Input.GetMouseButton(0))
                Shot();
        }

        protected override void Spawn()
        {
            _startPosition = transform.localPosition;
            _startRotation = transform.localRotation;
            transform.localPosition = _unequipLocalPosition;
            transform.localRotation = Quaternion.Euler(_unequipLocalRotation);
        }

        public override void Move(Vector2 delta)
        {
            if (_equiped == false) return;

            delta *= _sensativity;
            Vector3 rotDelta = new Vector3(-delta.y, delta.x, 0f);
            Vector3 rotation = _virtualCamera.localEulerAngles;

            if (rotation.x > 180f)
                rotation.x -= 360f;

            if (rotation.y > 180f)
                rotation.y -= 360f;

            rotation += rotDelta;
            rotation.x = Mathf.Clamp(rotation.x, _minAimAngles.x, _maxAimAngles.x);
            rotation.y = Mathf.Clamp(rotation.y, _minAimAngles.y, _maxAimAngles.y);
            _virtualCamera.localEulerAngles = rotation;
        }
        
        private void Shot()
        {
            if (_equiped == false || _currentShotDelay > 0f) return;

            _currentShotDelay = 1f;
            Bullet bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
            bullet.transform.up = _bulletSpawnPoint.forward;
            bullet.Init(_damage);
            Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = _camera.ScreenPointToRay(screenCenter);
            bullet.Launch(ray.direction);
        }
    }
}