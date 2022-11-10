using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Helpers
{
    public static class CameraHelper
    {
        public static bool CheckPointInCameraView(Vector3 position)
        {
            Vector3 viewpoint;
            viewpoint = Camera.main.WorldToViewportPoint(position);
            if (viewpoint.x < 0) return false;
            if (viewpoint.x > 1) return false;
            if (viewpoint.y < 0) return false;
            if (viewpoint.y > 1) return false;
            if (viewpoint.z <= 0) return false;
            return true;
        }
    }
}