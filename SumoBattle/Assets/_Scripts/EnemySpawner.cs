using UnityEngine;

public sealed class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        Vector3 randVector;
        GeneratyRandomVector(out randVector);

        Instantiate(enemy, randVector, Quaternion.identity);
    }

    private void GeneratyRandomVector(out Vector3 randVector)
    {
        const int edgeOfCliff = 6;
        int randXPos = Random.Range(-edgeOfCliff, edgeOfCliff);
        int randZPos = Random.Range(-edgeOfCliff, edgeOfCliff);

        randVector = new Vector3(randXPos, 0, randZPos);
    }
}