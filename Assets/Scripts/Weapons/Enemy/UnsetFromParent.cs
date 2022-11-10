using BzKovSoft.ObjectSlicer;
using UnityEngine;

namespace Weapons.Enemy
{
    public class UnsetFromParent : MonoBehaviour
    {
        [SerializeField] private BzSliceableBase _sliceable;

        private void OnEnable()
        {
            _sliceable.OnStartSlice += Unset;
        }

        private void OnDisable()
        {
            _sliceable.OnStartSlice -= Unset;
        }

        private void Unset()
        {
            transform.SetParent(null, true);
        }
    }
}