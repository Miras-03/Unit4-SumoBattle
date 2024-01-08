using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    private const int chaseSpeed = 30;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() => Chase();

    private void Chase()
    {
        Vector3 lookDirection = player.transform.position - transform.position.normalized;
        rb.AddForce(lookDirection * chaseSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
    }
}