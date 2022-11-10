#if UNITY_EDITOR
using Engine;
using UnityEditor;
using UnityEngine;

namespace editor
{
    [InitializeOnLoad]
    public class EditorMode
    {
        static EditorMode()
        {
            EditorApplication.playModeStateChanged += ModeChanged;

            AddDefineSymbolSDK(BuildTargetGroup.Android);
            AddDefineSymbolSDK(BuildTargetGroup.iOS);
            AddDefineSymbolSDK(BuildTargetGroup.Standalone);
        }

        static void AddDefineSymbolSDK(BuildTargetGroup targetGroup)
        {
            string supports = "Support_Template";
            string result = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);

            if (!result.Contains(supports))
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, result + ";" + supports);
                Debug.Log("Support_SDK is added!!");
            }
        }

        /// <summary>
        /// When player Start play mode in editor. This function will execute.
        /// </summary>
        [InitializeOnEnterPlayMode]
        public static void OnEnteredPlayMode(EnterPlayModeOptions options)
        {
            IAwake[] scriptables = EditorManager.FindAllAssetsOfType<IAwake>();
            foreach (var item in scriptables)
            {
                if (item != null)
                    item.Awake();
            }
        }

        private static void ModeChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.ExitingPlayMode:
                    IDestroy[] Destroy = EditorManager.FindAllAssetsOfType<IDestroy>();
                    foreach (var item in Destroy)
                    {
                        if (item != null)
                            item.OnDestroy();
                    }
                    break;
            }

        }
    }
}
#endif