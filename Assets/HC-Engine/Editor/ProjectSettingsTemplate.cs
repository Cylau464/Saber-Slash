using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Engine.Attribute;

namespace editor
{
    [InitializeOnLoad]
    sealed class InitializePin : Editor
    {
        [InitializeOnLoadMethod]
        public static void Initialize()
        {
            ProjectSettingsTemplate.tempSettings?.Clear();

            foreach (var type in ReflectionUtility.GetTypesWithAttribute<TemplateSettingsAttribute>())
            {
                object[] attributes = type.GetCustomAttributes(typeof(TemplateSettingsAttribute), true);

                foreach (var attribute in attributes)
                {
                    TemplateSettingsAttribute templateSettings = attribute as TemplateSettingsAttribute;

                    if (templateSettings == null) continue;

                    ProjectSettingsTemplate.tempSettings.Add(new ProjectSettingsTemplate.SettingsInfo
                        (
                            templateSettings,
                            AssetUtility.GetOrCreateAsset(type, templateSettings.path, templateSettings.name + ".asset"))
                        );
                }
            }
        }
    }

    public static class ProjectSettingsTemplate
    {
        internal class SettingsInfo
        {
            internal TemplateSettingsAttribute attribute;
            internal ScriptableObject scriptableObject;
            internal SerializedObject serializedObject;

            internal SettingsInfo(TemplateSettingsAttribute attribute, ScriptableObject scriptableObject)
            {
                this.attribute = attribute;
                this.scriptableObject = scriptableObject;
                this.serializedObject = new SerializedObject(scriptableObject);
            }
        }

        internal static List<SettingsInfo> tempSettings = new List<SettingsInfo>();

        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            var provider = new SettingsProvider("Project/Template/Settings", SettingsScope.Project)
            {
                label = "Settings",

                guiHandler = (searchContext) =>
                {
                    foreach (var settings in tempSettings)
                    {
                        if (settings.scriptableObject == null)
                            continue;

                        settings.serializedObject.UpdateIfRequiredOrScript();

                        if (GUILayout.Button(settings.attribute.name, GUIHeadStyle()))
                            settings.scriptableObject.PingObject();

                        using (var iterator = settings.serializedObject.GetIterator())
                        {
                            if (iterator.NextVisible(true))
                            {
                                do
                                {
                                    if (!iterator.name.Equals("m_Script"))
                                        EditorGUILayout.PropertyField(settings.serializedObject.FindProperty(iterator.name));
                                }
                                while (iterator.NextVisible(false));
                            }
                        }

                        EditorGUILayout.Space(30);
                        settings.serializedObject.ApplyModifiedProperties();
                    }
                },

                keywords = new HashSet<string>(new[] { "Template", "Settings" })
            };

            return provider;
        }

        private static GUIStyle GUIHeadStyle()
        {
            GUIStyle headButton = new GUIStyle(GUI.skin.button);
            headButton.fontStyle = FontStyle.Bold;

            return headButton;
        }
    }
}