using UnityEngine;
using Zenject;

namespace Skins
{
    public class SkinChanger : MonoBehaviour
    {
        [SerializeField] private Renderer _bladeRenderer;
        [SerializeField] private Renderer _trailRenderer;
        [SerializeField] private Light _light;

        [Inject] private SkinsHandler _skinsHandler;

        private void OnEnable()
        {
            _skinsHandler.OnPicked += Change;
        }

        private void OnDisable()
        {
            _skinsHandler.OnPicked -= Change;
        }

        private void Start()
        {
            SkinPreset skin = _skinsHandler.PickedSkin;

            if (skin != null)
                Change(skin);
        }

        public void Change(SkinPreset skin)
        {
            _bladeRenderer.material = skin.BladeMaterial;
            _trailRenderer.material = skin.TrailMaterial;
            _light.color = skin.LightColor;
        }
    }
}