using System;
using System.Collections;
using UnityEngine;

public sealed class SpawnManager : MonoBehaviour
{
    public Action OnNewWave;
    [SerializeField] private Transform[] enemies;
    [SerializeField] private Transform[] powerups;

    private int wavesCount = 1;

    private void Start() => ExecuteSpawn();

    private void OnEnable() => OnNewWave += ExecuteSpawn;

    private void OnDestroy() => OnNewWave -= ExecuteSpawn;

    private void ExecuteSpawn()
    {
        SpawnPowerup();
        SpawnEnemy();
    }

    public void SpawnPowerup()
    {
        Vector3 randVector;
        GeneratyRandomVector(out randVector);
        int maxRange = wavesCount > 2 ? 2 : 1;
        int randIndex = UnityEngine.Random.Range(0, maxRange);
        Instantiate(powerups[randIndex], randVector, Quaternion.identity);
    }

    private void GeneratyRandomVector(out Vector3 randVector)
    {
        const int edgeOfCliff = 6;
        int randXPos = UnityEngine.Random.Range(-edgeOfCliff, edgeOfCliff);
        int randZPos = UnityEngine.Random.Range(-edgeOfCliff, edgeOfCliff);

        randVector = new Vector3(randXPos, 0, randZPos);
    }

    public void SpawnEnemy() => StartCoroutine(SpawnWithDelay());

    private IEnumerator SpawnWithDelay()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < wavesCount; i++)
        {
            Vector3 randVector;
            GeneratyRandomVector(out randVector);
            int maxRange = wavesCount > 2 ? 3 : 2;
            int index = UnityEngine.Random.Range(0, maxRange);
            Instantiate(enemies[index], randVector, Quaternion.identity);
        }
        EnemyDeathCount.Instance.Count = wavesCount;
        wavesCount++;
    }
}