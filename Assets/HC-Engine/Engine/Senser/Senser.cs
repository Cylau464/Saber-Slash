using Engine.Data;
using Engine.Events;
using UnityEngine;

namespace Engine.Senser
{
    public enum SenserType { Audio, Vibration }

    [System.Serializable]
    public class Senser : ISenser, DI.IDependency, IAwake
    {
        public Event<ISenserUpdated> OnProductUpdated { get; private set; } = new Event<ISenserUpdated>();

        [SerializeField] private SenserInfo _info;

        private SenserType m_Type;
        private FieldKey<int> m_IsEnable;

        public bool isEnable => m_IsEnable.value == 1;
        public SenserType type => m_Type;

        public void Inject()
        {
            DI.DIContainer.Register<ISenser>(this);
        }

        public void Awake()
        {
            m_Type = _info.type;
            m_IsEnable = _info.isEnable;
        }

        /// <summary>
        /// Switch the Senser enable if the Senser was false you can switch it to true and opposite.
        /// </summary>
        public void SwitchEnable()
        {
            m_IsEnable.value = (m_IsEnable.value == 1) ? 0 : 1;
            OnProductUpdated.Events.Invoke((senser) => senser.OnSenserUpdated(m_IsEnable.value == 1));
        }

        public void SetEnable(bool enable)
        {
            if (enable != (m_IsEnable.value == 1))
            {
                m_IsEnable.value = (enable) ? 1 : 0;
                OnProductUpdated.Events.Invoke((senser) => senser.OnSenserUpdated(enable));
            }
        }
    }
}
