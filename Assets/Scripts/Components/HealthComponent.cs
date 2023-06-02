using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HealthComponent : MonoBehaviour, IHealthComponent
    {
        public event Action HpEmpty;

        [SerializeField] private int hitPoints;
        private int hitPointsOrigin;

        private void OnEnable(){
            hitPointsOrigin = hitPoints;
        }
        public bool IsHitPointsExists() {
            return this.hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            this.hitPoints -= damage;
            if (this.hitPoints <= 0)
            {
                hitPoints = hitPointsOrigin;
                this.HpEmpty?.Invoke();
            }
        }
    }
}