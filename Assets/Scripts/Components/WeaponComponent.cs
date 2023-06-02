using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour, IWeaponComponent
    {
        public Vector2 Position => firePoint.position;
        public Quaternion Rotation => firePoint.rotation;

        [SerializeField] private Transform firePoint;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private int countDown;
        private IBulletSpawner _bulletSpawner;

        private float _currentTime;
        private Transform _target;

        private void Awake(){
            _currentTime = countDown;
        }

        public void SetBulletSpawner(IBulletSpawner factory) => _bulletSpawner = factory;
        public void FlyBulletByArgs(BulletArgs args) => _bulletSpawner.SpawnBulletByArgs(args);

        public void FlyBulletByDirection(Vector2 direction)
        {
            _bulletSpawner.SpawnBulletByArgs(new BulletArgs
            {
                isPlayer = _bulletConfig.IsPlayer,
                physicsLayer = (int)this._bulletConfig.physicsLayer,
                color = this._bulletConfig.color,
                damage = this._bulletConfig.damage,
                position = Position,
                velocity = direction * this._bulletConfig.speed
            });
        }

        public void RateFire()
        {
            _currentTime -= Time.fixedDeltaTime;
            if (_currentTime <= 0)
            {
                TargetFire();
                _currentTime += countDown;
            }
        }

        private void TargetFire()
        {
            if (!_target) return;

            var startPosition = Position;
            var vector = (Vector2)_target.transform.position - startPosition;
            var direction = vector.normalized;
            FlyBulletByDirection(direction);
        }

        public void SetTarget(Transform target) => _target = target;
    }
}