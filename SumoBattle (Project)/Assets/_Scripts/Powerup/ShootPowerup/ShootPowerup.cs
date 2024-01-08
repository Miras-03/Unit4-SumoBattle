using UnityEngine;

namespace Powerup.Shootpowerup
{
    public sealed class ShootPowerup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<ShootPowerupController>().OnPowerupTake();
                Destroy(gameObject);
            }
        }
    }
}