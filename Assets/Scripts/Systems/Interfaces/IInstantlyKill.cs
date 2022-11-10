using UnityEngine;

namespace Game.Systems
{
    public interface IInstantlyKill
    {
        bool IsKilled { get; }

        void Kill();
        void Kill(Vector3 forceSourcePosition, float force, float radius);
    }
}