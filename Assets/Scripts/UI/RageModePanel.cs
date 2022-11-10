using Main.UI;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class RageModePanel : Panel
    {
        [SerializeField] private TMP_Text _ammoCounter;

        [Inject] private RageMode _rageMode;

        private void OnEnable()
        {
            _rageMode.OnActivated += Show;
            _rageMode.OnDeactivated += Hide;
        }

        private void OnDisable()
        {
            _rageMode.OnActivated -= Show;
            _rageMode.OnDeactivated -= Hide;
        }
    }
}