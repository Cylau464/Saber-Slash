using UnityEngine;
using Zenject;

namespace Weapons.Enemy
{
    public class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private ParticleSystem _shotParticle;
        [SerializeField] private CFX_LightIntensityFade _particleLight;

        [Inject] private Bullet.Factory _bulletFactory;

        public void Prepare()
        {
            _shotParticle.Play();

            if (_particleLight.gameObject.activeSelf == true)
                _particleLight.gameObject.SetActive(false);

            _particleLight.gameObject.SetActive(true);
        }

        public void Shot(Transform target, int damage)
        {
            Bullet bullet = _bulletFactory.Create(_bulletPrefab);
            bullet.transform.position = _bulletSpawnPoint.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.Init(damage);
            Vector3 dir = (target.position - _bulletSpawnPoint.position).normalized;
            dir.y = 0f;
            bullet.Launch(dir);
        }
    }
}