
using ShootEmUp;
using System;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;

public struct PathHiearchy
{
    public const string pathBulletPool = "[GAME_SYSTEM]/BulletPool";
    public const string pathEnemyPool = "[GAME_SYSTEM]/EnemyPool";
}

[CreateAssetMenu(fileName = "FactoryInstaller", menuName ="Installers/Factory")]
internal class FactorysInstaller: ScriptableObjectInstaller<FactorysInstaller>
{
    [SerializeField] private int initialSizePoolBullet;
    [SerializeField] private int initialSizePoolEnemy;

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Bullet bulletPrefab;
    public override void InstallBindings()
    {
        Container
            .BindFactory<BulletArgs, Bullet, Bullet.Factory>()
            .FromMonoPoolableMemoryPool(x => 
            x.WithInitialSize(initialSizePoolBullet)
            .FromComponentInNewPrefab(bulletPrefab)
            .UnderTransformGroup(PathHiearchy.pathBulletPool));

        Container
          .BindFactory<Vector2, Transform, Enemy, Enemy.Factory>()
          .FromMonoPoolableMemoryPool(x =>
          x.WithInitialSize(initialSizePoolEnemy)
          .FromComponentInNewPrefab(enemyPrefab)
          .UnderTransformGroup(PathHiearchy.pathEnemyPool));

    }
}
