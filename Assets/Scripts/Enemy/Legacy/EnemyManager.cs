using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private EnemyPool _enemyPool;

        [SerializeField]
        private CollisionSystem _bulletSystem;
        
        private readonly HashSet<GameObject> m_activeEnemies = new();

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                var enemy = this._enemyPool.SpawnEnemy();
                if (enemy != null)
                {
                    if (this.m_activeEnemies.Add(enemy))
                    {
                        //enemy.GetComponent<HealthComponent>().hpEmpty += this.OnDestroyed;
                        //enemy.GetComponent<EnemyAttackAgent>().OnFire += this.OnFire;
                    }    
                }
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (m_activeEnemies.Remove(enemy))
            {
                //enemy.GetComponent<HealthComponent>().hpEmpty -= this.OnDestroyed;
                //enemy.GetComponent<EnemyAttackAgent>().OnFire -= this.OnFire;

                _enemyPool.UnspawnEnemy(enemy);
            }
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            //_bulletSystem.FlyBulletByArgs(new BulletArgs
            //{
            //    isPlayer = false,
            //    physicsLayer = (int) PhysicsLayer.ENEMY,
            //    color = Color.red,
            //    damage = 1,
            //    position = position,
            //    velocity = direction * 2.0f
            //});
        }
    }
}