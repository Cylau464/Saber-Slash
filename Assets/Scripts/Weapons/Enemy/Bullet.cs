using BzKovSoft.ObjectSlicer;
using Game.Systems;
using System;
using System.Collections;
using Triggers;
using UnityEngine;
using Zenject;

namespace Weapons
{
    public class Bullet : MonoBehaviour, ISlowMotionActivator, IInstantlyKill
    {
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private float _speed;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private BzSliceableBase _slicer;

        [Header("Destroy")]
        [SerializeField] private float _damageDelay = .1f;
        [SerializeField] private float _destroyDelay = 10f;
        [SerializeField] private float _destroyTime = .25f;
        [SerializeField] private ParticleSystem _destroyParticle;
        [SerializeField] private AnimationCurve _destroyScaleCurve;

        private int _damage;
        private bool _enabled;

        public bool IsKilled { get; private set; }

        public Action OnDisabled { get; set; }

        public class Factory : PrefabFactory<Bullet>
        {
        }

        private void OnEnable()
        {
            _slicer.OnStartSlice += Disable;
        }

        private void OnDisable()
        {
            _slicer.OnStartSlice -= Disable;
        }

        public void Enable()
        {
            _enabled = true;
        }

        private void Disable()
        {
            _enabled = false;
            _rigidBody.isKinematic = true;
            OnDisabled?.Invoke();
        }

        public void Init(int damage)
        {
            _damage = damage;
            Invoke(nameof(Enable), _damageDelay);
        }

        public void Launch(Vector3 direction)
        {
            //transform.up = direction;
            _rigidBody.AddForce(direction * _speed, ForceMode.Impulse);
            Invoke(nameof(LifeTimeDestroy), _destroyDelay);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_enabled == true)
            {
                if ((1 << other.gameObject.layer & _targetLayer) != 0)
                {
                    HealthSystem health = other.GetComponentInParent<HealthSystem>();

                    if (health != null)
                    {
                        health.TakeDamage(_damage);
                        StartCoroutine(DestroySelf());
                    }
                }
                else if ((1 << other.gameObject.layer & _enemyLayer) != 0)
                {
                    StartCoroutine(DestroySelf());
                }
            }
        }

        private void LifeTimeDestroy()
        {
            StartCoroutine(DestroySelf());
        }

        private IEnumerator DestroySelf()
        {
            float t = 0f;
            _rigidBody.isKinematic = true;
            _destroyParticle.Play();
            _destroyParticle.transform.parent = null;
            _destroyParticle.transform.localScale = Vector3.one;

            while (t < 1f)
            {
                t += Time.deltaTime / _destroyTime;
                transform.localScale = Vector3.one * _destroyScaleCurve.Evaluate(t);

                yield return null;
            }


            Destroy(gameObject);
        }

        public void Kill()
        {
            if (IsKilled == true) return;

            LifeTimeDestroy();
            IsKilled = true;
        }

        public void Kill(Vector3 forceSourcePosition, float force, float radius)
        {
            if (IsKilled == true) return;

            Kill();
            _rigidBody.AddExplosionForce(force, forceSourcePosition, radius);
        }
    }
}