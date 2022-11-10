using Main.Level;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelProgressSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _levelNumberText;

        private void Start()
        {
            ILevelsData levelData = Engine.DI.DIContainer.AsSingle<ILevelsData>();
            _levelNumberText.text = levelData.playerLevel.ToString();
            _slider.value = 0f;
        }

        public void SetProgress(float value)
        {
            _slider.value = value;
        }
    }
}
