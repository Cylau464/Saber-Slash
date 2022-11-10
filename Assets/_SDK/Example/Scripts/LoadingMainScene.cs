using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
#if UNITY_IOS
using apps.ios;
#endif

public class LoadingMainScene : MonoBehaviour
{
    private void Start()
    {
        apps.AppsIntegration.Initialize();

#if UNITY_ANDROID
    SceneManager.LoadScene(1);
#elif UNITY_IOS
        StartCoroutine(LoadMainSceneAsn());
#endif
    }

#if UNITY_IOS          
    private IEnumerator LoadMainSceneAsn()
    {
        while(IOSInitialize.IsInitialized)
            yield return null;

        LoadScene();
    }
#endif

    private void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

}
