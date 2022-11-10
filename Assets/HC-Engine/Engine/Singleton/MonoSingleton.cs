using UnityEngine;

namespace Engine
{

    [DefaultExecutionOrder(-80)]
    public class MonoSingleton<T> : MonoBehaviour, ISingleton where T : Object
    {
        public bool isInited => m_Instance != null;

        private static T m_Instance;
        public static T Instance
        {
            get
            {
                if (m_Instance != null) return m_Instance;

                m_Instance = FindObjectOfType<T>();

                if (m_Instance != null) return m_Instance;

                throw new Exceptions.ObjectNotFoundException();
            }
        }

        protected void InitializeSingleton()
        {
            m_Instance = (this as T) ?? throw new Exceptions.ObjectNotFoundException();
        }
    }
}