using Physic;
using UnityEngine.UI;

namespace Engine.Input
{
    public enum MouseStatue { Non = -1, Idle = 0, Down = 1, Up = 2 }

    public static class ControllerInputs
    {
        public static bool s_EnableInputs { get; set; }

        private static GraphicRaycaster s_GraphicRaycaster;

        internal static void Initialize(GraphicRaycaster defaultGraphicRaycaster, bool enableInputs)
        {
            s_GraphicRaycaster = defaultGraphicRaycaster;

            s_EnableInputs = enableInputs;
        }

        public static MouseStatue GetMouseState()
        {
            if (s_EnableInputs && UnityEngine.Input.GetMouseButtonDown(0))
                return MouseStatue.Down;
            else
            if (s_EnableInputs && UnityEngine.Input.GetMouseButtonUp(0))
                return MouseStatue.Up;
            else
            if (s_EnableInputs && UnityEngine.Input.GetMouseButton(0))
                return MouseStatue.Idle;
            else
                return MouseStatue.Non;
        }

        public static bool OnMouse()
        {
            return s_EnableInputs && (UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.GetMouseButton(0) || UnityEngine.Input.GetMouseButtonUp(0));
        }

        public static bool OnMouse(MouseStatue mouseType)
        {
            switch (mouseType)
            {
                case MouseStatue.Down:
                    return s_EnableInputs && UnityEngine.Input.GetMouseButtonDown(0);
                case MouseStatue.Idle:
                    return s_EnableInputs && UnityEngine.Input.GetMouseButton(0);
                case MouseStatue.Up:
                    return s_EnableInputs && UnityEngine.Input.GetMouseButtonUp(0);
            }

            return false;
        }

        public static bool OnMouse(bool passThroughUI = false, GraphicRaycaster graphicRaycaster = null)
        {
            if (graphicRaycaster == null) graphicRaycaster = s_GraphicRaycaster;

            if (graphicRaycaster == null) throw new System.ArgumentNullException();

            return s_EnableInputs && (UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.GetMouseButton(0) || UnityEngine.Input.GetMouseButtonUp(0)) && (passThroughUI || !IsRaycastedUI(graphicRaycaster));
        }

        public static bool OnMouse(MouseStatue mouseType, bool passThroughUI = false, GraphicRaycaster graphicRaycaster = null)
        {
            if (graphicRaycaster == null) graphicRaycaster = s_GraphicRaycaster;
            if (graphicRaycaster == null) throw new System.ArgumentNullException();

            switch (mouseType)
            {
                case MouseStatue.Down:
                    return s_EnableInputs && UnityEngine.Input.GetMouseButtonDown(0) && (passThroughUI || !IsRaycastedUI(graphicRaycaster));
                case MouseStatue.Idle:
                    return s_EnableInputs && UnityEngine.Input.GetMouseButton(0);
                case MouseStatue.Up:
                    return s_EnableInputs && UnityEngine.Input.GetMouseButtonUp(0);
            }

            return false;
        }

        public static bool IsRaycastedUI(GraphicRaycaster graphicRaycaster = null)
        {
            if (graphicRaycaster == null) graphicRaycaster = s_GraphicRaycaster;

            if (graphicRaycaster == null) throw new System.ArgumentNullException();

            return RaycastHits.GetRaycastResults(graphicRaycaster).Count != 0;
        }
    }
}