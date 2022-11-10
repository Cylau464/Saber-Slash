using Engine.Input;
using UnityEngine;

namespace Examples
{
    public class PhysicsExample : MonoBehaviour
    {
        private void Update()
        {
            if (ControllerInputs.OnMouse(MouseStatue.Down))
            {
                Debug.Log(Physic.RaycastHits.GetColliderHitedRaycast<BoxCollider>(Camera.main, UnityEngine.Input.mousePosition));
            }
        }
    }
}