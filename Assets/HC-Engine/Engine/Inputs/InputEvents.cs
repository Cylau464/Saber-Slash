using Engine.Events;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Input
{
    public struct InputInfo
    {
        public Vector3 lastPosition;
        public Vector3 initPoint;

        public Vector3 currentPosition => UnityEngine.Input.mousePosition;
        public Vector3 daltaDrag => currentPosition - initPoint;

        public float lengthDrag => Vector3.Distance(currentPosition, initPoint);
        public Vector3 lastDaltaDrag => UnityEngine.Input.mousePosition - lastPosition;
    }

    [Serializable]
    public static class InputEvents
    {
        public static Event<IClick> Click { get; internal set; } = new Event<IClick>();
        public static Event<IBeginDrag> BeginDrag { get; internal set; } = new Event<IBeginDrag>();
        public static Event<IDrag> Drag { get; internal set; } = new Event<IDrag>();
        public static Event<IEndDrag> EndDrag { get; internal set; } = new Event<IEndDrag>();

        private static bool s_PassThroughUI = true;
        private static float s_SmoothDrag = 0;

        private static GraphicRaycaster s_GraphicRaycaster;

        public static bool isClicked { get; private set; }
        public static bool isDragging { get; private set; }

        private static InputInfo s_DragingData;
        private static bool s_IsClickedDown;
        private static bool s_IsInited = false;

        internal static void Initialize(GraphicRaycaster graphicRaycaster, bool passThroughUI, float smoothDrag)
        {
            Click.CleanNulls();
            BeginDrag.CleanNulls();
            Drag.CleanNulls();
            EndDrag.CleanNulls();

            s_PassThroughUI = passThroughUI;
            s_SmoothDrag = smoothDrag;

            s_GraphicRaycaster = graphicRaycaster;

            // Init data
            s_IsClickedDown = false;
            isClicked = false;
            isDragging = false;
            s_DragingData = new InputInfo();

            // is inited
            s_IsInited = true;
        }

        internal static void Update()
        {
            if (s_IsInited == false || !ControllerInputs.s_EnableInputs)
            {
                if (s_IsClickedDown && ControllerInputs.OnMouse(MouseStatue.Up))
                {
                    s_IsClickedDown = false;
                    OnMouseUp();
                }
                return;
            }

            isClicked = false;
            if (ControllerInputs.OnMouse(MouseStatue.Down, s_PassThroughUI))
            {
                s_IsClickedDown = true;
                OnMouseDown();
            }
            else
            if (s_IsClickedDown && ControllerInputs.OnMouse(MouseStatue.Up))
            {
                s_IsClickedDown = false;
                OnMouseUp();
            }
            else
            if (s_IsClickedDown && ControllerInputs.OnMouse(MouseStatue.Idle))
                OnMouse();

        }

        private static void OnMouseDown()
        {
            s_DragingData.lastPosition = s_DragingData.initPoint = UnityEngine.Input.mousePosition;
            isDragging = false;
        }

        private static void OnMouseUp()
        {
            if (isDragging)
            {
                InvokeEndDrag(s_DragingData);
            }
            else
            if (!ControllerInputs.IsRaycastedUI(s_GraphicRaycaster))
            {
                isClicked = true;

                InvokeClick(s_DragingData);
            }
            isDragging = false;
        }

        private static void OnMouse()
        {
            /// On drag
            if (s_SmoothDrag < Vector3.Distance(s_DragingData.lastPosition, UnityEngine.Input.mousePosition))
            {
                if (!isDragging)
                {
                    InvokeBeginDrag(s_DragingData);
                    isDragging = true;
                }

                InvokeDrag(s_DragingData);

                s_DragingData.lastPosition = UnityEngine.Input.mousePosition;
            }
        }

        private static void InvokeClick(InputInfo data)
        {
            Click.Events.Invoke(onClick => onClick?.OnClick(data));
        }

        private static void InvokeBeginDrag(InputInfo data)
        {
            BeginDrag.Events.Invoke(beginDrag => beginDrag?.OnBeginDrag(data));
        }

        private static void InvokeDrag(InputInfo data)
        {
            Drag.Events.Invoke(drag => drag?.OnDrag(data));
        }

        private static void InvokeEndDrag(InputInfo data)
        {
            EndDrag.Events.Invoke(endDrag => endDrag?.OnEndDrag(data));
        }
    }
}
