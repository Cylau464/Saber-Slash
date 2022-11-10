using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data
{
    [Serializable]
    public class FieldArray<T>
    {
        [Serializable]
        private struct Element : IComparable<Element>, IEquatable<Element>
        {
            [SerializeField] private int m_ID;
            [SerializeField] private FieldKey<T> m_Field;

            public int id => m_ID;
            public FieldKey<T> field => m_Field;

            public Element(int key, FieldKey<T> value)
            {
                this.m_ID = key;
                this.m_Field = value;
            }

            public int CompareTo(Element other)
			{
				return this.id.CompareTo(other.id);
			}

            public bool Equals(Element other)
            {
                return id == other.id;
            }

            public override int GetHashCode()
            {
                int hashCode = 1363396886;
                hashCode = hashCode * -1521134295 + id.GetHashCode();
                hashCode = hashCode * -1521134295 + EqualityComparer<FieldKey<T>>.Default.GetHashCode(field);
                return hashCode;
            }
        }

        [SerializeField, HideInInspector] private bool m_AutoSave;
        [SerializeField] private string m_Key;
        [SerializeField] private string m_FileName;
        [SerializeField] private List<Element> m_Elements;

        public string Key => m_Key;
        public string FileName => m_FileName;

        public FieldArray(string key, string fileName = null, int capacity = 0, bool autoSave = true)
        {
            m_Key = key;
            m_AutoSave = autoSave;
            m_FileName = fileName;

            m_Elements = new List<Element>(capacity);

            for (int i = 0; i < capacity; i++)
            {
                m_Elements.Add(new Element(i, new FieldKey<T>(key + i, fileName, default(T), autoSave)));
            }
        }

        public T this[int index]
        {
            get
            {
                return FindElement(index).value;
            }
            set
            {
                InsertSortedElement(index, value);
            }
        }

        public FieldKey<T> GetFieldKey(int index)
        {
            return FindElement(index);
        }

        private void InsertSortedElement(int index, T insertValue = default(T))
        {
            int startI = 0;
            int endI = m_Elements.Count;

            while (endI > startI)
            {
                int windowSize = endI - startI;
                int middleI = startI + (windowSize / 2);
                if (m_Elements[middleI].id == index)
                {
                    m_Elements[middleI].field.value = insertValue;
                    return;
                }
                else if (m_Elements[middleI].id < index)
                {
                    startI = middleI + 1;
                }
                else
                {
                    endI = middleI;
                }
            }

            m_Elements.Insert(startI, new Element(index, new FieldKey<T>((m_Key + index), m_FileName, insertValue, m_AutoSave)));
        }

        private FieldKey<T> FindElement(int index)
        {
            int startI = 0;
            int endI = m_Elements.Count;
            while (endI > startI)
            {
                int windowSize = endI - startI;
                int middleI = startI + (windowSize / 2);
                if (m_Elements[middleI].id == index)
                {
                    return m_Elements[middleI].field;
                }
                else
                if (m_Elements[middleI].id < index)
                {
                    startI = middleI + 1;
                }
                else
                {
                    endI = middleI;
                }
            }

            FieldKey<T> result = new FieldKey<T>((m_Key + index), m_FileName, default(T), m_AutoSave);
            m_Elements.Insert(startI, new Element(index, result));
            return result;
        }

        public void Save()
        {
            for (int i = 0; i < m_Elements.Count; i++)
            {
                m_Elements[i].field.Save();
            }
        }
    }
}