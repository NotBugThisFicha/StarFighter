
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

internal class EnemySpawner: SpawnerAbstract, IInitializable, IDisposable
{
    private readonly float _spawnCoolDown;
    private readonly Enemy.Factory _enemyFactory;
    private readonly AsyncProccesor _asyncProccesor;
    private readonly EnemyConfigs _enemyConfigs;
    private readonly SignalBus _signalBus;
    private readonly List<Enemy> _enemys = new List<Enemy>();

    public EnemySpawner(
        TransformContainers trContainers, 
        Enemy.Factory factory,
        AsyncProccesor asyncProccesor,
        EnemyConfigs enemyConfigsMono,
        SignalBus signalBus,
        float coolDown) : base(trContainers)
    {
        _enemyFactory = factory;
        _asyncProccesor = asyncProccesor;
        _enemyConfigs = enemyConfigsMono;
        _signalBus = signalBus;
        _spawnCoolDown = coolDown;
    }
    public void Initialize()
    {
        _asyncProccesor.StartCoroutine(SpawnEnemiesCor(_spawnCoolDown));
        _signalBus.Subscribe<OnEnemyDieEvent>(DespawnEnemy);
    }

    private IEnumerator SpawnEnemiesCor(float coolDown)
    {
        while (true)
        {
            var spawnPos = _enemyConfigs.enemyPositions.RandomSpawnPosition();
            var enemy = _enemyFactory.Create(spawnPos.position, _enemyConfigs.character);
            enemy.transform.SetParent(_trContainers.WorldContainer);
            var attackPos = _enemyConfigs.enemyPositions.RandomAttackPosition();
            enemy.SetDestination(attackPos.position);
            AddDisposable(enemy);
            _enemys.Add(enemy);
            yield return new WaitForSeconds(coolDown);
        }
    }

    private void DespawnEnemy(OnEnemyDieEvent dieEvent)
    {
        dieEvent.enemy.transform.SetParent(_trContainers.EnemyPoolContainer);
        _enemys.Remove(dieEvent.enemy);
        DespawnDisposable(dieEvent.enemy);
    }

    public void Dispose()=> _signalBus.Unsubscribe<OnEnemyDieEvent>(DespawnEnemy);
}
