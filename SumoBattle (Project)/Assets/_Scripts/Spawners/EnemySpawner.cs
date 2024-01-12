using System;
using System.Collections;
using UnityEngine;

public sealed class EnemySpawner : MonoBehaviour
{
    public Action OnNewWave;
    private ObjectsPooler objectsPooler;
    private WaveStage waveStage;

    private const int startSec = 2;
    private const int diffucultStage = 2;

    private void Awake()
    {
        objectsPooler = GetComponent<ObjectsPooler>();
        waveStage = WaveStage.Instance;
    }

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
        for (int i = 0; i < waveStage.Stage; i++)
            objectsPooler.GetObjects(GenerateRandomTag().ToString(), GenerateRandomVector(), Quaternion.identity);
    }

    private ObjectTypes GenerateRandomTag()
    {
        ObjectTypes[] randomEnemies = { ObjectTypes.OrdinaryEnemy, ObjectTypes.HeavyEnemy, ObjectTypes.BigEnemy };
        int enemiesCount = randomEnemies.Length;
        int randIndex = waveStage.Stage > diffucultStage ? UnityEngine.Random.Range(0, enemiesCount) : 0;
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