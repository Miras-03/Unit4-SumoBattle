using System;
using System.Collections;
using UnityEngine;

namespace Powerup.Pushpowerup
{
    public sealed class PushPowerupController : MonoBehaviour
    {
        public Action OnPowerupTake;
        public Action OnPowerupOver;
        private PlayerController playerController;

        private bool hasPowerUp;
        private const int powerupStrength = 15;

        private void Awake() => playerController = GetComponent<PlayerController>();

        private void OnEnable()
        {
            OnPowerupTake += TakePowerup;
            OnPowerupOver += OverPowerup;
        }

        private void OnDestroy()
        {
            OnPowerupTake -= TakePowerup;
            OnPowerupOver -= OverPowerup;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Enemy") && hasPowerUp)
                ApplyPowerup(collision);
        }

        private void TakePowerup()
        {
            StartCoroutine(PowerupCountDown());
            hasPowerUp = true;
            playerController.SetPowerupIndicator(true);
        }

        private void OverPowerup()
        {
            hasPowerUp = !hasPowerUp;
            playerController.SetPowerupIndicator(false);
        }

        private void ApplyPowerup(Collision collision)
        {
            Rigidbody enemyRb = collision.collider.GetComponent<Rigidbody>();
            Vector3 direction = collision.transform.position - transform.position;
            enemyRb.AddForce(direction * powerupStrength, ForceMode.Impulse);
        }

        private IEnumerator PowerupCountDown()
        {
            yield return new WaitForSeconds(7);
            OnPowerupOver.Invoke();
        }
    }
}