using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int chaseSpeed = 20;
    private PlayerController player;
    private Rigidbody rb;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable() => ResetProperties();

    private void FixedUpdate() => Chase();

    private void Chase()
    {
        Vector3 lookDirection = player.transform.position - transform.position.normalized;
        rb.AddForce(lookDirection * chaseSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
    }

    private void ResetProperties()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}