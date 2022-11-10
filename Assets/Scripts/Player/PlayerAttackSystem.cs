//using Engine.DI;
//using Game.Systems;
//using Main.UI;
//using System;
//using UnityEngine;

//namespace Game.Player
//{
//    public enum CombatType { Spell, Gun }

//    public class PlayerAttackSystem : MonoBehaviour
//    {
//        [SerializeField] private LayerMask _enemiesLayerMask;
//        [SerializeField] private GameObject _shotPrefab;

//        //private PlayerController _playerController;
//        //private ICameraGunHolder _cameraGunHolder;
//        //private ICameraSwordHolder _cameraSwordHolder;
//        //private ICameraSpellHolder _cameraSpellHolder;
//        private IPlayUI _playUI;

//        private int _enemiesAmount = 0;
//        private Action _callback;
//        private Vector3 _swordCenterPosition;
//        private Vector2 _screenCenterPosition;


//        private int _bullets = 30;
//        private int _currentBullets = 30;
//        private float _reloadingTime = 1f;
//        private float _reloadingEnd = -1f;

//        private float _shootingRate = 0.2f;
//        private float _timeAfterShot = -1f;

//        private float _distanceFromCamera = 1.4f;


//        private void Awake()
//        {
//            //_playerController = GetComponent<PlayerController>();
//            //_cameraGunHolder = DIContainer.AsSingle<ICameraGunHolder>();
//            //_cameraSwordHolder = DIContainer.AsSingle<ICameraSwordHolder>();
//            //_cameraSpellHolder = DIContainer.AsSingle<ICameraSpellHolder>();

//            _swordCenterPosition = new Vector3(Screen.width / 2, Screen.height / 8, _distanceFromCamera);
//            _screenCenterPosition = new Vector2(Screen.width / 2, Screen.height / 2);

//            _currentBullets = _bullets;

//            _playUI = DIContainer.AsSingle<IPlayUI>();
//            _playUI.UpdateBulletsAmount(_currentBullets);
//        }

//        public bool Shoot(out RaycastHit hit)
//        {
//            hit = new RaycastHit();
//            if (Time.time < _reloadingEnd) return false;
//            if (Time.time < _timeAfterShot) return false;

//            _playUI.SetActiveCooldownText(false);

//            _timeAfterShot = Time.time + _shootingRate;

//            Ray ray = Camera.main.ScreenPointToRay(_screenCenterPosition);
//            if (Physics.Raycast(ray, out hit, 100f, _enemiesLayerMask))
//            {
//                if (hit.transform.TryGetComponent(out HealthSystem healthSystem))
//                {
//                    healthSystem.TakeDamage(10);
//                }
//                Instantiate(_shotPrefab, hit.point, Quaternion.identity);
//            }

//            _currentBullets--;
//            _playUI.UpdateBulletsAmount(_currentBullets);
//            if (_currentBullets <= 0)
//            {
//                _playUI.SetActiveCooldownText(true);
//                _reloadingEnd = Time.time + _reloadingTime;
//                _currentBullets = _bullets;
//            }
//            return true;
//        }

//        public void TransitionToCombatState(CombatType combatType, HealthSystem[] healthSystems, Action callback)
//        {
//            switch(combatType)
//            {
//                //case CombatType.Gun:
//                //    _cameraGunHolder.RaiseGun();
//                //    _playerController.TransitionToState(PlayerController.States.Shooting);
//                //    break;
//                //case CombatType.Spell:
//                //    _cameraSpellHolder.RaiseSpell();
//                //    _playerController.TransitionToState(PlayerController.States.SpellCast);
//                //    break;
//            }    

//            foreach (HealthSystem healthSystem in healthSystems)
//                healthSystem.OnDeath += OnEnemyKilled;

//            _enemiesAmount = healthSystems.Length;
//            _callback = callback;
//        }

//        public void SetEnabledSword(bool enabled)
//        {
//            //if (enabled)
//            //{
//            //    _cameraSwordHolder.RaiseSword();
//            //}
//            //else
//            //{
//            //    _cameraSwordHolder.LowerSword();
//            //}
//        }

//        public void MoveSword(Vector2 direction)
//        {
//            Vector3 mousePosition = Input.mousePosition;
//            //Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 2f));
//            Vector3 centerPoint = Camera.main.ScreenToWorldPoint(_swordCenterPosition);

//            Vector3 pointFarNear = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _distanceFromCamera));
//            Vector3 pointFar = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 100f));

//            Vector3 swordDirection = (pointFar - centerPoint).normalized;

//            //_cameraSwordHolder.SwordObject.position = Vector3.Lerp(_cameraSwordHolder.SwordObject.position, centerPoint, Time.deltaTime * 10f);
//            //_cameraSwordHolder.SwordObject.position = Vector3.Lerp(_cameraSwordHolder.SwordObject.position, Vector3.Lerp(centerPoint, pointFarNear, 0.6f), Time.deltaTime * 10f);
//            //_cameraSwordHolder.SwordObject.rotation = Quaternion.Slerp(_cameraSwordHolder.SwordObject.rotation, Quaternion.LookRotation(swordDirection), Time.deltaTime * 10f);
//            //_cameraSwordHolder.SwordObject.RotateAround(Camera.main.transform.position, Camera.main.transform.right, mousePosition.y * Time.deltaTime * 100f);
//        }

//        private void OnEnemyKilled()
//        {
//            _enemiesAmount--;

//            if (_enemiesAmount <= 0)
//            {
//                _callback?.Invoke();
//            }
//        }
//    }
//}