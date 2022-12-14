using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private Vector3 _axis;
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Rotate(_axis, _speed * Time.deltaTime);
    }
}