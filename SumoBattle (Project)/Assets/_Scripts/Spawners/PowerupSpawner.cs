using System;
using UnityEngine;

public sealed class PowerupSpawner : MonoBehaviour
{
    public Action OnNewWave;
    [SerializeField] private Transform[] powerups;

    private const int diffucultStage = 2;
    private int waveStage = 1;

    private void Start() => SpawnPowerup();

    private void OnEnable() => OnNewWave += SpawnPowerup;

    private void OnDestroy() => OnNewWave -= SpawnPowerup;

    private void SpawnPowerup()
    {
        int size = powerups.Length;
        int randIndex = waveStage > diffucultStage ? UnityEngine.Random.Range(0, size) : UnityEngine.Random.Range(0, size-1);
        Instantiate(powerups[randIndex], GenerateRandomVector(), Quaternion.identity);
        waveStage++;
    }

    private Vector3 GenerateRandomVector()
    {
        const int edgeOfCliff = 6;
        int randXPos = UnityEngine.Random.Range(-edgeOfCliff, edgeOfCliff);
        int randZPos = UnityEngine.Random.Range(-edgeOfCliff, edgeOfCliff);

        return new Vector3(randXPos, 0, randZPos);
    }
}