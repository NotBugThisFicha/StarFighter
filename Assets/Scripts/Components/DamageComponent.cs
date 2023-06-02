
using System.ComponentModel;
using UnityEngine;

public sealed class DamageComponent :MonoBehaviour, IDamage
{
    [SerializeField]
    private int _damage;
    public int Damage => _damage;
    public void SetDamage(int value) => _damage = value;
}
