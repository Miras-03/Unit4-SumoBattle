using UnityEngine;

public sealed class HeavyEnemy : Enemy 
{
    private const int powerupStrength = 25;

    private void OnCollisionEnter(Collision collision) => ApplyPowerup(collision);

    private void ApplyPowerup(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody enemyRb = collision.collider.GetComponent<Rigidbody>();
            Vector3 direction = collision.transform.position - transform.position;
            enemyRb.AddForce(direction * powerupStrength, ForceMode.Acceleration);
        }
    }
}
