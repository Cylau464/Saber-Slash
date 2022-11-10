using Dreamteck.Splines;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(SplineFollower))]
    public class PlayerSplineMovement : MonoBehaviour
    {
        [SerializeField] private SplineFollower _splineFollower;
        [SerializeField] private float _speed = 10f;

        public void Move()
        {
            _splineFollower.Move(_speed * Time.deltaTime);
        }
    }
}