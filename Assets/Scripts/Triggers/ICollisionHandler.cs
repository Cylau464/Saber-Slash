using System;
using UnityEngine;

namespace Triggers
{
    public interface ICollisionHandler
    {
        public Action<Collider> OnTrigEnter { get; set; }
        public Action<Collider> OnTrigExit { get; set; }
        public Action<Collision> OnColEnter { get; set; }
        public Action<Collision> OnColExit { get; set; }
    }
}