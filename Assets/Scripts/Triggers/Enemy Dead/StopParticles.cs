using UnityEngine;

namespace Triggers
{
    public class StopParticles : EnemyDeadTrigger
    {
        [SerializeField] private ParticleSystem[] _particles;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (_stateMachine.IsDead == true)
                OnDead();
        }

        protected override void OnDead()
        {
            foreach (ParticleSystem particle in _particles)
                particle.Stop();
        }
    }
}