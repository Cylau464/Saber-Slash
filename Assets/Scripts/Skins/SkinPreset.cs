using Engine.Data;
using NaughtyAttributes;
using System;
using UnityEngine;

namespace Skins
{
    [Serializable]
    public struct Data
    {
        public bool Unlocked;
        public bool Picked;
        [AllowNesting, HideIf(nameof(Unlocked)), Range(0, 100)] public int UnlockProgress;
        public int AvailableAfterLevel;

        public Data(bool unlocked, int progress)
        {
            Unlocked = unlocked;
            UnlockProgress = progress;
            Picked = false;
            AvailableAfterLevel = 0;
        }
    }

    [CreateAssetMenu(fileName = "Skin Preset", menuName = "Add/Skins/Skin Preset")]
    public class SkinPreset : ScriptableObject
    {
        public Sprite Icon;
        public Material BladeMaterial;
        public Material TrailMaterial;
        public Color LightColor;

        [Space]
        public FieldKey<Data> Data;
        public FieldKey<Data> InitData;

        private bool _unlocked => Data.value.Unlocked;
        private bool _notPicked => !Data.value.Picked;

        public Action<SkinPreset> OnUnlocked { get; set; }
        public Action<SkinPreset> OnPicked { get; set; }
        public Action<SkinPreset> OnReset { get; set; }

        public void AddProgress(int progress)
        {
            Data data = Data.value;
            data.UnlockProgress = Mathf.Clamp(data.UnlockProgress + progress, 0, 100);
            Data.value = data;

            if (Data.value.UnlockProgress == 100)
                Unlock(true);
        }

        public void Unlock(bool pickAfterUnlock)
        {
            Data data = Data.value;

            if (data.UnlockProgress < 100)
                data.UnlockProgress = 100;

            data.Unlocked = true;
            Data.value = data;
            OnUnlocked?.Invoke(this);

            if (pickAfterUnlock == true)
                Pick();
        }

        [HideIf(nameof(_unlocked)), Button]
        public void Unlock()
        {
            Unlock(false);
        }

        [ShowIf(EConditionOperator.And, nameof(_unlocked), nameof(_notPicked)), Button]
        public void Pick()
        {
            if (Data.value.Unlocked == false || Data.value.Picked == true)
                return;

            Data data = Data.value;
            data.Picked = true;
            Data.value = data;
            OnPicked?.Invoke(this);
        }

        public void Unpick()
        {
            Data data = Data.value;
            data.Picked = false;
            Data.value = data;
        }

        [Button]
        public void Lock()
        {
            Data data = Data.value;
            data.Unlocked = false;
            data.Picked = false;
            data.UnlockProgress = 0;
            Data.value = data;
        }

        [Button]
        public void Reset()
        {
            Data.value = InitData.value;
            OnReset?.Invoke(this);
        }
    }
}