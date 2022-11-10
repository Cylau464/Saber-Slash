using UnityEngine;
using System;
using System.IO;

namespace Engine.Data
{
    [Serializable]
    public class FieldKey<T> : IField<T>
    {
        public static string FilePath(string fileName = "")
        {
            string directoryPath = Application.persistentDataPath + "/data/";

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            return directoryPath + fileName + ".json";
        }

        [SerializeField, HideInInspector] protected bool m_AutoSave;
        [SerializeField, HideInInspector] protected string m_FileName;

        [SerializeField] protected string m_Key;
        [SerializeField] protected T m_Value;

        private bool IsLoaded;

        public string Key => m_Key;
        public string fileName => m_FileName;
        public bool hasValue => ES3.KeyExists(m_Key, FilePath(m_FileName));

        public FieldKey(string key, string fileName, T value = default(T), bool autoSave = true)
        {
            this.IsLoaded = false;
            this.m_AutoSave = autoSave;

            this.m_Key = key;
            this.m_Value = value;

            this.m_FileName = fileName ?? throw new ArgumentNullException("The path file has a null value!.");
        }

        public virtual T value
        {
            get
            {
                if (IsLoaded == true) return m_Value;

                IsLoaded = true;
                return m_Value = ES3.Load(m_Key, FilePath(m_FileName), m_Value);
            }
            set
            {
                if (m_Value == null || !m_Value.Equals(value))
                {
                    m_Value = value;
                    if (m_AutoSave == true) Save();
                }
            }
        }

        public void Save()
        {
            ES3.Save(m_Key, m_Value, FilePath(m_FileName));
        }
    }
}