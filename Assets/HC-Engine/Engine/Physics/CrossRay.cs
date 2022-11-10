using UnityEngine;

namespace Engine.Physic
{
    public enum NormalAxis { Vertical, Horizontal }

    [System.Serializable]
    public class CrossRay
    {
        [SerializeField] private UnityEngine.Camera m_Camera;

        [SerializeField] private float m_Offset;
        [SerializeField] private NormalAxis m_NormalAxis;

        [SerializeField] private Plane m_Plane;

        public bool isReached { get; private set; }
        public Vector3 point { get; private set; }

        public CrossRay(UnityEngine.Camera camera, NormalAxis normalAxis = NormalAxis.Horizontal , float offset = 0.0f)
        {
            if (camera == null) throw new System.ArgumentNullException();

            m_Camera = camera;
            this.m_Offset = offset;
            this.m_NormalAxis = normalAxis;

            switch (this.m_NormalAxis)
            {
                case NormalAxis.Vertical:
                    m_Plane = new Plane(Vector3.forward, -Vector3.forward * this.m_Offset);
                    break;
                case NormalAxis.Horizontal:
                    m_Plane = new Plane(Vector3.up, Vector3.up * this.m_Offset);
                    break;
            }
        }

        /// <summary>
        /// Get cross point on plane.
        /// </summary>
        public void ThrowRaycast()
        {
            ThrowRaycast(UnityEngine.Input.mousePosition);
        }

        /// <summary>
        /// Get cross point on plane.
        /// </summary>
        public void ThrowRaycast(Vector3 castPosition)
        {
            isReached = false;

            Ray ray = m_Camera.ScreenPointToRay(castPosition);
            if (m_Plane.Raycast(ray, out float enter))
            {
                isReached = true;
                point = ray.GetPoint(enter);
            }
        }

        /// <summary>
        /// Get cross point on plane.
        /// </summary>
        /// <returns> True if it's raycasted. </returns>
        public bool ThrowRaycast(Vector3 castPosition, out Vector3 point)
        {
            isReached = false;

            Ray ray = m_Camera.ScreenPointToRay(castPosition);
            if (m_Plane.Raycast(ray, out float enter))
            {
                isReached = true;
                this.point = point = ray.GetPoint(enter);
                return true;
            }

            point = Vector3.zero;
            return false;
        }

        public void OnDrawGizmosSelected()
        {
            switch (m_NormalAxis)
            {
                case NormalAxis.Vertical:
                    Gizmos.color = Color.yellow;
                    Vector3 center = -Vector3.forward * m_Offset;
                    Gizmos.DrawLine(center + new Vector3(20, 20, 0), center + new Vector3(20, -20, 0));
                    Gizmos.DrawLine(center + new Vector3(20, -20, 0), center + new Vector3(-20, -20, 0));
                    Gizmos.DrawLine(center + new Vector3(-20, -20, 0), center + new Vector3(-20, 20, 0));
                    Gizmos.DrawLine(center + new Vector3(-20, 20, 0), center + new Vector3(20, 20, 0));
                    break;
                case NormalAxis.Horizontal:
                    Gizmos.color = Color.yellow;
                    center = Vector3.up * m_Offset;
                    Gizmos.DrawLine(center + new Vector3(20, 0, 20), center + new Vector3(20, 0, -20));
                    Gizmos.DrawLine(center + new Vector3(20, 0, -20), center + new Vector3(-20, 0, -20));
                    Gizmos.DrawLine(center + new Vector3(-20, 0, -20), center + new Vector3(-20, 0, 20));
                    Gizmos.DrawLine(center + new Vector3(-20, 0, 20), center + new Vector3(20, 0, 20));
                    break;
            }
        }
    }
}
