
using Assets.Scripts.Signals;
using ShootEmUp;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(HealthComponent))]
public abstract class CollidedObject: MonoBehaviour
{
    protected SignalBus _signalBus;
    protected IHealthComponent _healthComponent;
    protected virtual void Awake(){
        TryGetComponent(out _healthComponent);
    }

    protected virtual void OnEnable() => _healthComponent.HpEmpty += OnDie;
    protected virtual void OnDisable() => _healthComponent.HpEmpty -= OnDie;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _signalBus.TryFire(new CollisionEvent
        {
            first = this.gameObject,
            second = collision.gameObject
        });
    }

    protected abstract void OnDie();
}
