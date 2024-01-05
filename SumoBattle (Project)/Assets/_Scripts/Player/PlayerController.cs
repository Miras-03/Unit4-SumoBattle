using System.Collections;
using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform focusCentre;
    [SerializeField] private Transform powerupIndicator;
    private Rigidbody rb;
    private PlayerMove playerMove;

    private bool hasPowerUp;

    private const int powerupStrength = 15;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMove = new PlayerMove(rb, focusCentre);
    }

    private void Start() => SetPowerupIndicator(false);

    private void Update() => playerMove.IsPressingBoost();

    private void FixedUpdate()
    {
        playerMove.CheckOrMove();
        powerupIndicator.position = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && hasPowerUp)
            ApplyPowerup(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerUp = true;
            SetPowerupIndicator(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountDown());
        }
    }

    private void ApplyPowerup(Collision collision)
    {
        Rigidbody enemyRb = collision.collider.GetComponent<Rigidbody>();
        Vector3 direction = collision.transform.position - transform.position;
        enemyRb.AddForce(direction * powerupStrength, ForceMode.Impulse);
    }
    
    private void SetPowerupIndicator(bool flag) => powerupIndicator.gameObject.SetActive(flag);

    private IEnumerator PowerupCountDown()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = !hasPowerUp;
        SetPowerupIndicator(false);
    }
}