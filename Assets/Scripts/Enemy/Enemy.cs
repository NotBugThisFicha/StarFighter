
using Assets.Scripts.Signals;
using ShootEmUp;
using System;
using UnityEngine;
using Zenject;

[RequireComponent(
    typeof(MoveComponent), 
    typeof(WeaponComponent))]
[RequireComponent(typeof(DamageComponent))]
internal class Enemy: CollidedObject, IPoolable<Vector2, Transform, IMemoryPool>,IDisposable
{
    public class Factory : PlaceholderFactory<Vector2, Transform, Enemy> { }
    private IMoveComponent _moveComponent;
    private IWeaponComponent _weaponComponent;

    private IMemoryPool _pool;

    [Inject]
    private void Construct(SignalBus signalBus, IBulletSpawner factory)
    {
        TryGetComponent(out _moveComponent);
        TryGetComponent(out _weaponComponent);

        _weaponComponent.SetBulletSpawner(factory);
        _signalBus = signalBus;
    }

    private void FixedUpdate()
    {
        if (_moveComponent.IsReached && _healthComponent.IsHitPointsExists())
            _weaponComponent.RateFire();
    }
    protected override void OnDie() => _signalBus.TryFire(new OnEnemyDieEvent { enemy = this });
    public void OnSpawned(Vector2 startPos, Transform targetEnemy, IMemoryPool pool)
    {
        _pool = pool;
        transform.position = startPos;
        _weaponComponent.SetTarget(targetEnemy);
    }
    public void SetDestination(Vector2 destination) => _moveComponent.SetDestination(destination);
    public void OnDespawned()=> _pool = null;
    public void Dispose()=> _pool.Despawn(this);
}
