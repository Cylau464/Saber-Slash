using System;
using UnityEngine;

namespace Triggers
{
    public class CollisionHandler : MonoBehaviour, ICollisionHandler
    {
        public Action<Collider> OnTrigEnter { get; set; }
        public Action<Collider> OnTrigExit { get; set; }
        public Action<Collision> OnColEnter { get; set; }
        public Action<Collision> OnColExit { get; set; }

        private void OnCollisionEnter(Collision collision)
        {
            OnColEnter?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            OnColExit?.Invoke(collision);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnTrigEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnTrigExit?.Invoke(other);
        }
    }
}