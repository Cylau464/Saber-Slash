using Engine.utility;
using System.Collections.Generic;
using UnityEditor;

namespace editor.pin
{
    public static class PinListInfo
    {
        public static List<PinInfo> s_PinInfo = new List<PinInfo>();

        private static readonly string s_PinCountKey = "PIN_COUNT_KEY";
        private static readonly string s_PinItemKey = "PIN_ITEM_KEY";

        private static PinSettings m_Settings;
        internal static PinSettings settings
        {
            get
            {
                if (m_Settings == null) m_Settings = PinSettings.GetPinSettings() ?? throw new System.NullReferenceException("Pin Settings is not exists");

                return m_Settings;
            }
        }

        internal static int Count
        {
            get { return EditorPrefs.GetInt(s_PinCountKey, 0); }
            set { EditorPrefs.SetInt(s_PinCountKey, value); }
        }

        public static void OnInitializeProject()
        {
            LoadPaths();

            settings.OnPinInfoUpdated(ToArrayPinInfo());
        }

        internal static void LoadPaths()
        {
            int count = Count;
            if (count == 0) return;

            for (int i = 0; i < count; i++)
            {
                if (EditorPrefs.HasKey(s_PinItemKey + i))
                {
                    PinInfo newInfo = new PinInfo("", "");
                    newInfo.JsonToObject(EditorPrefs.GetString(s_PinItemKey + i));
                    s_PinInfo.Add(newInfo);
                }
            }
        }

        internal static void UpdatePinsInfo(PinInfo[] infos)
        {
            Count = infos.Length;
            s_PinInfo = new List<PinInfo>(infos);

            for (int i = 0; i < s_PinInfo.Count; i++)
            {
                EditorPrefs.SetString(s_PinItemKey + i, JsonConverter.ObjectToJson(s_PinInfo[i]));
            }
        }

        internal static void Save()
        {
            Count = s_PinInfo.Count;

            for (int i = 0; i < s_PinInfo.Count; i++)
            {
                EditorPrefs.SetString(s_PinItemKey + i, JsonConverter.ObjectToJson(s_PinInfo[i]));
            }
        }

        public static bool AddPinObject(UnityEngine.Object obj)
        {
            if (s_PinInfo.Exists((path) =>
            {
                if (path.path == AssetDatabase.GetAssetPath(obj)) return true;
                return false;
            })) return false;

            s_PinInfo.Add(new PinInfo(obj));

            settings.OnPinInfoUpdated(ToArrayPinInfo());

            Save();
            return true;
        }

        public static void RemovePinObject(UnityEngine.Object obj)
        {
            s_PinInfo.RemoveAt(s_PinInfo.FindIndex((path) =>
            {
                if (path.path == AssetDatabase.GetAssetPath(obj)) return true;
                return false;
            }));

            settings.OnPinInfoUpdated(ToArrayPinInfo());

            Save();
        }

        public static void RemoveAllPinObjects()
        {
            s_PinInfo.Clear();

            settings.OnPinInfoUpdated(new PinInfo[0]);

            int count = Count + 20;
            for (int i = 0; i < count; i++)
            {
                if (EditorPrefs.HasKey(s_PinItemKey + i))
                    EditorPrefs.DeleteKey(s_PinItemKey + i);
            }

            if (EditorPrefs.HasKey(s_PinCountKey))
                EditorPrefs.DeleteKey(s_PinCountKey);
        }

        public static UnityEngine.Object GetPinObject(int index)
        {
            if ((uint)Count <= (uint)index) return null;
            return AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(s_PinInfo[index].path);
        }

        public static string GetNickName(int index)
        {
            if ((uint)Count <= (uint)index) return null;
            return s_PinInfo[index].nickName;
        }

        public static void DebugAllPins()
        {
            foreach (PinInfo path in s_PinInfo)
            {
                UnityEngine.Debug.Log(path.path);
            }
        }

        internal static PinInfo[] ToArrayPinInfo()
        {
            return s_PinInfo.ToArray();
        }
    }
}