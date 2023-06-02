
using ShootEmUp;
using System.Collections.Generic;
using Zenject;

internal class BulletTracker: IFixedTickable
{
    private readonly IBulletSpawner _bulletSpawner;
    private readonly LevelBounds _bounds;
    public BulletTracker(IBulletSpawner bulletSpawner, LevelBounds bounds) {
        _bulletSpawner = bulletSpawner;
        _bounds = bounds;
    }

    public void FixedTick()
    {
        List<Bullet> activBullets = _bulletSpawner.Bullets;
        var i = activBullets.Count;
        while(i-- > 0)
        {
            var bullet = activBullets[i];
            if (!_bounds.InBounds(bullet.transform.position))
                _bulletSpawner.DespawnBullet(bullet);
        }
    }
}
