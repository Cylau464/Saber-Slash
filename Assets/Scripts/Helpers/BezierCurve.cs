using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Helpers
{
    public struct BezierCurve
    {
        public Vector3 startPosition;
        public Vector3 endPosition;
        public float height;

        public Vector3 heightPosition => centerPosition + Vector3.up * height;
        public Vector3 centerPosition => Vector3.Lerp(startPosition, endPosition, 0.5f);
        public Vector3 GetCurvePosition(float t) => Vector3.Lerp(Vector3.Lerp(startPosition, heightPosition, t), Vector3.Lerp(heightPosition, endPosition, t), t);
    }
}