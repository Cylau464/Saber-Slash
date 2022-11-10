using UnityEngine;
using System.Collections;
using UnityEditor;

namespace IronSourceAnalyticsSDK.Editor
{
    [CustomEditor(typeof(IronSourceAnalytics))]
    public class IronSourceAnalyticsInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.indentLevel = 1;
            EditorGUILayout.Space();

            GUILayout.BeginHorizontal();

            GUILayout.Label("IronSourceAnalytics Object", EditorStyles.largeLabel);

            GUILayout.EndHorizontal();
        }
    }
}