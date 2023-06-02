
using ShootEmUp;
using System;
using System.Collections.Generic;
using Zenject;
using Bullet = ShootEmUp.Bullet;

public sealed class BulletSpawner: SpawnerAbstract, IInitializable, IDisposable, IBulletSpawner
{
    private readonly Bullet.Factory _bulletFactory;
    private readonly SignalBus _signalBus;

    private readonly List<Bullet> bullets = new List<Bullet>();

    public List<Bullet> Bullets => bullets;

    public BulletSpawner(
        Bullet.Factory factory, 
        TransformContainers trContainers,
        SignalBus signalBus) : base(trContainers)
    {
        _signalBus = signalBus;
        _bulletFactory = factory;
    }

    public void Initialize() => _signalBus.Subscribe<OnBulletDieEvent>(DespawnBullet);
    public void Dispose() => _signalBus.Unsubscribe<OnBulletDieEvent>(DespawnBullet);

    public void SpawnBulletByArgs(BulletArgs args)
    {
        var bullet = _bulletFactory.Create(args);
        bullet.transform.SetParent(_trContainers.WorldContainer);
        AddDisposable(bullet);
        bullets.Add(bullet);
    }

    private void DespawnBullet(OnBulletDieEvent onBulletDie)
    {
        Bullet bullet = onBulletDie.bullet;
        DespawnBullet(bullet);
    }

    public void DespawnBullet(Bullet bullet)
    {
        bullet.transform.SetParent(_trContainers.BulletPoolContainer);
        bullets.Remove(bullet);
        DespawnDisposable(bullet);
    }
}
