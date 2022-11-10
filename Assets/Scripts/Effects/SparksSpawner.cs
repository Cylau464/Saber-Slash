using CameraExtensions;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Effects
{
    public class SparksSpawner : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _sparks;
        [SerializeField] private LayerMask _ignoredMask;
        [SerializeField, TagField] private string _ignoredTag = "WithoutSparks";

        [SerializeField] private int _onEnterParticles = 15;
        [SerializeField] private int _onStayParticles = 5;
        [SerializeField] private float _rayDistance = 2f;

        [Header("Vibration")]
        [SerializeField] private LayerMask _vibrationOnStayMask;
        [SerializeField] private CameraShakeSettings _cameraShakeSettings;

        [Header("Burn")]
        [SerializeField] private float _burnTextureSize = .5f;
        [SerializeField] private float _maxBurnTextureSize = 1f;

        private CinemachineBasicMultiChannelPerlin _noise;
        [Inject] private CinemachineVirtualCamera VirtualCamera { set => _noise = value.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>(); }

        private LayerMask _targetMask;
        private Vector3 _lastPosition;
        private Camera _camera;

        private void Start()
        {
            _targetMask = -1 & ~_ignoredMask;
            _lastPosition = transform.position;
            _camera = Camera.main;
        }

        private void Update()
        {
            Ray ray = new Ray(transform.position, transform.up);

            if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _targetMask) == true)
            {
                if ((1 << hit.collider.gameObject.layer & _vibrationOnStayMask) != 0)
                {
                    Vibration.VibrateShort();
                    CameraShake.Shake(_noise, _cameraShakeSettings, false);
                }

                if (hit.collider.TryGetComponent(out DrawOnTexture drawTexture) == true)
                {
                    float size = Mathf.Lerp(_maxBurnTextureSize, _burnTextureSize, hit.distance / _rayDistance);
                    float brushRotation = Vector3.SignedAngle((_lastPosition - transform.position).normalized, _camera.transform.forward, Vector3.up);
                    drawTexture.Draw(hit.textureCoord, size, brushRotation);
                }

                int particles = _onEnterParticles;

                if (hit.collider.CompareTag(_ignoredTag) == false)
                    Emit(hit.point, particles);
            }

            _lastPosition = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((1 << other.gameObject.layer & _ignoredMask) != 0
                || other.gameObject.tag == _ignoredTag)
                return;

            Ray ray = new Ray(transform.position, transform.up);

            if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _targetMask) == true)
                Emit(hit.point, _onEnterParticles);
            else
                Emit(other.ClosestPoint(transform.position), _onEnterParticles);
        }

            //private void OnTriggerStay(Collider other)
            //{
            //    if ((1 << other.gameObject.layer & _vibrationOnStayMask) != 0)
            //    {
            //        Vibration.VibrateShort();
            //        CameraShake.Shake(_noise, _cameraShakeSettings, false);
            //    }

            //    if ((1 << other.gameObject.layer & _ignoredMask) != 0
            //        || other.gameObject.tag == _ignoredTag)
            //        return;

            //    Ray ray = new Ray(transform.position, transform.up);
            //    DrawOnTexture drawOnTexture = other.GetComponent<DrawOnTexture>();

            //    if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _targetMask) == true)
            //    {
            //        Emit(hit.point, _onStayParticles);
            //        float size = Mathf.Lerp(_maxBurnTextureSize, _burnTextureSize, hit.distance / _rayDistance);
            //        //drawOnTexture?.RaycastDraw(ray, size);
            //    }
            //    else
            //    {
            //        Vector3 point = other.ClosestPoint(transform.position);
            //        Emit(point, _onStayParticles);
            //        //drawOnTexture?.DrawByPosition(point, _burnTextureSize);
            //    }
            //}

            private void Emit(Vector3 point, int count)
        {
            ParticleSystem.EmitParams param = new ParticleSystem.EmitParams();
            param.position = point;
            param.applyShapeToPosition = true;
            _sparks.Emit(param, count);
        }
    }
}