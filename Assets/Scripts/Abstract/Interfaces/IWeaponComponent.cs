
using ShootEmUp;
using UnityEngine;

internal interface IWeaponComponent
{
    public Vector2 Position { get; }
    public Quaternion Rotation { get; }
    public void FlyBulletByArgs(BulletArgs args);
    public void FlyBulletByDirection(Vector2 direction);
    public void RateFire();
    public void SetTarget(Transform target);
    public void SetBulletSpawner(IBulletSpawner factory);
}
