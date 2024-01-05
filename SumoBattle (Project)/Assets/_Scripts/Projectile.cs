using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;

    private const int speed = 150;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        GameObject enemy = FindNearestEnemy();
        if (!enemy)
        {
            Vector3 direction = (enemy.transform.position - transform.position).normalized;

            // Apply force towards the target
            rb.AddForce(direction * speed, ForceMode.Impulse);
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}
