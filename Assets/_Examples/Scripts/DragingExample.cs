using Engine.Input;
using Engine.Physic;
using UnityEngine;

namespace Examples
{
    public class DragingExample : MonoBehaviour, IBeginDrag, IEndDrag, IDrag, IClick
    {
        public Camera _camera;
        public GameObject _cube;
        public CrossRay _crossRay;

        // Start is called before the first frame update
        protected void OnEnable()
        {
            _crossRay = new CrossRay(_camera, NormalAxis.Horizontal, 0);

            InputEvents.BeginDrag.Subscribe(this);
            InputEvents.Drag.Subscribe(this);
            InputEvents.EndDrag.Subscribe(this);

            InputEvents.Click.Subscribe(this);
        }

        protected void OnDisable()
        {
            InputEvents.BeginDrag.Unsubscribe(this);
            InputEvents.Drag.Unsubscribe(this);
            InputEvents.EndDrag.Unsubscribe(this);

            InputEvents.Click.Unsubscribe(this);
        }

        public void OnBeginDrag(InputInfo data)
        {
            _cube?.SetActive(true);
        }

        public void OnDrag(InputInfo data)
        {
            _crossRay.ThrowRaycast();

            if (_crossRay.isReached == true)
                _cube.transform.position = _crossRay.point;
        }

        public void OnEndDrag(InputInfo data)
        {
            _cube.SetActive(false);
        }

        public void OnClick(InputInfo data)
        {
            Debug.Log("Is clicked...");
        }
    }
}
