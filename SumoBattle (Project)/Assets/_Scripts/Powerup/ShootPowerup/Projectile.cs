using UnityEngine;

namespace Powerup.Pushpowerup
{
    public sealed class Projectile : MonoBehaviour
    {
        private Rigidbody rb;

        private const int speed = 5;
        private const int powerupStrength = 500;

        private void Awake() => rb = GetComponent<Rigidbody>();

        private void FixedUpdate()
        {
            if (Target != null)
            {
                Vector3 direction = (Target.position - transform.position).normalized;
                rb.AddForce(direction * speed, ForceMode.Impulse);
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                ApplyPowerup(collision);
                Destroy(gameObject);
            }
        }

        private void ApplyPowerup(Collider collision)
        {
            Rigidbody enemyRb = collision.GetComponent<Rigidbody>();
            Vector3 direction = collision.transform.position - transform.position;
            enemyRb.AddForce(-direction * powerupStrength, ForceMode.Acceleration);
        }

        public Transform Target { get; set; }
    }
}