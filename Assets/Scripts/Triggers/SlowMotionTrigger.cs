using Game.Systems;
using States.Characters.Enemy;
using UnityEngine;

namespace Triggers
{
    public class SlowMotionTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject _activator;
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private float _duration = 2f;
        [SerializeField] private float _durationAfterDisable = .25f;

        private ISlowMotionActivator _slowMoActivator;
        private bool _enabled = true;

        private void Awake()
        {
            _slowMoActivator = _activator.GetComponent<ISlowMotionActivator>();
        }

        private void OnEnable()
        {
            _slowMoActivator.OnDisabled += Disable;
        }

        private void OnDisable()
        {
            _slowMoActivator.OnDisabled -= Disable;
        }

        private void Disable()
        {
            _enabled = false;
            SlowMotionSystem.Deactivate(_slowMoActivator, _durationAfterDisable);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_enabled == true && (1 << other.gameObject.layer & _targetMask) != 0)
                SlowMotionSystem.Activate(_duration, _slowMoActivator);
        }
    }
}