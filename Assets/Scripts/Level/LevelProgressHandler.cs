using Dreamteck.Splines;
using UI;
using UnityEngine;
using Zenject;

namespace Main.Level
{
    public class LevelProgressHandler : MonoBehaviour
    {
        [Inject] private LevelProgressSlider _levelProgressSlider;
        [Inject] private SplineFollower _splineFollower;

        private void Update()
        {
            _levelProgressSlider.SetProgress(((float)_splineFollower.clampedPercent));
        }
    }
}
