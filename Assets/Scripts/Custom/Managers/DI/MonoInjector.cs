using Engine.DI;
using Engine.Store;
using Engine.Senser;
using Engine.Money;
using Main.Level;
using System.Collections.Generic;
using UnityEngine;
using Engine.Camera;
using System.Linq;
using Engine;

namespace Main.DI
{
    public class MonoInjector : MonoBehaviour
    {
        [SerializeField] public Store[] m_Stores;
        [SerializeField] public Senser[] m_Sensers;
        [SerializeField] public MoneyManager[] m_MoneyManager;
        [SerializeField] public LevelsData[] m_LevelsData;
        [SerializeField] public LevelGroupsManager[] m_LevelGroupsManager;

        [Header("Other Dependencies")]
        [SerializeField] private Object[] m_Dependencies;

        private void Awake()
        {
            /// Inject All Dependencies.
            InjectorDependencies injector = new InjectorDependencies(m_Dependencies);

            injector.AddDependencies(m_Stores);
            injector.AddDependencies(m_Sensers);
            injector.AddDependencies(m_MoneyManager);
            injector.AddDependencies(m_LevelsData);
            injector.AddDependencies(m_LevelGroupsManager);
            injector.AddDependency(new VirtualCamerasManager());

            injector.InjectAll();

            /// Awake All Dependencies.
            foreach (IAwake awake in injector.OfType<IAwake>())
            {
                awake.Awake();
            }
        }

#if UNITY_EDITOR
        [NaughtyAttributes.Button("Reload Dependencies")]
        public void ReloadDependencies()
        {
            List<Object> allObjects = new List<Object>();

            allObjects.AddRange(editor.AssetUtility.FindScribtableObjectsOfType<ScriptableObject>());
            allObjects.AddRange(FindObjectsOfType(typeof(Object)));

            List<Object> objects = new List<Object>();
            foreach (Object obj in allObjects)
            {
                if (obj as IDependency != null) objects.Add(obj);
            }

            m_Dependencies = objects.ToArray();
        }
#endif
    }
}