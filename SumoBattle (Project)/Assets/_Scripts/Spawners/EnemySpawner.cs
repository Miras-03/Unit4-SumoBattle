using System;
using System.Collections;
using UnityEngine;

public sealed class EnemySpawner : MonoBehaviour
{
    public Action OnNewWave;
    private ObjectsPooler objectsPooler;

    private int waveCount = 1;
    private const int startSec = 2;
    private const int diffucultStage = 2;

    private void Awake() => objectsPooler = GetComponent<ObjectsPooler>();

    private void Start()
    {
        objectsPooler.CreateObjects(transform);
        GetEnemy();
    }

    private void OnEnable() => OnNewWave += GetEnemy;

    private void OnDestroy() => OnNewWave -= GetEnemy;

    public void GetEnemy() => StartCoroutine(GetWithDelay());

    private IEnumerator GetWithDelay()
    {
        yield return new WaitForSeconds(startSec);
        for (int i = 0; i < waveCount; i++)
            objectsPooler.GetObjects(GenerateRandomTag().ToString(), GenerateRandomVector(), Quaternion.identity);
        EnemyDeathCount.Instance.Count = waveCount;
        waveCount++;
    }

    private ObjectTypes GenerateRandomTag()
    {
        ObjectTypes[] randomEnemies = { ObjectTypes.OrdinaryEnemy, ObjectTypes.HeavyEnemy, ObjectTypes.BigEnemy };
        int enemiesCount = randomEnemies.Length;
        int randIndex = waveCount > diffucultStage ? UnityEngine.Random.Range(0, enemiesCount) : UnityEngine.Random.Range(0, enemiesCount-1);
        return randomEnemies[randIndex];
    }

    private Vector3 GenerateRandomVector()
    {
        const int edgeOfCliff = 6;
        int randXPos = UnityEngine.Random.Range(-edgeOfCliff, edgeOfCliff);
        int randZPos = UnityEngine.Random.Range(-edgeOfCliff, edgeOfCliff);

        return new Vector3(randXPos, 0, randZPos);
    }

    internal enum ObjectTypes
    {
        OrdinaryEnemy,
        HeavyEnemy,
        BigEnemy
    }
}