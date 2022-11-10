using BzKovSoft.ObjectSlicer;
using CameraExtensions;
using System;
using System.Diagnostics;
using UnityEngine;

namespace BzKovSoft.ObjectSlicerSamples
{
    public class ObjectSlicerBySword : BzSliceableObjectBase
	{
		[HideInInspector] public Vector3 SwordPosition { get; set; }
		[SerializeField] private SwordSliceableAsync _swordSlicer;

		private Plane _plane;

		protected override BzSliceTryData PrepareData(Plane plane)
		{
			// remember some data. Later we could use it after the slice is done.
			// here I add Stopwatch object to see how much time it takes
			// and vertex count to display.
			ResultData addData = new ResultData();

			// count vertices
			var filters = GetComponentsInChildren<MeshFilter>();
			for (int i = 0; i < filters.Length; i++)
			{
				addData.vertexCount += filters[i].sharedMesh.vertexCount;
			}

			// remember start time
			addData.stopwatch = Stopwatch.StartNew();

			// colliders that will be participating in slicing
			var colliders = gameObject.GetComponentsInChildren<Collider>();
			_plane = plane;

			// return data
			return new BzSliceTryData()
			{
				// componentManager: this class will manage components on sliced objects
				componentManager = new StaticComponentManager(gameObject, plane, colliders),
				plane = plane,
				addData = addData,
			};
		}

		protected override void OnSliceFinished(BzSliceTryResult result)
		{
			if (!result.sliced)
            {
				_swordSlicer.TrySliceAgain();
				return;
            }

			if (_cameraShakeOnSlice == true)
				CameraShake.Shake(_noise, _cameraShakeSettings);

			Vibration.VibratePeek();

			// on sliced, get data that we saved in 'PrepareData' method
			var addData = (ResultData)result.addData;
			addData.stopwatch.Stop();
			drawText += gameObject.name +
				". VertCount: " + addData.vertexCount.ToString() + ". ms: " +
				addData.stopwatch.ElapsedMilliseconds.ToString() + Environment.NewLine;

			if (drawText.Length > 1500) // prevent very long text
				drawText = drawText.Substring(drawText.Length - 1000, 1000);

			float randomRotationForce = UnityEngine.Random.Range(_minRotationForce, _maxRotationForce);
			Vector3 rotateNormal = new Vector3(_plane.normal.z, _plane.normal.x, Mathf.Abs(_plane.normal.y));

			if (result.outObjectNeg.TryGetComponent(out Rigidbody outObjectNegRigidbody))
			{
				if (outObjectNegRigidbody.gameObject.tag == _unpinRigibodiesAfterSliceTag)
					outObjectNegRigidbody.transform.parent = null;

				outObjectNegRigidbody.useGravity = true;
				outObjectNegRigidbody.isKinematic = false;
				outObjectNegRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
				outObjectNegRigidbody.AddForce(SwordPosition * _sliceForwardForce + -_plane.normal * _sliceSideForce, ForceMode.Impulse);
				outObjectNegRigidbody.AddTorque(rotateNormal * randomRotationForce, ForceMode.VelocityChange);

				if (_sliceParticlePrefab != null)
				{
					Transform negTransform = outObjectNegRigidbody.transform;// result.outObjectNeg.transform;
					Instantiate(_sliceParticlePrefab, negTransform.position, negTransform.rotation, outObjectNegRigidbody.transform);
				}
			}

			randomRotationForce = UnityEngine.Random.Range(_minRotationForce, _maxRotationForce);

			if (result.outObjectPos.TryGetComponent(out Rigidbody outObjectPosRigidbody))
			{
				if (outObjectPosRigidbody.gameObject.tag == _unpinRigibodiesAfterSliceTag)
					outObjectPosRigidbody.transform.parent = null;

				outObjectPosRigidbody.useGravity = true;
				outObjectPosRigidbody.isKinematic = false;
				outObjectPosRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
				outObjectPosRigidbody.AddForce(SwordPosition * _sliceForwardForce + _plane.normal * _sliceSideForce, ForceMode.Impulse);
				outObjectPosRigidbody.AddTorque(-rotateNormal * randomRotationForce, ForceMode.VelocityChange);

				if (_sliceParticlePrefab != null)
				{
					Transform posTransform = outObjectPosRigidbody.transform;// result.outObjectPos.transform;
					Instantiate(_sliceParticlePrefab, posTransform.position, posTransform.rotation, outObjectPosRigidbody.transform);
				}
			}

			//var particle = Instantiate(_sparksParticlePrefab, _plane.ClosestPointOnPlane(outObjectPosRigidbody.position), Quaternion.identity);
			//particle.transform.right = _plane.normal;

			OnFinishSlice?.Invoke();
		}

		static string drawText = "-";

		//void OnGUI()
		//{
		//	GUI.Label(new Rect(10, 10, 2000, 2000), drawText);
		//}

		// DTO that we pass to slicer and then receive back
		class ResultData
		{
			public int vertexCount;
			public Stopwatch stopwatch;
		}
	}
}
