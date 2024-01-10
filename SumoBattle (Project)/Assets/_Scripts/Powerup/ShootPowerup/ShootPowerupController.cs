using System;
using System.Collections;
using UnityEngine;
using Powerup.Pushpowerup;
using UnityEngine.Pool;

namespace Powerup.Shootpowerup
{
    public sealed class ShootPowerupController : MonoBehaviour
    {
        public Action OnPowerupTake;
        public Action OnPowerupOver;

        [SerializeField] private Projectile projectile;
        private PlayerController playerController;
        private ObjectPool<Projectile> pool;

        private const int distanceView = 25;

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
            pool = new ObjectPool<Projectile>(CreateObject, GetObject, ReleaseObject, DestroyObject,
                collectionCheck: false,
                defaultCapacity: 9,
                maxSize: 18 );
        }

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

        private Projectile CreateObject() => Instantiate(projectile, transform.position, Quaternion.identity);

        private void GetObject(Projectile obj) => obj.gameObject.SetActive(true);

        private void ReleaseObject(Projectile obj) => obj.gameObject.SetActive(false);

        private void DestroyObject(Projectile obj) => Destroy(obj.gameObject);

        private void RemoveObject(Projectile obj) => pool.Release(obj);

        private void ResetObjectProperties(Projectile obj)
        {
            obj.transform.position = transform.position;
            obj.transform.rotation = Quaternion.identity;
            obj.Rigidbody.velocity = Vector3.zero;
            obj.Rigidbody.angularVelocity = Vector3.zero;
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
                                Projectile obj = pool.Get();
                                ResetObjectProperties(obj);
                                obj.GetComponent<Projectile>().Target = t;
                                obj.Init(RemoveObject);
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
            Enemy[] enemies = FindObjectsOfType<Enemy>();
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