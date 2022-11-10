using System.Collections.Generic;
using UnityEngine;

namespace Triggers
{
    public class ParentCollisionHandler : CollisionHandler, ICollisionHandler
    {
        private List<CollisionHandler> _collisionHanlders = new List<CollisionHandler>();

        public void AddChildHandler(CollisionHandler child)
        {
            child.OnColEnter += Enter;
            _collisionHanlders.Add(child);
        }

        private void OnDestroy()
        {
            foreach (CollisionHandler handler in _collisionHanlders)
                handler.OnColEnter -= Enter;
        }

        private void Enter(Collision collision)
        {
            OnColEnter?.Invoke(collision);
        }
    }
}