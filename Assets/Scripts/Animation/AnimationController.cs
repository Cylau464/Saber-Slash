using UnityEngine;

namespace Animation
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;

        private void Awake()
        {
            InitializeParams();
        }

        protected virtual void InitializeParams()
        {

        }
    }
}