using UnityEditor;

namespace editor.pin
{
    [System.Serializable]
    public class PinInfo
    {
        public string nickName;
        public string path;

        public PinInfo(string nickName, string path)
        {
            this.nickName = nickName;
            this.path = path;
        }

        public PinInfo(UnityEngine.Object obj)
        {
            this.nickName = obj.name;
            this.path = AssetDatabase.GetAssetPath(obj);
        }
    }
}
