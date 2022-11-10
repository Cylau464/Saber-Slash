using UnityEngine;

namespace Helpers
{
    public class TextureScaler : MonoBehaviour
    {
        private enum Placement { Center, LeftBorder, RightBorder }

        [SerializeField] private Renderer _renderer;
        [SerializeField] private Placement _placement;
        [SerializeField] private float _targetWidth = 5f;

        private MaterialPropertyBlock _propertyBlock;

        private void Start()
        {
            Scale();
        }

        private void Scale()
        {
            Vector2 tiling = Vector2.one;
            Vector2 offset = Vector2.zero;

            tiling.x = _renderer.transform.localScale.x / _targetWidth;
            tiling.y = _renderer.transform.localScale.z / _targetWidth;

            switch(_placement)
            {
                case Placement.Center:
                    if(_renderer.transform.localScale.x % _targetWidth != 0f)
                        offset.x = 0.5f;
                    break;
                case Placement.RightBorder:
                    offset.x = 1f - tiling.x;
                    break;
            }

            _propertyBlock = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetVector("_BaseMap_ST", new Vector4(tiling.x, tiling.y, offset.x, offset.y));
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}