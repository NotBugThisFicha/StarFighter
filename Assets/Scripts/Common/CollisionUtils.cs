using UnityEngine;

namespace ShootEmUp
{
    internal static class CollisionUtils
    {
        internal static void DealDamage(GameObject first, GameObject second)
        {
            if (!first.TryGetComponent(out ITeamComponent team1) | 
                !second.TryGetComponent(out ITeamComponent team2)|
                     team1.IsPlayer == team2.IsPlayer            |
                !first.TryGetComponent(out IDamage damageble))   return;

            if (second.TryGetComponent(out IHealthComponent hitPoints))
                hitPoints.TakeDamage(damageble.Damage);
        }
    }
}