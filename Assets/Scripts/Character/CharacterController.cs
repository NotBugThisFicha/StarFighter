using Assets.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [RequireComponent( 
        typeof(WeaponComponent),
        typeof(MoveComponent))]
    [RequireComponent(typeof(DamageComponent))]
    public sealed class CharacterController : CollidedObject
    {
        private IWeaponComponent _weaponComponent;
        private IMoveComponent _moveComponent;
        private IInputManager _inputManager;

        [Inject]
        private void Construct(
            IInputManager inputManager,
            SignalBus signalBus,
            IBulletSpawner factory)
        {
            TryGetComponent(out _weaponComponent);
            TryGetComponent(out _moveComponent);

            _weaponComponent.SetBulletSpawner(factory);
            _inputManager = inputManager;
            _signalBus = signalBus;
        }

        protected override void OnEnable() {
            _inputManager.FireButtonClickEvent += OnFlyBullet;
            base.OnEnable();
        }

        protected override void OnDisable() {
            _inputManager.FireButtonClickEvent -= OnFlyBullet;
            base.OnDisable();
        }
        protected override void OnDie()=>
            _signalBus.TryFire(new FinishGameEvent());

        private void FixedUpdate()=>
            _moveComponent.MoveByRigidbodyVelocity(new Vector2(_inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);

        private void OnFlyBullet()=>
            _weaponComponent.FlyBulletByDirection(_weaponComponent.Rotation * Vector2.up);
    }
}