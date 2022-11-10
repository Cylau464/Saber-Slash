using BzKovSoft.ObjectSlicer;
using System.Collections;
using UnityEngine;

namespace Effects
{
    public class Dissolve : MonoBehaviour
    {
        [SerializeField] private Renderer[] _renderers;
        [SerializeField] private BzSliceableBase _sliceable;
        [SerializeField] private float _dissolveTime = .5f;

        [HideInInspector] public bool Activated;

        private MaterialPropertyBlock _propertyBlock;

        private const string DISSOLVE_ENABLED_PROPERTY = "_DissolveEnabled";
        private const string DISSOLVE_PROGRESS_PROPERTY = "_DissolveProgress";

        public void Awake()
        {
            if (Activated == false)
            {
                _sliceable.OnStartSlice += Activate;
                enabled = false;
            }
        }

        private void OnDestroy()
        {
            _sliceable.OnStartSlice -= Activate;
        }

        private void OnEnable()
        {
            _propertyBlock = new MaterialPropertyBlock();

            foreach (Renderer renderer in _renderers)
            {
                renderer.GetPropertyBlock(_propertyBlock);
                _propertyBlock.SetFloat(DISSOLVE_ENABLED_PROPERTY, 1f);
                renderer.SetPropertyBlock(_propertyBlock);
            }
            
            StartCoroutine(Dissolving());
        }

        private void Activate()
        {
            enabled = true;
            Activated = true;
        }

        private IEnumerator Dissolving()
        {
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime / _dissolveTime;
                _propertyBlock.SetFloat(DISSOLVE_PROGRESS_PROPERTY, t);

                foreach (Renderer renderer in _renderers)
                    renderer.SetPropertyBlock(_propertyBlock);

                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}