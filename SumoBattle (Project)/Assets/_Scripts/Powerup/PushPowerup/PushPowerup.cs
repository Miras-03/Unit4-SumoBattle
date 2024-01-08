using UnityEngine;

namespace Powerup.Pushpowerup
{
    public sealed class PushPowerup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PushPowerupController>().OnPowerupTake();
                Destroy(gameObject);
            }
        }
    }
}