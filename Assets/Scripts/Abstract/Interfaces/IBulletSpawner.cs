
using ShootEmUp;
using System.Collections.Generic;

public interface IBulletSpawner
{
    public List<Bullet> Bullets { get; }
    public void SpawnBulletByArgs(BulletArgs args);
    public void DespawnBullet(Bullet bullet);
}
