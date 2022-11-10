using Triggers;
using UnityEngine;

namespace BzKovSoft.ObjectSlicerSamples
{
	public class AdderSwordSliceableAsync : MonoBehaviour
	{
		[SerializeField] private Renderer _renderer;
		[SerializeField] private ParentCollisionHandler _parentCollisionHandler;
		[SerializeField, Range(0f, 1f)] private float _depthThreshold;

		private void Start()
		{
			var rigids = GetComponentsInChildren<Rigidbody>();

			for (int i = 0; i < rigids.Length; i++)
			{
				var rigid = rigids[i];
				var go = rigid.gameObject;

				if (go == gameObject)
					continue;

				if (go.GetComponent<SwordSliceableAsync>() != null)
					continue;

				SwordSliceableAsync sword = go.AddComponent<SwordSliceableAsync>();
				sword.Renderer = _renderer;
				sword.DepthThreshold = _depthThreshold;
				CollisionHandler handler = go.AddComponent<CollisionHandler>();
				_parentCollisionHandler.AddChildHandler(handler);
			}
		}
	}
}