using Engine.DI;
using UnityEngine;

namespace Examples
{
    public class WinLostLogicExample : MonoBehaviour
    {
        public Transform _cube;

        public Transform _winPoint;
        public Transform _lostPoint;

        void Update()
        {
            if (!GameManager.isPlaying)
                return;

            /// Example of logic for win case.
            if (Vector3.Distance(_cube.position, _winPoint.position) < 3)
            {
                DIContainer.AsSingle<Engine.IMakeCompleted>().MakeCompleted();
            }
            else
            /// Example of logic for lost case.
            if (Vector3.Distance(_cube.position, _lostPoint.position) < 3)
                DIContainer.AsSingle<Engine.IMakeFailed>().MakeFailed(50, "Obstacle");
        }
    }
}
