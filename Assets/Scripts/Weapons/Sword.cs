using System;
using System.Collections.Generic;
using UnityEngine;

namespace BzKovSoft.ObjectSlicerSamples
{
	/// <summary>
	/// The script must be attached to a GameObject that have collider marked as a "IsTrigger".
	/// </summary>
	public class Sword : MonoBehaviour
	{
		public int SliceID { get; private set; }
		private Vector3 _prevPos;
		private Vector3 _pos;
		private List<SwordSliceableAsync> _impaledObjects = new List<SwordSliceableAsync>();

		[SerializeField] private Transform _blade;
		[SerializeField]
		private Vector3 _origin = Vector3.down;

		[SerializeField]
		private Vector3 _direction = Vector3.up;

		private void Update()
		{
			_prevPos = _pos;
			_pos = transform.position;
		}

		public Vector3 Origin
		{
			get
			{
				Vector3 localShifted = transform.InverseTransformPoint(transform.position) + _origin;
				return transform.TransformPoint(localShifted);
			}
		}

		public Vector3 BladeDirection { get { return _blade.rotation * _direction.normalized; } }
		public Vector3 MoveDirection { get { return (_pos - _prevPos).normalized; } }

		public void BeginNewSlice()
		{
			SliceID = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
		}
    }
}
