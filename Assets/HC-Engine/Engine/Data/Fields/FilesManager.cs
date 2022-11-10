using System.IO;
using UnityEngine;

namespace Engine.Data
{
    public static class FilesManager
    {
        private static bool m_HasDirectory = false;
        public static string pathDirectory
        {
            get
            {
                if (!m_HasDirectory)
                {
                    if (!Directory.Exists(Application.persistentDataPath + "/data/"))
                    {
                        Directory.CreateDirectory(Application.persistentDataPath + "/data/");
                    }

                    m_HasDirectory = true;
                }

                return Application.persistentDataPath + "/data/";
            }
        }
    }
}