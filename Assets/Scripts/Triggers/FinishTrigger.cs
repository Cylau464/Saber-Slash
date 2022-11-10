using UnityEngine;
using Zenject;

namespace Triggers
{
    public class FinishTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayer;

        [Inject] private GameManager _gameManager;

        private void OnTriggerEnter(Collider other)
        {
            if ((1 << other.gameObject.layer & _playerLayer) != 0)
            {
                _gameManager.MakeCompleted();
            }
        }
    }
}
