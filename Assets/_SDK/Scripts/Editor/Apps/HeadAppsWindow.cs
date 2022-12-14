using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

namespace apps.editor
{
    public static class HeadAppsWindow
    {
        [MenuItem("DSOneGames/Ping AppsSettings", false, 101)]
        public static void AppSettingsPing()
        {
            AppsUtility.PingAsset(AppsUtility.GetAsset<AppsSettings>(AppsSettings.globalDirectoryPath, "AppsSettings.asset"));
        }

        public static void SaveAllActiveScenes()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                EditorSceneManager.SaveScene(SceneManager.GetSceneAt(i));
            }
        }
    }
}