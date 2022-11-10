using apps;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Main.UI
{
    public class PanelLose : Panel
    {
        [SerializeField] private Button _tryAgainButton;
        [SerializeField] private Button _rewardButton;
        [SerializeField] private GameObject _rewardLoadingIcon;
        [SerializeField] private GameObject _rewardIcon;
        [SerializeField] private Image _timeLeftImage;
        [Space]
        [SerializeField] private float _showTryAgainDelay = 2f;
        [SerializeField] private float _timeForRevive = 5f;

        private void Start()
        {
            _rewardButton.onClick.AddListener(ShowReward);
            _tryAgainButton.onClick.AddListener(ReloadScene);
        }

        private void OnEnable()
        {
            StartCoroutine(ReviveTimeLimit());
            _tryAgainButton.gameObject.SetActive(false);
            Invoke(nameof(ShowTryAgain), _showTryAgainDelay);

            if (ADSManager.IsRewardedLoaded == false)
            {
                _rewardIcon.SetActive(false);
                _rewardLoadingIcon.SetActive(true);
            }
            else
            {
                _rewardIcon.SetActive(true);
                _rewardLoadingIcon.SetActive(false);
            }
        }

        private void Update()
        {
            if (ADSManager.IsRewardedLoaded == false)
            {
                _rewardIcon.SetActive(false);
                _rewardLoadingIcon.SetActive(true);
            }
            else
            {
                _rewardIcon.SetActive(true);
                _rewardLoadingIcon.SetActive(false);
            }
        }

        private void ReloadScene()
        {
            GameScenes.ReloadScene();
        }

        private void ShowReward()
        {
            ADSManager.ShowRewardedVideo("revive", Revive);
        }

        private void Revive()
        {
            GameManager.Instance.MakeContinued();
        }

        private void ShowTryAgain()
        {
            _tryAgainButton.gameObject.SetActive(true);
        }

        private IEnumerator ReviveTimeLimit()
        {
            float t = 0f;
            _timeLeftImage.fillAmount = 1f;
            _rewardButton.interactable = true;

            while (t < 1f)
            {
                t += Time.unscaledDeltaTime / _timeForRevive;
                _timeLeftImage.fillAmount = 1f - t;

                yield return null;
            }

            _rewardButton.interactable = false;
        }
    }
}
