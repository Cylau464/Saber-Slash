using Animation;
using UnityEngine;
using Zenject;

namespace Effects
{
    public class ElectricAttack : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;

        [Inject] private EnemyAnimationController _animationController;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _animationController.OnAttacked += Attack;
        }

        private void OnDisable()
        {
            _animationController.OnAttacked -= Attack;
        }

        private void Attack()
        {
            _particle.transform.rotation = Quaternion.LookRotation(_particle.transform.position - _camera.transform.position);
            _particle.Play();
        }
    }
}