using System.Collections;
using System.Collections.Generic;

namespace Engine.DI
{
    public class InjectorDependencies : IEnumerable
    {
        private List<IDependency> m_Dependencies;

        public IDependency this[int index] => m_Dependencies[index];

        public int Count => m_Dependencies.Count;

        public InjectorDependencies()
        {
            m_Dependencies = new List<IDependency>();
        }

        public InjectorDependencies(IEnumerable<IDependency> dependencies)
        {
            if (dependencies == null) throw new System.ArgumentNullException("The dependencies has a null value!.");

            m_Dependencies = new List<IDependency>(dependencies);
        }

        public InjectorDependencies(IEnumerable<UnityEngine.Object> dependencies)
        {
            if (dependencies == null) throw new System.ArgumentNullException("The dependencies has a null value!.");

            m_Dependencies = new List<IDependency>();
            foreach (UnityEngine.Object obj in dependencies)
            {
                IDependency dependency = obj as IDependency;
                if (dependency != null) m_Dependencies.Add(dependency);
            }
        }

        public void InjectAll()
        {
            foreach (IDependency dependency in m_Dependencies)
            {
                if (dependency != null && !dependency.Equals(null)) dependency.Inject();
            }
        }

        public void AddDependency(IDependency dependency)
        {
            m_Dependencies.Add(dependency);
        }

        public void AddDependencies(IEnumerable<IDependency> dependency)
        {
            m_Dependencies.AddRange(dependency);
        }

        public IEnumerator GetEnumerator()
        {
            return m_Dependencies.GetEnumerator();
        }
    }
}