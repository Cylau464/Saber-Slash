using System.Collections;
using UnityEngine;

namespace Effects
{
    public class EmissionFlicker : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private int _materialIndex;
        [Space]
        [SerializeField] private AnimationCurve _flickerCurve;
        [SerializeField] private float _flickerInterval = 1f;
        [SerializeField, ColorUsage(true, true)] private Color _minColor;
        [SerializeField, ColorUsage(true, true)] private Color _maxColor;

        private MaterialPropertyBlock _propertyBlock;

        private const string EMISSION_COLOR_PROPERTY = "_EmissionColor";

        private void Start()
        {
            _propertyBlock = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(_propertyBlock, _materialIndex);
        }

        private void Update()
        {
            _propertyBlock.SetColor(EMISSION_COLOR_PROPERTY, Color.Lerp(_minColor, _maxColor, _flickerCurve.Evaluate(Mathf.Repeat(Time.time, 1f))));
            _renderer.SetPropertyBlock(_propertyBlock, _materialIndex);
        }
    }
}