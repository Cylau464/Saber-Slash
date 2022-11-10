using UnityEngine;
using UnityEngine.UI;

namespace loading
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField] private Slider _loadingBar;

        private AsyncOperation _loadingScene;

        protected void Start()
        {
            _loadingBar.value = 0f;
            _loadingScene = GameScenes.LoadMainScene();
        }

        private void Update()
        {
            _loadingBar.value = _loadingScene.progress;
        }
    }
}
