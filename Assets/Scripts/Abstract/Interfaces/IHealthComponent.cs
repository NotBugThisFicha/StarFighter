
using System;
using UnityEngine;

public interface IHealthComponent
{
    public event Action HpEmpty;
    public bool IsHitPointsExists();
    public void TakeDamage(int damage);
}
