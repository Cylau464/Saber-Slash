#if UNITY_EDITOR
using Engine;
using UnityEditor;
using UnityEngine;

namespace editor
{
    public class HeadTemplateEditor
    {
        [MenuItem("DSOneGames/Reset Data", false, 0)]
        public static void ResetAllData()
        {
            PlayerPrefs.DeleteAll();
            Data.ObjectSaver.ClearAllFiles();

            IResetData[] reseters = EditorManager.FindAllAssetsOfType<IResetData>();

            int idData = 0;
            foreach (IResetData reset in reseters)
            {
                idData++;
                reset.ResetData(idData);
            }

            EditorManager.SaveGame();
            Debug.Log("Reset Data assets is finished!...");
        }

        [MenuItem("DSOneGames/Unlock Data", false, 0)]
        public static void UnlockAllData()
        {
            PlayerPrefs.DeleteAll();
            Data.ObjectSaver.ClearAllFiles();

            IUnlock[] unlocks = EditorManager.FindAllAssetsOfType<IUnlock>();

            foreach (IUnlock unlock in unlocks)
            {
                unlock.Unlock();
            }

            EditorManager.SaveGame();
            Debug.Log("Reset Data assets is finished!...");
        }

        [MenuItem("DSOneGames/Validate Settings", false, 0)]
        public static void ValidateAll()
        {
            IValidate[] validates = EditorManager.FindAllAssetsOfType<IValidate>();
            for (int i = 0; i < validates.Length; i++)
            {
                validates[i].Validate();
            }

            EditorManager.SaveGame();

            Debug.Log("Validate assets is finished!...");
        }

        [MenuItem("DSOneGames/Save Assets", false, 150)]
        public static void SaveAssets()
        {
            IAsset[] validates = EditorManager.FindAllAssetsOfType<IAsset>();
            for (int i = 0; i < validates.Length; i++)
            {
                if (validates[i] != null) EditorUtility.SetDirty(validates[i] as Object);
            }

            EditorManager.SaveGame();

            for (int i = 0; i < validates.Length; i++)
            {
                if (validates[i] != null) EditorUtility.ClearDirty(validates[i] as Object);
            }

            Debug.Log("Save assets is finished!...");
        }
    }
}
#endif