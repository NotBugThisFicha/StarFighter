using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class EnemyPositions
    {
        [SerializeField]
        private Transform[] spawnPositions;

        [SerializeField]
        private Transform[] attackPositions;

        public Transform RandomSpawnPosition()
        {
            return this.RandomTransform(this.spawnPositions);
        }

        public Transform RandomAttackPosition()
        {
            return this.RandomTransform(this.attackPositions);
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            var index = UnityEngine.Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}