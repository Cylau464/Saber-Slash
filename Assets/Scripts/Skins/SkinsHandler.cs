using Engine.DI;
using Main.Level;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Skins
{
    public class SkinsHandler : MonoBehaviour
    {
        [SerializeField] private int _progressPerLevel = 20;

        public SkinPreset UnlockingInProgress { get; private set; }
        public SkinPreset PickedSkin { get; private set; }

        private SkinsContainer _skinsContainer;
        private LevelsSettings _levelsSettings;

        [Inject] private GameManager _gameManager;

        public Action<SkinPreset> OnPicked { get; set; }

        [Inject]
        private void Init(SkinsContainer skinsContainer, LevelsSettings levelsSettings)
        {
            _skinsContainer = skinsContainer;
            _levelsSettings = levelsSettings;

            if (UnlockingInProgress == null)
                ChangeSkinForUnlock();

            PickedSkin = _skinsContainer.SkinPresets.FirstOrDefault(x => x.Data.value.Picked == true);

            if (PickedSkin == null)
            {
                PickedSkin = _skinsContainer.SkinPresets.FirstOrDefault(x => x.Data.value.Unlocked == true);
                PickedSkin.Pick();
            }
        }

        private void OnEnable()
        {
            _gameManager.OnCompleted += LevelCompleted;

            foreach (SkinPreset skin in _skinsContainer.SkinPresets)
            {
                skin.OnUnlocked += OnSkinUnlocked;
                skin.OnPicked += OnSkinPicked;
                skin.OnReset += OnSkinReset;
            }
        }

        private void OnDisable()
        {
            _gameManager.OnCompleted -= LevelCompleted;

            foreach (SkinPreset skin in _skinsContainer.SkinPresets)
            {
                skin.OnUnlocked -= OnSkinUnlocked;
                skin.OnPicked -= OnSkinPicked;
                skin.OnReset -= OnSkinReset;
            }
        }

        private void LevelCompleted()
        {
            UnlockingInProgress.AddProgress(_progressPerLevel);
        }

        public void ChangeSkinForUnlock()
        {
            SkinPreset lastUnlockedSkin = UnlockingInProgress;
            UnlockingInProgress = _skinsContainer.SkinPresets.FirstOrDefault(
                x => x.Data.value.Unlocked == false
                && x.Data.value.AvailableAfterLevel <= _levelsSettings.playerLevel.value
            );

            if (UnlockingInProgress == null)
            {
                int lastUnlockedSkinIndex = Array.IndexOf(_skinsContainer.SkinPresets, lastUnlockedSkin);

                if (lastUnlockedSkinIndex >= 0)
                {
                    do
                        lastUnlockedSkinIndex = (lastUnlockedSkinIndex + 1) % _skinsContainer.SkinPresets.Length;
                    while (_skinsContainer.SkinPresets[lastUnlockedSkinIndex].Data.value.AvailableAfterLevel > _levelsSettings.playerLevel.value);

                    UnlockingInProgress = _skinsContainer.SkinPresets[lastUnlockedSkinIndex];
                }
                else
                {
                    UnlockingInProgress = _skinsContainer.SkinPresets.FirstOrDefault(x => x.Data.value.Unlocked == true);
                }

                UnlockingInProgress.Lock();
            }
        }

        private void PickFirstSkin()
        {
            SkinPreset skin = _skinsContainer.SkinPresets.FirstOrDefault(x => x.Data.value.Unlocked == true);
            skin.Pick();
        }

        private void OnSkinUnlocked(SkinPreset skin)
        {
            ChangeSkinForUnlock();
        }

        private void OnSkinPicked(SkinPreset skin)
        {
            if (skin == PickedSkin) return;

            PickedSkin.Unpick();
            PickedSkin = skin;
            OnPicked?.Invoke(skin);
        }

        private void OnSkinReset(SkinPreset skin)
        {
            if (skin == PickedSkin)
                PickFirstSkin();
        }
    }
}