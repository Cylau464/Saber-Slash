using UnityEngine;
using Engine.Events;

namespace Examples
{
    public interface IExecuteExample
    {
        void Execute();
    }

    public class ExecuteExample : IExecuteExample
    {
        public void Execute()
        {
            Debug.Log("Is Executed...");
        }
    }

    public class InterfaceEventExample : MonoBehaviour
    {
        public InterfaceEvent<IExecuteExample> executions = new InterfaceEvent<IExecuteExample>();

        private void OnEnable()
        {
            executions.Subscribe(new ExecuteExample());
        }

        private void OnDisable()
        {
            executions.Unsubscribe(new ExecuteExample());
        }

        [NaughtyAttributes.Button("Execute")]
        public void Execute()
        {
            executions.Invoke(item => item.Execute());
        }
    }
}