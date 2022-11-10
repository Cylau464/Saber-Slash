using UnityEngine;
using UnityEngine.Events;

public class AnimationEventsCaller : MonoBehaviour
{
    [SerializeField] private UnityEvent _action;

    private void Action()
    {
        _action.Invoke();
    }
}
