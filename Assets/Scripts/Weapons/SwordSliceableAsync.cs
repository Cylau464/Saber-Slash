using BzKovSoft.CharacterSlicerSamples;
using BzKovSoft.ObjectSlicer;
using System;
using System.Collections;
using UnityEngine;

namespace BzKovSoft.ObjectSlicerSamples
{
    /// <summary>
    /// This script will invoke slice method of IBzSliceableAsync interface if knife slices this GameObject.
    /// The script must be attached to a GameObject that have rigidbody on it and
    /// IBzSliceable implementation in one of its parent.
    /// </summary>
    [DisallowMultipleComponent]
	public class SwordSliceableAsync : MonoBehaviour
	{
		[SerializeField] private Renderer _renderer;
		public Renderer Renderer { set => _renderer = value; }
		[SerializeField, Range(0f, 1f)] private float _depthThreshold = 0f; // In percent
		public float DepthThreshold { set => _depthThreshold = value; }

		IBzSliceableAsync _sliceableAsync;

		private Sword _sword;
		private Plane _plane;
		private int _sliceID;

		[Zenject.Inject] private Transform _plane2;

		private void Start()
		{
			_sliceableAsync = GetComponentInParent<IBzSliceableAsync>();
		}

		private void OnTriggerEnter(Collider other)
		{
			var knife = other.gameObject.GetComponentInParent<Sword>();

			if (knife == null)
				return;

			_sword = knife;
			_plane = GetPlane(knife);
			_sliceID = _sword.SliceID;
			StartCoroutine(Slice(knife));
		}

		public void TrySliceAgain()
        {
			if (_sword == null)
				return;

			_plane = GetPlane(_sword);
			_sliceID = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			StartCoroutine(Slice(_sword));
        }

		private IEnumerator Slice(Sword knife)
		{
			// The call from OnTriggerEnter, so some object positions are wrong.
			// We have to wait for next frame to work with correct values
			yield return null;
			
			if (_sliceableAsync != null)
			{
				if (_sliceableAsync is ObjectSlicerBySword)
                {
                    (_sliceableAsync as ObjectSlicerBySword).SwordPosition = knife.transform.forward;
					_sliceableAsync.Slice(_plane, _sliceID, null);
                }
                else if (_sliceableAsync is CharacterSlicerBySword)
                {
					CharacterSlicerBySword characterSliceableAsync = _sliceableAsync as CharacterSlicerBySword;
					characterSliceableAsync.SwordForward = knife.transform.forward;
					characterSliceableAsync.Slice(_plane, _sliceID, null, this);
                }

			}
		}

		private Plane GetPlane(Sword knife)
        {
			Vector3 point = GetCollisionPoint(knife);
			Vector3 normal = knife.BladeDirection;// Vector3.Cross(knife.MoveDirection, knife.BladeDirection);

			if (_plane2 != null)
            {
				_plane2.position = point;
				_plane2.forward = normal;

            }
			return new Plane(normal, point);
		}

		private Plane GetPlane(Sword knife, Vector3 normal)
        {
			Vector3 point = GetCollisionPoint(knife);
			return new Plane(normal, point);
		}

		private Vector3 GetCollisionPoint(Sword knife)
		{
			if (_renderer == null)
            {
				Debug.LogWarning("Skinned Mesh Renderer is null");
				return knife.Origin;
            }

			Quaternion rotation = transform.rotation;
			transform.rotation = Quaternion.identity;
			Vector3 size = _renderer.bounds.size;
			Vector3 center = _renderer.bounds.center;
			float averageSize = (size.x / 3 + size.y / 3 + size.z / 3) * .5f * (1f - _depthThreshold);
			transform.rotation = rotation;
			Vector3 distToObject = transform.position - knife.Origin;
			Vector3 proj = Vector3.Project(distToObject, knife.transform.forward);//knife.BladeDirection);
			Vector3 collisionPoint = knife.Origin + proj;

            if (Vector3.Distance(transform.position, collisionPoint) > averageSize)
            {
				collisionPoint = center + (collisionPoint - transform.position).normalized * averageSize;
            }

            //Debug.Break();
            return collisionPoint;
		}
	}
}