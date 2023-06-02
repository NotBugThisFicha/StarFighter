using Assets.Scripts.Signals;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public struct BulletArgs
    {
        public Vector2 position;
        public Vector2 velocity;
        public Color color;
        public int physicsLayer;
        public int damage;
        public bool isPlayer;
    }
    public sealed class CollisionSystem: IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        public CollisionSystem(SignalBus signalBus){
            _signalBus = signalBus;
        }

        public void Initialize()=>
            _signalBus.Subscribe<CollisionEvent>(OnBulletCollision);

        public void Dispose()=>
            _signalBus.Unsubscribe<CollisionEvent>(OnBulletCollision);

        private void OnBulletCollision(CollisionEvent collisionEvent)=>
            CollisionUtils.DealDamage(collisionEvent.first, collisionEvent.second);
    }
}