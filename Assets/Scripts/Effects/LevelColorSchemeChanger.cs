using Main.Level;
using UnityEngine;
using Zenject;

namespace Effects
{
    public class LevelColorSchemeChanger : MonoBehaviour
    {
        [SerializeField] private ColorSchemePreset[] _presets;
        [SerializeField] private int _levelsToChange = 5;

        //[Inject] private ParticleSystemRenderer _fodParticle;
        [Inject] private Renderer _fogMesh;
        [Inject] private Light _directionalLight;

        private MaterialPropertyBlock _fogMeshPropertyBlock;
        //private MaterialPropertyBlock _fogParticlePropertyBlock;

        private void Start()
        {
            ILevelsData levelData = Engine.DI.DIContainer.AsSingle<ILevelsData>();

            int indexPreset = (levelData.playerLevel - 1) / _levelsToChange % _presets.Length;
            ColorSchemePreset preset = _presets[indexPreset];
            _fogMeshPropertyBlock = new MaterialPropertyBlock();
            _fogMesh.GetPropertyBlock(_fogMeshPropertyBlock);
            _fogMeshPropertyBlock.SetColor("_FogColor", preset.FogColor);
            _fogMesh.SetPropertyBlock(_fogMeshPropertyBlock);
            RenderSettings.skybox = preset.Skybox;
            RenderSettings.fogColor = preset.FogColor;
            _directionalLight.color = preset.LightColor;
            //_fogParticlePropertyBlock = new MaterialPropertyBlock();
            //_fodParticle.GetPropertyBlock(_fogParticlePropertyBlock);
            //_fogParticlePropertyBlock.SetColor("_Color", preset.FogColor);
            //_fodParticle.SetPropertyBlock(_fogParticlePropertyBlock);
        }
    }
}