using UnityEditor;
using UnityEngine;

namespace editor.pin
{
    public class PinSceneView : Editor
    {
        private const int k_Padding = 5;

        private static bool s_IsInited = false;

        internal static void OnInitializeProject()
        {
            if (!s_IsInited)
            {
                SceneView.duringSceneGui += UpdateSceneView;
                s_IsInited = true;
            }
        }

        private static void UpdateSceneView(SceneView sceneView)
        {
            Handles.BeginGUI();

            int j = 0;
            for (int i = 0; i < PinListInfo.Count; i++)
            {
                string nickName = PinListInfo.GetNickName(i);
                Object obj = PinListInfo.GetPinObject(i);
                if (obj == null) continue;

                if (GUI.Button(
                    new Rect(
                    sceneView.camera.pixelWidth - PinListInfo.settings.m_ButtonScale.x - 15,
                    120 + ((k_Padding + PinListInfo.settings.m_ButtonScale.y) * j),
                    PinListInfo.settings.m_ButtonScale.x,
                    PinListInfo.settings.m_ButtonScale.y),
                    nickName, GetButtonStyle()))
                {
                    AssetUtility.PingObject(obj);
                }

                if (GUI.Button(new Rect(sceneView.camera.pixelWidth - PinListInfo.settings.m_ButtonScale.x - 40,
                    120 + ((k_Padding + PinListInfo.settings.m_ButtonScale.y) * j), 20f, 20f), "X", GetButtonStyle()))
                {
                    PinListInfo.RemovePinObject(obj);
                }
                j++;
            }

            Handles.EndGUI();
        }

        private static GUIStyle GetButtonStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.alignment = TextAnchor.MiddleLeft;
            return style;
        }
    }

}