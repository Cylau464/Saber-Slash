using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Game.Systems
{
    public class EnemyHealthSystem : HealthSystem, IInstantlyKill
    {
        [SerializeField] private GameObject _forceTargetParent;
        [SerializeField, TagField] private string _unpinAfterKillTag;
        [SerializeField] private Animator _animator;

        public bool IsKilled { get; private set; }

        public void Kill()
        {
            if (IsKilled == true) return;

            TakeDamage(_health);
            IsKilled = true;
        }

        public void Kill(Vector3 forceSourcePosition, float force, float radius)
        {
            if (IsKilled == true) return;

            Kill();
            _animator.enabled = false;

            Rigidbody[] rbs = _forceTargetParent.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rb in rbs)
            {
                rb.interpolation = RigidbodyInterpolation.Interpolate;
                rb.isKinematic = false;
                rb.useGravity = true;

                if (rb.CompareTag(_unpinAfterKillTag) == true)
                    rb.transform.parent = null;
                
                rb.AddExplosionForce(force, forceSourcePosition, radius);
            }
        }
    }
}