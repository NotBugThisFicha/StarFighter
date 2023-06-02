
using UnityEngine;

internal interface IMoveComponent
{
    public bool IsReached { get; }
    public void SetDestination(Vector2 end);
    public void MoveByRigidbodyVelocity(Vector2 vector);
    public void SetVelocity(Vector2 velocity);
}
