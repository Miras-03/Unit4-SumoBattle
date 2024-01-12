using System.Collections;
using UnityEngine;

public sealed class OutOfCliffDestroy : MonoBehaviour
{
    private PowerupSpawner powerupSpawner;
    private EnemySpawner enemySpawner;
    private WaveStage waveStage;
    private EnemyDeath enemyDeath;

    private const int waitSec = 1;

    private void Awake()
    {
        powerupSpawner = FindObjectOfType<PowerupSpawner>();
        enemySpawner = FindObjectOfType<EnemySpawner>();

        waveStage = WaveStage.Instance;
        enemyDeath = EnemyDeath.Instance;
    }

    private void OnEnable() => StartCoroutine(DelayOrDestroy());

    private IEnumerator DelayOrDestroy()
    {
        while (!HasFell())
            yield return new WaitForSeconds(1);
        CheckOrExecuteWave();
    }

    public void CheckOrExecuteWave()
    {
        bool areThereEnemies = --enemyDeath.Count < 1;
        if (areThereEnemies)
        {
            waveStage.Stage++;
            enemyDeath.Count = waveStage.Stage-1;
            StartCoroutine(WaitAndReleaseObject());
            ExecuteWave();
        }
    }

    private IEnumerator WaitAndReleaseObject()
    {
        yield return new WaitForSeconds(waitSec);
        gameObject.SetActive(false);
        
    }

    private void ExecuteWave()
    {
        powerupSpawner.OnNewWave();
        enemySpawner.OnNewWave();
    }

    private bool HasFell() => transform.position.y < 0;
}