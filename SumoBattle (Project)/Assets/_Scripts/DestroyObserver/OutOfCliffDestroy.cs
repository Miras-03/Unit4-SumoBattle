using System.Collections;
using UnityEngine;

public sealed class OutOfCliffDestroy : MonoBehaviour
{
    private PowerupSpawner powerupSpawner;
    private EnemySpawner enemySpawner;

    private const int waitSec = 1;

    private void Awake()
    {
        powerupSpawner = FindObjectOfType<PowerupSpawner>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Start() => StartCoroutine(DelayOrDestroy());

    private IEnumerator DelayOrDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            CheckOrDestroy();
        }
    }

    private void CheckOrDestroy()
    {
        if (transform.position.y < 0)
        {
            CheckAndExecuteWave();
            Destroy(gameObject, waitSec);
        }
    }

    public void CheckAndExecuteWave()
    {
        int count = --EnemyDeathCount.Instance.Count;
        if (count == 0)
        {
            powerupSpawner.OnNewWave.Invoke();
            enemySpawner.OnNewWave.Invoke();
        }
    }
}