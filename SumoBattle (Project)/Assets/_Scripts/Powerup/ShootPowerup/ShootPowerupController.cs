using System;
using System.Collections;
using UnityEngine;
using Powerup.Pushpowerup;

namespace Powerup.Shootpowerup
{
    public sealed class ShootPowerupController : MonoBehaviour
    {
        public Action OnPowerupTake;
        public Action OnPowerupOver;

        [SerializeField] private Transform projectile;
        private PlayerController playerController;

        private const int distanceView = 25;

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

        private void TakePowerup()
        {
            playerController.SetPowerupIndicator(true);
            StartCoroutine(PowerupCountDown());
            StartCoroutine(LaunchProjectiles());
        }

        private void OverPowerup()
        {
            playerController.SetPowerupIndicator(false);
            StopAllCoroutines();
        }

        private IEnumerator LaunchProjectiles()
        {
            while (true)
            {
                Transform[] enemies = GetEnemyTargets();
                bool isThereAnyEnemy = enemies != null && enemies.Length > 0;
                while (isThereAnyEnemy)
                {
                    foreach (Transform t in enemies)
                    {
                        if (t != null)
                        {
                            float distance = Vector3.Distance(t.position, transform.position);
                            if (distance < distanceView)
                            {
                                Transform p = Instantiate(projectile, transform.position, Quaternion.identity);
                                p.GetComponent<Projectile>().Target = t;
                            }
                        }
                    }
                    yield return new WaitForSeconds(1);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        private Transform[] GetEnemyTargets()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            int enemyCount = enemies.Length;
            Transform[] targets = new Transform[enemyCount];

            for (int i = 0; i < enemyCount; i++)
                targets[i] = enemies[i].transform;

            return targets;
        }

        private IEnumerator PowerupCountDown()
        {
            yield return new WaitForSeconds(7);
            OnPowerupOver.Invoke();
        }
    }
}