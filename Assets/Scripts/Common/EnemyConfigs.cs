
using ShootEmUp;
using System;
using UnityEngine;

[Serializable]
internal class EnemyConfigs
{
    [Header("Spawn")]
    public EnemyPositions enemyPositions;
    public Transform character;
}
