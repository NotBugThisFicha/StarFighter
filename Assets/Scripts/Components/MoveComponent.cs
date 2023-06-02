using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class MoveComponent : MonoBehaviour, IMoveComponent
    {
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private float speed = 5.0f;

        private Vector2 destinationPoint =Vector2.zero;

        private bool isReached;
        public bool IsReached => isReached;

        private void Awake(){
            TryGetComponent(out rigidbody2D);
        }
        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = this.rigidbody2D.position + vector * this.speed;
            this.rigidbody2D.MovePosition(nextPosition);
        }

        public void SetDestination(Vector2 end) {
            destinationPoint = end;
            isReached = false;
        }

        public void SetVelocity(Vector2 velocity)
        {
            this.rigidbody2D.velocity = velocity;
        }

        private void FixedUpdate()
        {
            if (isReached || destinationPoint == Vector2.zero)
            {
                return;
            }

            var vector = destinationPoint - (Vector2)this.transform.position;
            if (vector.magnitude <= 0.25f)
            {
                this.isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            MoveByRigidbodyVelocity(direction);
        }
    }
}