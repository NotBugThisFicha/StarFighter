using Assets.Scripts.Signals;
using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [RequireComponent(
        typeof(SpriteRenderer),
        typeof(MoveComponent))]
    [RequireComponent(
        typeof(DamageComponent),
        typeof(TeamComponent))]
    public class Bullet : CollidedObject, IPoolable<BulletArgs, IMemoryPool>, IDisposable
    {
        public class Factory : PlaceholderFactory<BulletArgs, Bullet> { }

        private SpriteRenderer spriteRenderer;
        private IDamage _damageComponent;
        private ITeamComponent _teamComponent;
        private IMoveComponent _moveComponent;

        private IMemoryPool _pool;

        [Inject]
        public void Construct(SignalBus signalBus){
            _signalBus = signalBus;
        }
        protected override void Awake()
        {
            TryGetComponent(out _moveComponent);
            TryGetComponent(out spriteRenderer);
            TryGetComponent(out _damageComponent);
            TryGetComponent(out _teamComponent);
            base.Awake();
        }

        protected override void OnDie() =>
            _signalBus.TryFire(new OnBulletDieEvent { bullet = this });

        private void SetPhysicsLayer(int physicsLayer)=>
            gameObject.layer = physicsLayer;

        private void SetPosition(Vector3 position)=>
            transform.position = position;

        private void SetColor(Color color)=>
            spriteRenderer.color = color;

        public void OnDespawned()=>
            _pool = null;

        public void OnSpawned(BulletArgs args, IMemoryPool pool)
        {
            _pool = pool;

            SetPhysicsLayer(args.physicsLayer);
            SetPosition(args.position);
            SetColor(args.color);

            _moveComponent.SetVelocity(args.velocity);
            _damageComponent.SetDamage(args.damage);
            _teamComponent.SetStatusPlayer(args.isPlayer);
        }

        public void Dispose()=>
            _pool.Despawn(this);
    }
}